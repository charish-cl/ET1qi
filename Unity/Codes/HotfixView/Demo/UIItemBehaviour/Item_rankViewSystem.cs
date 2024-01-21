
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_rankDestroySystem : DestroySystem<Scroll_Item_rank> 
	{
		public override void Destroy( Scroll_Item_rank self )
		{
			self.DestroyWidget();
		}
	}
}
