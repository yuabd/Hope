using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hammer.Logic.Helper;
using Hope.Helpers;
using Hope.Models;
using Hammer.Logic.Models;

namespace Hope.Controllers
{
	public class NewsController : Controller
	{
		//
		// GET: /News/

		public ActionResult Index(int? page)
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
			var model = new SitePaginated<News>(list, page ?? 1, 20);
			ViewBag.News = "current";

			return View(model);
		}

		public ActionResult Detail(int id)
		{
			ViewBag.News = "current";
			var news = new NewsHelper().GetNewsByID(id);

			return View(news);
		}

	}
}
