using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hope.Models;
using System.Web.Security;
using Hope.Helpers;
using Hammer.Logic.Models;

namespace Hope.Controllers
{
	public class AccountController : Controller
	{
		//
		// GET: /Account/

		public ActionResult Index(string returnUrl)
		{
			LoginViewModel model = new LoginViewModel();

			return View(model);
		}

		public ActionResult LoginJson(LoginViewModel model)
		{
			var result = new UserHelper().Login(model.UserID, model.Password, true);

			return Json(result);
		}

		public ActionResult GetValidateCode()
		{
			LoginHelper vCode = new LoginHelper();
			string code = vCode.CreateValidateCode(4);
			Session["ValidateCode"] = code;
			byte[] bytes = vCode.CreateValidateGraphic(code);

			return File(bytes, @"image/jpeg");
		}

		public ActionResult Register()
		{
			return View();
		}

		public ActionResult RegisterJson(User param, string cook)
		{
			if (cook != Session["ValidateCode"].ToString())
			{
				return Json(new { Tag = -2, Message = "验证码错误" });
			}

			var clearPwd = param.Password;
			var clearUserName = param.UserName;

			var result = new UserHelper().Register(param);

			if (result.Tag == 1)
			{
				new UserHelper().Login(clearUserName, clearPwd, true);
			}

			return Json(result);
		}

		[HttpPost]
		public ActionResult Index(LoginViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (model.ValidCode != Session["ValidateCode"].ToString())
				{
					ViewBag.LoginError = "验证码错误！";
					return View(model);
				}

				var result = new UserHelper().Login(model.UserID, model.Password, model.IsMemberMe);

				if (result.Tag == 1)
				{
					if (!string.IsNullOrEmpty(returnUrl))
					{
						return Redirect(returnUrl.ToString());
					}

					return RedirectToAction("Index", "Home");
				}
				else
				{
					ViewBag.LoginError = result.Message;
					return View(model);
				}
			}
			else
			{
				return View(model);
			}
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();

			return Redirect("/");
		}

	}
}
