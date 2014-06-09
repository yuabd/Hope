using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hope.Services;
using Hope.Models;
using System.IO;
using Hope.Helpers;
using Hammer.Logic.Helper;
using Hammer.Logic.Models;

namespace Hope.Areas.Admin.Controllers
{
	[Authorize(Roles = "1")]
	public class WishController : Controller
	{
		//private SiteService siteService = new SiteService();
		//
		// GET: /Admin/Wish/

		public ActionResult WishView(int? status)
		{
			ViewBag.Status = status;

			return View();
		}

		public ActionResult WishJson()
		{
			int tCount = 0;
			GetReportDataParams param = new GetReportDataParams();
			List<KeyValue> where = new Functions().GetParam(Request);
			//where.Add(new KeyValue { Key = "Status1", Value = WishStatus.WaitAudit.ToString() });

			param.PageIndex = string.IsNullOrEmpty(Request["page"]) ? 1 : Convert.ToInt32(Request["page"]);
			param.PageSize = string.IsNullOrEmpty(Request["rows"]) ? 20 : Convert.ToInt32(Request["rows"]);
			param.Order = Request["sort"] == null ? "" : Request["sort"] + " " + Request["order"];
			param.Where = where;

			var list = new WishHelper().GetWishReport(param, out tCount);
			var json = new DataGridJson(tCount, list);

			return Json(json);
		}

		public ActionResult WaitWishView()
		{
			return View();
		}

		public ActionResult WaitWishJson()
		{
			int tCount = 0;
			GetReportDataParams param = new GetReportDataParams();
			List<KeyValue> where = new Functions().GetParam(Request);
			where.Add(new KeyValue { Key = "Status", Value = WishStatus.WaitAudit.ToString() });

			param.PageIndex = string.IsNullOrEmpty(Request["page"]) ? 1 : Convert.ToInt32(Request["page"]);
			param.PageSize = string.IsNullOrEmpty(Request["rows"]) ? 20 : Convert.ToInt32(Request["rows"]);
			param.Order = Request["sort"] == null ? "" : Request["sort"] + " " + Request["order"];
			param.Where = where;

			var list = new WishHelper().GetWishReport(param, out tCount);
			var json = new DataGridJson(tCount, list);

			return Json(json);
		}

		public ActionResult AuditWishJson(string ids)
		{
			var result = new WishHelper().AuditWish(ids);

			return Json(result);
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


		public ActionResult TransferWishView()
		{

			return View();
		}

		public ActionResult TransferWishJson(int id, int userID)
		{
			var result = new WishHelper().TransferWish(id, userID);

			return Json(result);
		}

		//public ActionResult Index(int? page, bool? id)
		//{
		//    var wishes = new List<Wish>();
		//    if (id != null)
		//    {
		//        wishes = siteService.GetWishes((bool)id).ToList();
		//    }
		//    else
		//    {
		//        wishes = siteService.GetWishes().ToList();
		//    }

		//    var pwishes = new Paginated<Wish>(wishes, page ?? 1, 20);

		//    return View(pwishes);
		//}

		//public ActionResult Add()
		//{
		//    var wish = new Wish();

		//    return View(wish);
		//}

		//[HttpPost]
		//public ActionResult Add(Wish wish)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        //siteService.InsertWish(wish);
		//        siteService.Save();

		//        return RedirectToAction("Index");
		//    }

		//    return View(wish);
		//}

		//public ActionResult Edit(int id)
		//{
		//    var wish = siteService.GetWish(id);

		//    return View(wish);
		//}

		//[HttpPost]
		//public ActionResult Edit(Wish wish)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        //siteService.UpdateWish(wish);
		//        siteService.Save();

		//        return RedirectToAction("Index");
		//    }

		//    return View(wish);
		//}

		//public ActionResult Delete(int id)
		//{
		//    var wish = siteService.GetWish(id);
		//    siteService.DeleteApply(id);

		//    siteService.DeleteWish(wish);
		//    siteService.Save();

		//    return RedirectToAction("Index");
		//}

		//public ActionResult DeleteApply(int id)
		//{
		//    var wish = siteService.GetWish(id);
		//    siteService.DeleteApply(id);
		//    wish.IsApply = false;
		//    siteService.Save();

		//    return RedirectToAction("Index");
		//}

		//public ActionResult GetApply(int id)
		//{
		//    var apply = siteService.GetApply(id);

		//    return View(apply);
		//}

		////public JsonResult GetApply(int applyID)
		////{
		////    var apply = siteService.GetApply(applyID);

		////    var result = Json(new
		////    {
		////        ContactName = apply.ContactName,
		////        ContactAddress = apply.ContactAddress,
		////        ContactEmail = apply.ContactEmail,
		////        ContactTel = apply.ContactTel,
		////        Message = apply.Message
		////    });

		////    return result;
		////}

		//public ActionResult Apply(int id)
		//{
		//    return View();
		//}

        public ActionResult UploadExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase file)
		{
		    string line;
		    string[] arrayLine;

		    if (file.ContentLength > 0)
		    {
		        if (file.ContentType == "application/vnd.ms-excel")
		        {
		            string filename = file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);
		            string filePath = HttpContext.Server.MapPath("/content/" + filename);
		            file.SaveAs(filePath);
		            using (StreamReader streamReader = new StreamReader(filePath))
		            {
		                while ((line = streamReader.ReadLine()) != null)
		                {
		                    var wish = new Wish();
		                    arrayLine = line.Split(new char[] { ',' });
                            //姓名
		                    wish.Name = arrayLine[2];
                            //许愿人介绍
		                    wish.Description = arrayLine[6];
                            //浏览
		                    wish.Count = 0;
                            //日期
		                    wish.DateStart = DateTime.Now;
                            //寄语
		                    //wish.Hope = arrayLine[4];
		                    wish.Title = arrayLine[6];
		                    wish.Status = 0;
		                    wish.PictureFile = "";
		                    wish.Support = 0;
                            wish.UserID = 1;
                            wish.WishDescription = arrayLine[9];

		                    new WishHelper().AddWish(wish);
		                    //siteService.Save();
                            //string a = arrayLine[9];
                            //if (a == "1")
                            //{
                            //    wish.IsApply = true;
                            //    var apply = new Apply();

                            //    apply.ContactName = arrayLine[10];
                            //    apply.ContactAddress = arrayLine[11];
                            //    apply.ContactTel = arrayLine[12];
                            //    apply.WishID = wish.WishID;

                            //    siteService.InsertApply(apply);
                            //}

                            //siteService.Save();
		                }
		            }
		        }
		    }

		    return View();
		}

	}
}
