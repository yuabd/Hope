using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hope.Helpers;
using Hammer.Entity;

namespace Hope.Areas.Admin.Controllers
{
	[Authorize(Roles = "1")]
    public class MenuController : Controller
    {
        //
        // GET: /Admin/Menu/

		public ActionResult MenuList(int? systemID = 1)
		{
			ViewBag.SystemID = systemID;
			return View();
		}

		public ActionResult MenuJosn(string id, int? systemID = 1)
		{
			var list = new MenuHelper().GetMenus(id, systemID);

			return Json(list);
		}

		public ActionResult AddMenu(int? systemID = 1)
		{
			ViewBag.SystemID = systemID;
			return View();
		}

		public ActionResult AddMenuJson(MenuEntity menu)
		{
			menu.Enable = "Y";
			menu.SystemID = 1;
			var result = new MenuHelper().InsertMenu(menu);

			return Json(result);
		}

		public ActionResult EditMenu(int id)
		{
			var menu = new MenuHelper().GetMenuByID(id);

			return View(menu);
		}

		public ActionResult EditMenuJson(MenuEntity menu)
		{
			var result = new MenuHelper().EditMenu(menu);

			return Json(result);
		}

		public ActionResult EditPage()
		{
			//var page = new Helpers.Helper().getpa
			return View();
		}

		public ActionResult DeleteMenu(int id, string state, string type)
		{
			var result = new MenuHelper().DisEnableMenu(id, state, type);
			return Json(result);
		}

    }
}
