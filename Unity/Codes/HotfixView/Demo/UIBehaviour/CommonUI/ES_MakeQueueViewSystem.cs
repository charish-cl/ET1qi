
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ES_MakeQueueAwakeSystem : AwakeSystem<ES_MakeQueue,Transform> 
	{
		public override void Awake(ES_MakeQueue self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ES_MakeQueueDestroySystem : DestroySystem<ES_MakeQueue> 
	{
		public override void Destroy(ES_MakeQueue self)
		{
			self.DestroyWidget();
		}
	}
}
