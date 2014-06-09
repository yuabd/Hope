using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hammer.Logic.Models;
using Hammer.Logic.DataAccess;
using Hammer.Logic;
using Hammer.Logic.Helper;

namespace Hope.Helpers
{
	public class UserHelper
	{
		public static string _userName;
		public static string UserName 
		{
			get
			{
				if (string.IsNullOrEmpty(_userName))
				{
					return HttpContext.Current.Session["UserName"].UString();
				}

				return _userName;
			}
		}

		public BaseObject Login(string userName, string clearPassword, bool rememberMe)
		{
			using (UserLogic logic = new UserLogic())
			{
				var result = logic.Login(userName, clearPassword, rememberMe);
				if (result.Tag == 1)
				{
					_userName = HttpContext.Current.Session["UserName"].UString();
				}
				return result;
			}
		}

		public BaseObject Register(User user)
		{
			using (UserLogic logic = new UserLogic())
			{
				return logic.Register(user);
			}
		}

		public UserEntity GetUserEntity(int id)
		{
			using (UserLogic logic = new UserLogic())
			{
				return logic.GetUserEntity(id);
			}
		}

		public List<User> GetUserList(GetReportDataParams param, out int totalCount)
		{
			using (UserLogic logic = new UserLogic())
			{
				return logic.GetUserList(param, out totalCount);
			}
		}

		public BaseObject EditProfile(User user)
		{
			using (UserLogic logic = new UserLogic())
			{
				return logic.EditProfile(user);
			}
		}

		public User GetUser(int userID)
		{
			using (UserLogic logic = new UserLogic())
			{
				return logic.GetUser(userID);
			}
		}

		public BaseObject DeleteUser(int id)
		{
			using (UserLogic logic = new UserLogic())
			{
				return logic.DeleteUser(id);
			}
		}
	}
}