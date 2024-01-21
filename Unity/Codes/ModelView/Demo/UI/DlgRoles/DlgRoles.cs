using System.Collections.Generic;

namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	public  class DlgRoles :Entity,IAwake,IUILogic
	{

		public DlgRolesViewComponent View { get => this.Parent.GetComponent<DlgRolesViewComponent>();}

		public Dictionary<int, Scroll_Item_role> ScrollItemRoles;


	}
}
