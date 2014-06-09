using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Hope.Models;
using System.Web.Script.Serialization;

namespace Hope.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

		/// <summary>
		/// 上传图片
		/// </summary>
		/// <returns></returns>
		public ActionResult UploadImage()
		{
			HttpPostedFileBase file = Request.Files["Filedata"];

			//var photo = photoService.InsertPhoto(new Photo() { });

			var guid = Guid.NewGuid();

			string saveUrl = DateTime.Now.ToString("yyyyMM") + @"\" + DateTime.Now.ToString("yyyyMM") + guid + ".jpg";

			string url = System.Web.HttpContext.Current.Server.MapPath("/content/images/" + saveUrl);

			string directory = Path.GetDirectoryName(url);
			if (directory != null && !Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			if (file != null) file.SaveAs(url);

			file = null;

			return Json(new { queueID = 1, imgUrl = saveUrl.Replace(@"\", "/") });
		}

		public ActionResult UploadPicture(HttpPostedFileBase filedata)
		{
			xheditorModel model = new xheditorModel();

			if (filedata.ContentLength > 0)
			{
				var guid = Guid.NewGuid();
				var fileName = guid + ".jpg";
				Hope.Helpers.IO.UploadImageFile(filedata.InputStream, "/Content/images", fileName, 720);

				model.msg = "/Content/images/" + fileName;
			}

			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return this.Content(javaScriptSerializer.Serialize(model));
		}
    }
}
