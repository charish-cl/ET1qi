
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_productionDestroySystem : DestroySystem<Scroll_Item_production> 
	{
		public override void Destroy( Scroll_Item_production self )
		{
			self.DestroyWidget();
		}
	}
}
