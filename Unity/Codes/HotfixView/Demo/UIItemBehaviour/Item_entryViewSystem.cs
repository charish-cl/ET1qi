
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_entryDestroySystem : DestroySystem<Scroll_Item_entry> 
	{
		public override void Destroy( Scroll_Item_entry self )
		{
			self.DestroyWidget();
		}
	}
}
