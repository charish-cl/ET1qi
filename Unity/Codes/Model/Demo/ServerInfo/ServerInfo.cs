namespace ET
{
    public enum ServerStatus
    {
        Normal = 0,
        Stop   = 1,//停服状态
    }

    //Entity自带ID,不用再加
    public class ServerInfo : Entity,IAwake
    {
        public int Status;
        public string ServerName;
    }
}