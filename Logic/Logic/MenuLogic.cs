using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hammer.Entity;
using Hammer.Logic.DataAccess;
using Hammer.Logic.Models;
using Hammer.Logic.Helper;

namespace Hammer.Logic
{
	public class MenuLogic : DbAccess
	{
		public MenuLogic(SiteDataContext db)
			: base(db)
		{

		}

		public List<MenuEntity> GetAllPages(int? systemID)
		{
			var query = (from l in db.Pages
						 where l.Enable == "Y" && l.SystemID == systemID
						 orderby l.OrderIndex
						 select new MenuEntity
						 {
							 Enable = l.Enable,
							 ID = l.MenuID,
							 MenuName = l.PageName,
							 MenuUrl = l.PageUrl,
							 OrderIndex = l.OrderIndex,
							 ParentID = l.MenuID,
							 Selected = l.Selected,
							 SystemID = l.SystemID
						 }).ToList();
			return query;
		}

		public List<MenuEntity> GetAllMenus(int? systemID)
		{
			var query = (from l in db.Menus
						 where l.Enable == "Y" && l.SystemID == systemID
						 orderby l.OrderIndex
						 select new MenuEntity
						 {
							 Enable = l.Enable,
							 ID = l.ID,
							 MenuName = l.MenuName,
							 MenuUrl = "",
							 OrderIndex = l.OrderIndex,
							 ParentID = l.ParentID,
							 Selected = l.Selected,
							 SystemID = l.SystemID
						 }).ToList();
			return query;
		}

		public BaseObject InsertMenu(MenuEntity menu)
		{
			BaseObject obj = new BaseObject();

			if (menu.Type == "Page")
			{
				var samename = db.Pages.FirstOrDefault(m => m.PageName == menu.MenuName && m.MenuID == menu.ParentID);
				if (samename != null)
				{
					obj.Tag = -1;
					obj.Message = "页面名称已经存在！";
					return obj;
				}

				var page = new Page()
				{
					Enable = menu.Enable,
					Selected = menu.Selected,
					MenuID = (int)menu.ParentID,
					OrderIndex = menu.OrderIndex,
					PageName = menu.MenuName,
					PageUrl = menu.MenuUrl,
					SystemID = menu.SystemID
				};

				db.Pages.Add(page);
			}
			else
			{
				var samename = db.Menus.FirstOrDefault(m => m.MenuName == menu.MenuName && m.ParentID == menu.ParentID);
				if (samename != null)
				{
					obj.Tag = -1;
					obj.Message = "菜单名称已经存在！";
					return obj;
				}

				var u = new Menu()
				{
					Enable = menu.Enable,
					MenuName = menu.MenuName,
					//MenuUrl = menu.MenuUrl,
					OrderIndex = menu.OrderIndex,
					ParentID = menu.ParentID,
					Selected = menu.Selected,
					SystemID = menu.SystemID
				};

				db.Menus.Add(u);
			}

			try
			{
				db.SaveChanges();

				obj.Tag = 1;
				obj.Message = "新增成功！";
			}
			catch (Exception e)
			{
				obj.Tag = -1;
				obj.Message = "新增失败！";
			}

			return obj;
		}

		public MenuEntity GetMenuByID(int id)
		{
			var menu = (from l in db.Menus
						where l.ID == id
						select new MenuEntity
						{
							ID = id,
							Enable = l.Enable,
							MenuName = l.MenuName,
							MenuUrl = "",
							OrderIndex = l.OrderIndex,
							ParentID = l.ParentID,
							Selected = l.Selected,
							Type = "Menu",
							SystemID = l.SystemID
						}).FirstOrDefault();

			return menu;
		}

		public BaseObject EditMenu(MenuEntity menu)
		{
			BaseObject obj = new BaseObject();

			try
			{
				var m = db.Menus.FirstOrDefault(q => q.ID == menu.ID);
				if (m == null)
				{
					obj.Tag = -1;
					obj.Message = "记录不存在！";

					return obj;
				}

				//m.Enable = menu.Enable;
				m.MenuName = menu.MenuName;
				//m.MenuUrl = menu.MenuUrl;
				m.OrderIndex = menu.OrderIndex;
				//m.ParentID = menu.ParentID;
				m.Selected = menu.Selected;
				//m.SystemID = menu.SystemID;

				db.SaveChanges();
				obj.Tag = 1;
			}
			catch (Exception e)
			{
				obj.Tag = -1;
				obj.Message = e.Message;
			}

			return obj;
		}

		public BaseObject DelMenu(int id)
		{
			BaseObject obj = new BaseObject();
			var menu = db.Menus.Find(id);

			if (menu == null)
			{
				obj.Tag = -1;
				obj.Message = "记录不存在！";

				return obj;
			}

			try
			{
				db.Menus.Remove(menu);
				db.SaveChanges();

				obj.Tag = 1;
			}
			catch (Exception e)
			{
				obj.Tag = -2;
				obj.Message = e.Message;
			}

			return obj;
		}

		public List<MenuList> GetMenus(string ids, int? systemID)
		{
			int? menuID = null;
			int? pageID = null;
			if (!string.IsNullOrEmpty(ids))
			{
				if (ids.IndexOf("menu") > 0) menuID = Convert.ToInt32(ids.Split(':')[0]);
				if (ids.IndexOf("page") > 0) pageID = Convert.ToInt32(ids.Split(':')[0]);
			}

			var list = new List<MenuList>();
			var query1 = new List<MenuList>();
			var query2 = new List<MenuList>();
			var query3 = new List<MenuList>();

			if (string.IsNullOrEmpty(ids))
			{
				list = (from l in db.Menus
						where l.SystemID == systemID && (l.ParentID == null || l.ParentID == 0)
						select new MenuList
						{
							ID = l.ID,
							Enable = l.Enable,
							MenuName = l.MenuName,
							MenuUrl = "",
							OrderIndex = l.OrderIndex,
							_parentId = l.ParentID,
							Selected = l.Selected,
							SystemID = l.SystemID,
							Type = "Menu"
						}).ToList();
				for (int i = 0; i < list.Count; i++)
				{
					MenuList menu = list[i];
					menu.MenuGuid = Guid.NewGuid().ToString();
					menu.idtype = menu.ID + ":menu";
					menu.state = "closed";
				}
			}

			if (menuID != null)
			{
				query1 = (from l in db.Menus
						  where l.ParentID == menuID && l.SystemID == systemID
						  select new MenuList
						  {
							  ID = l.ID,
							  Enable = l.Enable,
							  MenuName = l.MenuName,
							  MenuUrl = "",
							  OrderIndex = l.OrderIndex,
							  _parentId = l.ParentID,
							  Selected = l.Selected,
							  SystemID = l.SystemID,
							  Type = "Menu"
						  }).ToList();
				for (int i = 0; i < query1.Count; i++)
				{
					MenuList menu = query1[i];
					menu.MenuGuid = Guid.NewGuid().ToString();
					menu.idtype = menu.ID + ":menu";
					menu.state = "closed";
				}

				query2 = (from l in db.Pages
						  where l.MenuID == menuID && l.SystemID == systemID
						  select new MenuList
						  {
							  ID = l.MenuID,
							  Enable = l.Enable,
							  MenuName = l.PageName,
							  MenuUrl = l.PageUrl,
							  OrderIndex = l.OrderIndex,
							  _parentId = menuID,
							  Selected = l.Selected,
							  SystemID = l.SystemID,
							  Type = "Page"
						  }).ToList();

				for (int i = 0; i < query2.Count; i++)
				{
					MenuList menu = query2[i];
					menu.MenuGuid = Guid.NewGuid().ToString();
					menu.idtype = menu.ID + ":page";
					menu.state = "";
				}
			}

			if (pageID != null)
			{
				query3 = (from l in db.Pages
						  where l.MenuID == menuID && l.SystemID == systemID
						  select new MenuList
						  {
							  ID = l.ID,
							  Enable = "Y",
							  MenuName = l.PageName,
							  MenuUrl = l.PageUrl,
							  OrderIndex = l.OrderIndex,
							  Selected = l.Selected,
							  SystemID = l.SystemID,
							  Type = "Page",
						  }).ToList();

				for (int i = 0; i < query3.Count; i++)
				{
					MenuList menu = query3[i];
					menu.MenuGuid = Guid.NewGuid().ToString();
					menu.idtype = menu.ID + ":page";
					menu.state = "";
				}
			}

			list.AddRange(query1);
			list.AddRange(query2);
			list.AddRange(query3);

			return list;
		}

		public List<MenuTree> HandleSubMenu(int? parentID)
		{
			var rtnList = new List<MenuTree>();
			var menulist = db.Menus.Where(m => m.ParentID == parentID).OrderByDescending(m => m.OrderIndex).ToList();

			foreach (var item in menulist)
			{
				if (parentID != null && item.ParentID.Equals(parentID))
				{
					var menu = new MenuTree()
					{
						id = item.ID,
						pId = parentID,
						name = item.MenuName,
						type = "Menu",
						children = HandleSubMenu(item.ID)
					};
					rtnList.Add(menu);
				}
			}

			return rtnList;
		}

		public List<MenuTree> GetTreeMenu(int? systemID)
		{
			var menus = new List<MenuTree>();
			var menulist = db.Menus.Where(m => (m.ParentID == 0 || m.ParentID == null) && m.SystemID == systemID).OrderByDescending(m => m.OrderIndex).ToList();
			foreach (var item in menulist)
			{
				var menu = new MenuTree()
				{
					id = item.ID,
					pId = item.ParentID,
					name = item.MenuName,
					type = "Menu",
					children = HandleSubMenu(item.ID)
				};

				menus.Add(menu);
			}

			return menus;
		}

		public BaseObject DisEnableMenu(int id, string state, string type)
		{
			var obj = new BaseObject();
			try
			{
				var newState = state == PublicType.Yes ? PublicType.No : PublicType.Yes;
				if (type.ToLower() == "page")
				{

					var page = db.Pages.FirstOrDefault(m => m.ID == id);
					if (page == null)
					{
						obj.Tag = -1;
						obj.Message = "操作失败!";
					}
					page.Enable = newState;
				}
				else if (type.ToLower() == "menu")
				{
					var menu = db.Menus.FirstOrDefault(m => m.ID == id);
					if (menu == null)
					{
						obj.Tag = -1;
						obj.Message = "操作失败!";
					}
					menu.Enable = newState;
				}

				db.SaveChanges();
				obj.Tag = 1;
			}
			catch (Exception e)
			{
				obj.Tag = -1;
				obj.Message = "内部错误!, 错误信息: " + e.Message;
			}

			return obj;
		}
	}
}
