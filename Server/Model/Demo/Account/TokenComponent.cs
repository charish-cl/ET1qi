using System.Collections.Generic;

namespace ET
{
    // 用于存储Token信息
    [ComponentOf(typeof(Scene))]
    public class TokenComponent : Entity,IAwake
    {
        public readonly Dictionary<long, string> TokenDictionary = new Dictionary<long, string>();
    }
} 