using System;
using System.Text.RegularExpressions;

namespace ET
{
    [FriendClass(typeof(Account))]
    public class C2A_LoginAccountHandler : AMRpcHandler<C2A_LoginAccount,A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }
            //不及时移除，将在5秒后断开连接
            session.RemoveComponent<SessionAcceptTimeoutComponent>();
            //服务端接受客户端多次请求，客户端可能多次点击登录，同时发送多个请求
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                //reply（）后session不能直接Dispose（），session reply（）是有延迟的
                session.Disconnect().Coroutine();
                return;
            }
            
            //判断账号密码是否为空
            if (string.IsNullOrEmpty(request.AccountName) || string.IsNullOrEmpty(request.Password))
            {
                //返回错误码
                response.Error = ErrorCode.ERR_LoginInfoIsNull;
                reply();
                session.Disconnect().Coroutine();
                return;
            }
            
            if (!Regex.IsMatch(request.AccountName.Trim(),@"^(?=.*[0-9].*)(?=.*[A-Z].*)(?=.*[a-z].*).{6,15}$"))
            {
                response.Error = ErrorCode.ERR_AccountNameFormError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }
   
            if (!Regex.IsMatch(request.Password.Trim(),@"^[A-Za-z0-9]+$"))
            {
                response.Error = ErrorCode.ERR_PasswordFormError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            if (session.GetComponent<AccountsZone>() == null)
            {
                session.AddComponent<AccountsZone>();
            }

            if (session.GetComponent<RoleInfosZone>() == null)
            {
                session.AddComponent<RoleInfosZone>();
            }
            
            using (session.AddComponent<SessionLockingComponent>())
            {
                //登陆账号hashcode作为锁,解决A和B同时注册账号的问题
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount,request.AccountName.Trim().GetHashCode()))
                {
                    var accountInfoList = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Query<Account>(d=>d.AccountName.Equals(request.AccountName.Trim()));
                    Account account     = null;
                    if (accountInfoList!=null && accountInfoList.Count > 0)
                    {
                        //查询的第一个满足条件的账号
                        account = accountInfoList[0];
                        session.GetComponent<AccountsZone>().AddChild(account);
                        //判断账号是否在黑名单中
                        if (account.AccountType == (int)AccountType.BlackList)
                        {
                            response.Error = ErrorCode.ERR_AccountInBlackListError;
                            reply();
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }

                        //判断登录密码是否正确
                        if (!account.Password.Equals(request.Password))
                        {
                            response.Error = ErrorCode.ERR_LoginPasswordError;
                            reply();
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        account             = session.GetComponent<AccountsZone>().AddChild<Account>();
                        account.AccountName = request.AccountName.Trim();
                        account.Password    = request.Password;
                        account.CreateTime  = TimeHelper.ServerNow();
                        account.AccountType = (int)AccountType.General;
                        await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save<Account>(account);
                    }

                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "LoginCenter");
                    long loginCenterInstanceId = startSceneConfig.InstanceId;
                    var loginAccountResponse  = (L2A_LoginAccountResponse) await ActorMessageSenderComponent.Instance.Call(loginCenterInstanceId,new A2L_LoginAccountRequest(){AccountId = account.Id});

                    if (loginAccountResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = loginAccountResponse.Error;

                        reply();
                        session?.Disconnect().Coroutine();
                        account?.Dispose();
                        return;
                    }
                    //获取当前已经登陆的玩家的ID
                    long accountSessionInstanceId = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(account.Id);
                    Session otherSession   = Game.EventSystem.Get(accountSessionInstanceId) as Session;
                    //如果当前账号已经登陆，将其踢下线
                    otherSession?.Send(new A2C_Disconnect(){Error = 0});
                    otherSession?.Disconnect().Coroutine();
                    session.DomainScene().GetComponent<AccountSessionsComponent>().Add(account.Id,session.InstanceId);
                    //挂载在Session上，为何要增加AccountCheckOutTimeComponent,防止玩家直接关闭客户端，导致服务器无法知道玩家下线
                    session.AddComponent<AccountCheckOutTimeComponent, long>(account.Id);
                    
                    string Token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue).ToString();
                    
                    //拿到账号服务器上的TokenComponent组件，将Token存入其中,移除旧的Token
                    session.DomainScene().GetComponent<TokenComponent>().Remove(account.Id);
                    session.DomainScene().GetComponent<TokenComponent>().Add(account.Id,Token);
                    response.AccountId = account.Id;
                    response.Token     = Token;

                    reply();
                    account?.Dispose();
                    
                }
            }
         
            
        }
    }
}