namespace ET
{
    public static partial class TimerType
    {
        // 框架层0-1000，逻辑层的timer type 1000-9999
        public const int MoveTimer = 1001;
        public const int AITimer = 1002;
        public const int SessionAcceptTimeout = 1003;

        public const int AccountSessionCheckOutTime = 1004;

        public const int PlayerOfflineOutTime = 1005;
        
        public const int NoticeUnitNumericTime = 1006;

        public const int SaveChangeDBData = 1007;

        public const int BattleRound = 1008;
        
        public const int MakeQueueOver = 1009;
        
        public const int MakeQueueUI = 1010;
        
        public const int RankUI = 1010;
        
        // 不能超过10000
    }
}