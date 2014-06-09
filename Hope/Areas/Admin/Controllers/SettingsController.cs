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
    public class SettingsController : Controller
    {
        //
        // GET: /Admin/Settings/

        public ActionResult UserListView()
        {
            return View();
        }

		public ActionResult UserListJson()
		{
			int tCount = 0;
			GetReportDataParams param = new GetReportDataParams();
			List<KeyValue> where = new Functions().GetParam(Request);

			param.PageIndex = string.IsNullOrEmpty(Request["page"]) ? 1 : Convert.ToInt32(Request["page"]);
			param.PageSize = string.IsNullOrEmpty(Request["rows"]) ? 20 : Convert.ToInt32(Request["rows"]);
			param.Order = Request["sort"] == null ? "" : Request["sort"] + " " + Request["order"];
			param.Where = where;

			var list = new UserHelper().GetUserList(param, out tCount);
			var json = new DataGridJson(tCount, list);

			return Json(json);
		}

		public ActionResult EditUserView(int id)
		{
			var user = new UserHelper().GetUser(id);

			return View(user);
		}

		public ActionResult EditUserJson(User user)
		{
			var result = new UserHelper().EditProfile(user);

			return Json(result);
		}

		public ActionResult DeleteUserJson(int id)
		{
			var result = new UserHelper().DeleteUser(id);

			return Json(result);
		}

    }
}
