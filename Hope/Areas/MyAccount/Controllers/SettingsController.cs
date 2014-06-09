using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hope.Helpers;
using Hammer.Logic.Helper;
using Hammer.Logic.Models;

namespace Hope.Areas.MyAccount.Controllers
{
    public class SettingsController : Controller
    {
        //
        // GET: /MyAccount/Settings/

        public ActionResult Profile()
        {
			var user = new UserHelper().GetUser(User.Identity.Name.Uint());

			return View(user);
        }

		public ActionResult ProfileJosn(User user)
		{
			var result = new UserHelper().EditProfile(user);

			return Json(result);
		}

    }
}
