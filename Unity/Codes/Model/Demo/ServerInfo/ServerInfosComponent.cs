using System.Collections.Generic;

namespace ET
{
    /// <summary>
    /// 记录所有服务器信息
    /// </summary>
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(ServerInfo))]
    public class ServerInfosComponent : Entity,IAwake,IDestroy
    {
        public List<ServerInfo> ServerInfoList = new List<ServerInfo>();

        public int CurrentServerId = 0;
    }
}