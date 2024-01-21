
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_attributeDestroySystem : DestroySystem<Scroll_Item_attribute> 
	{
		public override void Destroy( Scroll_Item_attribute self )
		{
			self.DestroyWidget();
		}
	}
}
