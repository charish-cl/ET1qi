namespace ET
{
    //心跳包检测组件，对应客户端的PingComponent
    [ComponentOf(typeof(Session))]
    public class SessionIdleCheckerComponent: Entity, IAwake<int>, IDestroy
    {
        public long RepeatedTimer;
    }
}