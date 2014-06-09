using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hammer.Logic;
using Hammer.Entity;
using Hammer.Logic.DataAccess;
using Hammer.Logic.Helper;

namespace Hope.Helpers
{
	public class MenuHelper : DbAccess
	{
		public List<MenuEntity> GetAllMenus(int? systemID)
		{
			using (MenuLogic logic = new MenuLogic(db))
			{
				return logic.GetAllMenus(systemID);
			}
		}

		public List<MenuEntity> GetAllPages(int? systemID)
		{
			using (MenuLogic logic = new MenuLogic(db))
			{
				return logic.GetAllPages(systemID);
			}
		}

		public BaseObject InsertMenu(MenuEntity menu)
		{
			using (MenuLogic logic = new MenuLogic(db))
			{
				return logic.InsertMenu(menu);
			}
		}

		public MenuEntity GetMenuByID(int id)
		{
			using (MenuLogic logic = new MenuLogic(db))
			{
				return logic.GetMenuByID(id);
			}
		}

		public BaseObject EditMenu(MenuEntity menu)
		{
			using (MenuLogic logic = new MenuLogic(db))
			{
				return logic.EditMenu(menu);
			}
		}

		public BaseObject DelMenu(int id)
		{
			using (MenuLogic logic = new MenuLogic(db))
			{
				return logic.DelMenu(id);
			}
		}

		public List<MenuList> GetMenus(string ids, int? systemID)
		{
			using (MenuLogic logic = new MenuLogic(db))
			{
				return logic.GetMenus(ids, systemID);
			}
		}

		public List<MenuTree> GetTreeMenu(int? systemID)
		{
			using (MenuLogic logic = new MenuLogic(db))
			{
				return logic.GetTreeMenu(systemID);
			}
		}

		public BaseObject DisEnableMenu(int id, string state, string type)
		{
			using (MenuLogic logic = new MenuLogic(db))
			{
				return logic.DisEnableMenu(id, state, type);
			}
		}
	}
}