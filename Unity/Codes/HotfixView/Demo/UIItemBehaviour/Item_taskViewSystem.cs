
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_taskDestroySystem : DestroySystem<Scroll_Item_task> 
	{
		public override void Destroy( Scroll_Item_task self )
		{
			self.DestroyWidget();
		}
	}
}
