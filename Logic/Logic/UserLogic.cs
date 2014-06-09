using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hammer.Logic.Helper;
using Hammer.Logic.DataAccess;
using Hammer.Logic.Models;
using System.Web;
using System.Web.Security;
using System.Data;

namespace Hammer.Logic
{
	public class UserLogic : DbAccess
	{
		public BaseObject Login(string userName, string clearPassword, bool rememberMe)
		{
			BaseObject obj = new BaseObject();

			var user = db.Users.FirstOrDefault(m => m.UserName == userName);
			if (user == null)
			{
				obj.Tag = -1;
				obj.Message = "用户名或密码错误!";

				return obj;
			}
			if (UserAuthenticated(user.ID, clearPassword))
			{
				//var user = GetUser(userID);

				string roles = GetUserRoles(user.ID);

				GenerateAuthenticationTicket(user.ID, rememberMe, roles);
				HttpContext.Current.Session["UserName"] = user.UserName;
				//HttpContext.Current.Response.Cookies.Add(cookies);

				user.LastLoginDate = DateTime.Now;

				db.SaveChanges();

				obj.Tag = 1;
			}
			else
			{
				obj.Tag = -1;
				obj.Message = "用户名或密码错误!";
			}

			return obj;
		}

		public BaseObject Register(User user)
		{
			BaseObject obj = new BaseObject();

			if (db.Users.Any(m => m.UserName == user.UserName))
			{
				obj.Tag = -1;
				obj.Message = "用户名已经存在! ";

				return obj;
			}

			var roles = new string[] { "2" };

			InsertUser(user, roles);

			db.SaveChanges();

			obj.Tag = 1;

			return obj;
		}

		public BaseObject EditProfile(User user)
		{
			var obj = new BaseObject();

			var u = GetUser(user.ID);
			if (u == null)
			{
				obj.Tag = -1;
				obj.Message = "用户不存在！";

				return obj;
			}

			if (db.Users.Any(m => m.UserName.Contains(user.UserName)))
			{
				obj.Tag = -1;
				obj.Message = "用户名已存在!";
			}

			u.HeadPicture = user.HeadPicture;
			u.Description = user.Description;
			u.RealName = user.RealName;
			u.Phone = user.Phone;
			u.UserName = user.UserName;

			if (!string.IsNullOrEmpty(user.Password))
			{
				var newPassword = EncryptPassword(user.Password);
				u.Password = newPassword;
			}

			db.SaveChanges();
			obj.Tag = 1;

			return obj;
		}

		private bool UserAuthenticated(int userID, string clearPassword)
		{
			string encryptedPassword = EncryptPassword(clearPassword);

			var r = GetUser(userID, encryptedPassword);

			return r != null;
		}

		public BaseObject ChangePassword(int userID, string clearNewPassword)
		{
			var obj = new BaseObject();

			var user = GetUser(userID);

			if (user == null)
			{
				obj.Tag = -1;
				obj.Message = "未登陆或用户不存在！";

				return obj;
			}

			user.Password = EncryptPassword(clearNewPassword);
			obj.Tag = 1;

			return obj;
		}

		public string EncryptPassword(string clearPassword)
		{
			return FormsAuthentication.HashPasswordForStoringInConfigFile(clearPassword, "MD5");
		}

		private static void GenerateAuthenticationTicket(int userID, bool rememberMe, string roles)
		{
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
			   1, // Ticket version
			   userID.ToString(), // Username associated with ticket
			   DateTime.Now, // Date/time issued
			   DateTime.Now.AddDays(30), // Date/time to expire
			   rememberMe, // persistent user cookie
			   roles, // User-data, in this case the roles
			   FormsAuthentication.FormsCookiePath);// Path cookie valid for Encrypt the cookie using the machine key for secure transport

			string hash = FormsAuthentication.Encrypt(ticket);
			HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

			cookie.Expires = DateTime.Now.AddHours(12);

			if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

			// Add the cookie to the list for outgoing response
			HttpContext.Current.Response.Cookies.Add(cookie);
		}

		// Repository functions

		public User GetUser(int userID)
		{
			return db.Users.Find(userID);
		}

		public string GetUserRoles(int userID)
		{
			var roleList = "";
			var roles = from u in db.UserRoleJoins where u.UserID == userID select u;

			foreach (var item in roles)
			{
				roleList += item.UserRoleID + ",";
			}

			if (!string.IsNullOrEmpty(roleList))
				roleList = roleList.Substring(0, roleList.Length - 1);	// remove last comma (,) symbol

			return roleList;
		}

		public User GetUser(int userID, string encryptedPassword)
		{
			return db.Users.SingleOrDefault(u => u.ID == userID && u.Password == encryptedPassword);
		}

		public void InsertUser(User user, string[] roles)
		{
			if (!string.IsNullOrEmpty(user.Password))
			{
				user.DateStart = DateTime.Now;
				user.LastLoginDate = DateTime.Now;
				user.Password = EncryptPassword(user.Password);
				user.Heart = 0;
				
				db.Users.Add(user);

				db.SaveChanges();

				foreach (var item in roles)
				{
					var roleID = Convert.ToInt32(item);

					UserRoleJoin userRoleJoin = new UserRoleJoin();

					userRoleJoin.UserRoleID = roleID;

					userRoleJoin.UserID = user.ID;

					db.UserRoleJoins.Add(userRoleJoin);
				}
			}
		}

		public List<User> GetUserList(GetReportDataParams param, out int totalCount)
		{
			DataSet ds = MSSqlHelper.GetReportData("UserList", param, XMLID.Admin, out totalCount);
			var dt = ds.Tables[0];
			if (dt == null)
				return new List<User>();

			var list = (from l in dt.AsEnumerable()
						select new User
						   {
							   ID = l.Field<int>("ID"),
							   //Count = l.Field<int>("Count"),
							   DateStart = l.Field<DateTime>("DateStart"),
							   //DateEnd = l.Field<DateTime?>("DateEnd"),
							   Description = l.Field<string>("Description"),
							   Support = l.Field<int>("Support"),
							   Phone = l.Field<string>("Phone"),
							   HeadPicture = l.Field<string>("HeadPicture"),
							   UserName = l.Field<string>("UserName"),
							   LastLoginDate = l.Field<DateTime>("LastLoginDate"),
							   Heart = l.Field<int>("Heart")
						   }).ToList();

			return list;
		}

		public BaseObject DeleteUser(int id)
		{
			var obj = new BaseObject();

			var user = db.Users.Find(id);
			if (user == null)
			{
				obj.Tag = -1;
				return obj;
			}

			db.Users.Remove(user);
			db.SaveChanges();

			return obj;
		}

		public UserEntity GetUserEntity(int id)
		{
			var user = db.Users.FirstOrDefault(m => m.ID == id);
			var userEntity = new UserEntity();
			if (user == null)
			{
				return userEntity;
			}

			var wish = new WishLogic(db).GetSiteWishList();
			userEntity.Apply = wish.Where(m => m.ApplyUserID == id).ToList().Count;
			userEntity.Publish = wish.Where(m => m.UserID == id).ToList().Count;
			userEntity.Description = user.Description;
			userEntity.ID = user.ID;
			userEntity.RealName = user.RealName;
			userEntity.HeadPicture = user.HeadPicture;
			userEntity.Heart = user.Heart;
			userEntity.Phone = user.Phone;
			userEntity.Support = user.Support;
			userEntity.UserName = user.UserName;

			return userEntity;
		}
	}
}
