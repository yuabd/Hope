using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hope.Helpers;

namespace Hope.Controllers
{
    public class ControlController : Controller
    {
        //
        // GET: /Control/

		public ActionResult MenuTreeView()
		{
			//ViewBag.SystemID = systemID;
			return PartialView();
		}

		public ActionResult GetMenuTreeJson(int? systemID = 2)
		{
			var menus = new SystemHelper().GetTreeMenu(systemID);

			//var json = JsonConvert.SerializeObject(menus);

			return Json(menus, JsonRequestBehavior.AllowGet);
		}

    }
}
