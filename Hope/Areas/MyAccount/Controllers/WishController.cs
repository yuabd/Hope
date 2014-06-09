using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hammer.Logic.Helper;
using Hope.Helpers;
using Hammer.Logic.Models;

namespace Hope.Areas.MyAccount.Controllers
{
	[Authorize]
	public class WishController : Controller
	{
		//
		// GET: /MyAccount/Wish/

		public ActionResult WishListView(int? status)
		{
			ViewBag.Status = status;
			return View();
		}

		public ActionResult WishListJson()
		{
			int tCount = 0;
			GetReportDataParams param = new GetReportDataParams();
			List<KeyValue> where = new Functions().GetParam(Request);
			//where.Add(new KeyValue { Key = "Status1", Value = WishStatus.WaitAudit.ToString() });
			where.Add(new KeyValue { Key = "UserID", Value = User.Identity.Name });

			param.PageIndex = string.IsNullOrEmpty(Request["page"]) ? 1 : Convert.ToInt32(Request["page"]);
			param.PageSize = string.IsNullOrEmpty(Request["rows"]) ? 20 : Convert.ToInt32(Request["rows"]);
			param.Order = Request["sort"] == null ? "" : Request["sort"] + " " + Request["order"];
			param.Where = where;

			var list = new WishHelper().GetWishReport(param, out tCount);
			var json = new DataGridJson(tCount, list);

			return Json(json);
		}

		public ActionResult AlreadyApplyWishView()
		{
			ViewBag.ApplyUserID = User.Identity.Name;

			return View();
		}

		public ActionResult AlreadyApplyWishJson()
		{
			int tCount = 0;
			GetReportDataParams param = new GetReportDataParams();
			List<KeyValue> where = new Functions().GetParam(Request);

			param.PageIndex = string.IsNullOrEmpty(Request["page"]) ? 1 : Convert.ToInt32(Request["page"]);
			param.PageSize = string.IsNullOrEmpty(Request["rows"]) ? 20 : Convert.ToInt32(Request["rows"]);
			param.Order = Request["sort"] == null ? "" : Request["sort"] + " " + Request["order"];
			param.Where = where;

			var list = new WishHelper().GetWishReport(param, out tCount);
			var json = new DataGridJson(tCount, list);

			return Json(json);
		}

		public ActionResult AddWishView()
		{
			return View();
		}

		public ActionResult AddWishJosn(Wish wish)
		{

			wish.UserID = User.Identity.Name.Uint();

			var result = new WishHelper().AddWish(wish);

			return Json(result);
		}

		public ActionResult EditWishView(int id)
		{
			var wish = new WishHelper().GetWishByID(id);

			return View(wish);
		}

		public ActionResult EditWishJson(Wish wish)
		{
			var result = new WishHelper().UpdateWish(wish);
			return Json(result);
		}

		public ActionResult DeleteWishJson(int id)
		{
			var result = new WishHelper().DeleteWish(id);

			return Json(result);
		}

		public ActionResult MakeDone(int id)
		{
			var result = new WishHelper().MakeDone(id);

			return Json(result);
		}
	}
}
