using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hope.Services;
using Hope.Models;
using Hope.Helpers;
using Hammer.Logic.Models;
using Hammer.Logic.Helper;

namespace Hope.Controllers
{
	public class HomeController : Controller
	{

		public ActionResult Index()
		{
			ViewBag.HomeTotal = new WishHelper().GetHomeTotal();
			ViewBag.Home = "current";
			return View();
		}
		//心愿
		public ActionResult WishList(int? status, string title, int? page)
		{
			var result = new WishHelper().GetWishList(status, title);
			var model = new SitePaginated<Wish>(result, page ?? 1, 9);

			switch (status)
			{
				case 1: ViewBag.B = "current"; break;
				case 2: ViewBag.C = "current"; break;
				case 3: ViewBag.D = "current"; break;
				default: ViewBag.A = "current"; break;
			}
			ViewBag.Wish = "current";

			return View(model);
		}

		public ActionResult Wish(int id)
		{
			var wish = new WishHelper().GetWishByID(id);
			var wishList = new WishHelper().GetSiteWishList().ToList();
			ViewBag.New = wishList.OrderByDescending(m => m.DateStart).Take(5).ToList();
			ViewBag.AlreadyApply = wishList.Where(m => m.Status == "进行中");
			ViewBag.Wish = "current";

			return View(wish);
		}

		public ActionResult Quit(int id)
		{
			var result = new WishHelper().QuitWish(id);

			return Json(result);
		}

		public ActionResult ApplyWish(int id)
		{
			var result = new WishHelper().ApplyWish(id, User.Identity.Name.Uint());

			return Json(result);
		}

		//支援者
		public ActionResult WeakerList(string name, int? page)
		{
			int tCount = 0;
			GetReportDataParams param = new GetReportDataParams();
			List<KeyValue> where = new Functions().GetParam(Request);

			if (!string.IsNullOrEmpty(name))
			{
				where.Add(new KeyValue { Key = "UserName", Value = name.UString() });
			}

			param.PageIndex = string.IsNullOrEmpty(Request["page"]) ? 1 : Convert.ToInt32(Request["page"]);
			param.PageSize = string.IsNullOrEmpty(Request["rows"]) ? 20 : Convert.ToInt32(Request["rows"]);
			param.Order = Request["sort"] == null ? "" : Request["sort"] + " " + Request["order"];
			param.Where = where;
			if (Request["sort"] != null)
			{
				var request = Request["sort"].UString();
				if (request.Contains("Heart"))
				{
					ViewBag.Heart = "current";
				}
				else if (request.Contains("Support"))
				{
					ViewBag.Support = "current";
				}
				else
				{
					ViewBag.All = "current";
				}
			}
			else
			{
				ViewBag.All = "current";
			}

			ViewBag.Weaker = "current";

			var list = new UserHelper().GetUserList(param, out tCount);
			var model = new SitePaginated<User>(list, page ?? 1, 20);

			return View(model);
		}

		public ActionResult Weaker(int id)
		{
			var user = new UserHelper().GetUserEntity(id);

			var wishList = new WishHelper().GetSiteWishList().ToList();
			ViewBag.New = wishList.OrderByDescending(m => m.DateStart).Take(5).ToList();
			ViewBag.AlreadyApply = wishList.Where(m => m.Status == "进行中");

			ViewBag.Publish = wishList.Where(m => m.UserID == id);
			ViewBag.Apply = wishList.Where(m => m.ApplyUserID == id);
			ViewBag.Weaker = "current";

			return View(user);
		}

		public ActionResult HomeTotalCount()
		{
			return View();
		}

		public ActionResult Support(int id, string type)
		{
			new WishHelper().Support(id, type);

			return null;
		}

		//
		// GET: /Home/
		//private SiteService siteService = new SiteService();

		//public ActionResult Index(int? page)
		//{
		//    var wishes = siteService.GetWishes().ToList();
		//    var pwishes = new Paginated<Wish>(wishes, page ?? 1, 20);

		//    return View(pwishes);
		//}

		//[HttpPost]
		//public ActionResult Apply(Apply apply)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        var wish = siteService.GetWish(apply.WishID);
		//        wish.IsApply = true;
		//        apply.Date = DateTime.Now;
		//        siteService.InsertApply(apply);
		//        siteService.Save();

		//        return RedirectToAction("Index");
		//    }

		//    return View(apply);
		//}
	}
}
