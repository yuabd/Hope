using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hope.Helpers;

namespace Hope.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
			ViewBag.Contact = "current";
			var news = new NewsHelper().GetNewsByID(2);

			return View(news);
        }

    }
}
