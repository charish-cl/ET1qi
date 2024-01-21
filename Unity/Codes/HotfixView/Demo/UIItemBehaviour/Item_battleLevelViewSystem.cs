
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_battleLevelDestroySystem : DestroySystem<Scroll_Item_battleLevel> 
	{
		public override void Destroy( Scroll_Item_battleLevel self )
		{
			self.DestroyWidget();
		}
	}
}
