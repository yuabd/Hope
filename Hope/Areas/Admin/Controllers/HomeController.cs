using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using Hope.Helpers;

namespace Hope.Areas.Admin.Controllers
{
	[Authorize(Roles = "1")]
	public class HomeController : Controller
	{
		//
		// GET: /Admin/Home/

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Top()
		{
			var str = new ArrayList();
			var allMenus = new MenuHelper().GetAllMenus(1);
			var allPages = new MenuHelper().GetAllPages(1);

			ViewBag.AllMenus = allMenus;
			ViewBag.AllPages = allPages;

			return View();
		}

		public ActionResult Left()
		{
			return View();
		}

		public ActionResult Middle()
		{
			return View();
		}

	}
}
