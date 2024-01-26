using System.Collections.Generic;


namespace ET
{
	public static class RealmGateAddressHelper
	{
		public static StartSceneConfig GetGate(int zone,long accountId)
		{
			//获取当前登录的区服的所有网关
			List<StartSceneConfig> zoneGates = StartSceneConfigCategory.Instance.Gates[zone];
			//根据账号ID取模，获取一个网关 ，这样就可以保证一个账号登录的时候，每次都是登录到同一个网关
			int n = accountId.GetHashCode() % zoneGates.Count;

			return zoneGates[n];
		}
		
		
		public static StartSceneConfig GetRealm(int zone)
		{
			StartSceneConfig zoneRealm = StartSceneConfigCategory.Instance.Realms[zone];
			return zoneRealm;
		}
	}
}
