
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ES_EquipItemAwakeSystem : AwakeSystem<ES_EquipItem,Transform> 
	{
		public override void Awake(ES_EquipItem self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ES_EquipItemDestroySystem : DestroySystem<ES_EquipItem> 
	{
		public override void Destroy(ES_EquipItem self)
		{
			self.DestroyWidget();
		}
	}
}
