using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hammer.Logic.Helper;
using Hope.Helpers;
using Hammer.Logic.Models;

namespace Hope.Areas.Admin.Controllers
{
	[Authorize(Roles = "1")]
	public class NewsManageController : Controller
	{
		//
		// GET: /Admin/NewsManage/

		public ActionResult NewsListView()
		{
			return View();
		}

		public ActionResult NewsListJson()
		{
			int tCount = 0;
			GetReportDataParams param = new GetReportDataParams();
			List<KeyValue> where = new Functions().GetParam(Request);
			where.Add(new KeyValue { Key = "CategoryID", Value = "1" });

			param.PageIndex = string.IsNullOrEmpty(Request["page"]) ? 1 : Convert.ToInt32(Request["page"]);
			param.PageSize = string.IsNullOrEmpty(Request["rows"]) ? 20 : Convert.ToInt32(Request["rows"]);
			param.Order = Request["sort"] == null ? "" : Request["sort"] + " " + Request["order"];
			param.Where = where;

			var list = new NewsHelper().GetNewsList(param, out tCount);
			var json = new DataGridJson(tCount, list);

			return Json(json);
		}

		public ActionResult NewsAddView(int? categoryID)
		{
			ViewBag.CategoryID = categoryID;

			return View();
		}

		public ActionResult NewsAddJson(News news)
		{
			var result = new NewsHelper().InsertNews(news);

			return Json(result);
		}

		public ActionResult NewsEditView(int id)
		{
			var news = new NewsHelper().GetNewsByID(id);

			return View(news);
		}

		[ValidateInput(false)]
		public ActionResult NewsEditJson(News news)
		{
			var result = new NewsHelper().UpdateNews(news);

			return Json(result);
		}

		public ActionResult DeleteNewsJson(int id)
		{
			var result = new NewsHelper().DeleteNews(id);

			return Json(result);
		}
	}
}
