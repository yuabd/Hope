using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hammer.Logic.Models;
using Hammer.Logic.Helper;
using Hammer.Logic.DataAccess;
using Hammer.Logic.Entity;

namespace Hammer.Logic
{
	public class WishLogic : DbAccess
	{
		public WishLogic(SiteDataContext db)
			: base(db)
		{
		}

		public string GetStatusName(int i)
		{
			var str = "";
			switch (i)
			{
				case 0: str = "待审核"; break;
				case 1: str = "待认领"; break;
				case 2: str = "进行中"; break;
				case 3: str = "完成"; break;
				default:
					break;
			}
			return str;
		}

		public List<WishList> GetSiteWishList()
		{
			var list = (from l in db.Wishes
						where l.Status != WishStatus.WaitAudit
						select new WishList()
						{
							ApplyUser = db.Users.FirstOrDefault(m => m.ID == l.ApplyUserID).UserName,
							Count = l.Count,
							DateStart = l.DateStart,
							ID = l.ID,
							UserName = db.Users.FirstOrDefault(m => m.ID == l.UserID).UserName,
							StatusID = l.Status,
							Support = l.Support,
							Title = l.Title,
							ApplyUserID = l.ApplyUserID,
							UserID = l.UserID,
							PictureFile = l.PictureFile,
							Name = l.Name
						}).ToList();

			list.ForEach(m => m.Status = GetStatusName(m.StatusID));

			return list;
		}


		public List<Wish> GetWishList(int? status, string title)
		{
			var list = from l in db.Wishes
					   where l.Status != WishStatus.WaitAudit
					   select l;

			if (!string.IsNullOrEmpty(title))
			{
				list = from l in list
					   where l.Title.Contains(title)
					   select l;
			}

			if (status != null && status > 0)
			{
				list = from l in list
					   where l.Status == status
					   select l;
			}

			return list.ToList();
		}

		public BaseObject QuitWish(int id)
		{
			var obj = new BaseObject();


			try
			{
				var wish = db.Wishes.FirstOrDefault(m => m.ID == id);
				if (wish == null)
				{
					obj.Tag = -1;
					obj.Message = "该心愿不能放弃!";

					return obj;
				}
				var user = db.Users.FirstOrDefault(m => m.ID == wish.ApplyUserID);
				user.Heart -= 1;
				wish.ApplyUserID = 0;
				wish.Status = WishStatus.Normal;

				db.SaveChanges();
				obj.Tag = 1;

			}
			catch (Exception e)
			{
				obj.Tag = -1;
				obj.Message = e.Message;
			}

			return obj;
		}

		public HomeTotal GetHomeTotal()
		{
			var result = new HomeTotal();
			var wishs = db.Wishes;
			//总心愿数
			result.wish_num = wishs.Count();
			//许愿人数
			result.weaker_num = wishs.Select(m => m.UserID).Distinct().Count();
			//已完成心愿
			result.finished = wishs.Where(m => m.Status == WishStatus.Done).Count();
			//献爱心人数
			result.give_num = wishs.Select(m => m.ApplyUserID).Distinct().Count();
			//未完成心愿数
			result.unfinished = wishs.Where(m => m.Status != WishStatus.Done && m.Status != WishStatus.WaitAudit).ToList();

			var users = (from l in db.Users
						 orderby l.Heart descending
						 select new
						 {
							 Heart = l.Heart,
							 ID = l.ID,
							 UserName = l.UserName
						 }).Take(10).ToList();
			var list = from l in users
					   select new KeyValue<int>()
					   {
						   Key = l.Heart.UString(),
						   Main = l.ID,
						   Value = l.UserName
					   };



			result.ListPeople = list.ToList();

			result.News = db.News.Where(m => m.CategoryID == 1).OrderByDescending(m => m.DateCreated).Take(5).ToList();

			return result;
		}


		public List<WishList> GetWishReport(GetReportDataParams param, out int totalCount)
		{
			DataSet ds = MSSqlHelper.GetReportData("WishList", param, XMLID.Admin, out totalCount);
			var dt = ds.Tables[0];
			if (dt == null)
				return new List<WishList>();

			var article = (from l in dt.AsEnumerable()
						   select new WishList
										 {
											 ID = l.Field<int>("ID"),
											 Count = l.Field<int>("Count"),
											 DateStart = l.Field<DateTime?>("DateStart"),
											 Name = l.Field<string>("Name"),
											 Title = l.Field<string>("Title"),
											 Support = l.Field<int>("Support"),
											 StatusID = l.Field<int>("Status"),
											 UserName = l.Field<string>("AddUser"),
											 ApplyUser = l.Field<string>("ApplyUser"),
											 UserID = l.Field<int>("UserID"),
											 ApplyUserID = l.Field<int>("ApplyUserID"),
											 PictureFile = l.Field<string>("PictureFile"),
										 }).ToList();

			article.ForEach(m => m.Status = GetStatusName(m.StatusID));

			return article;
		}

		public BaseObject AddWish(Wish wish)
		{
			var obj = new BaseObject();

			try
			{
				if (db.Wishes.Any(m => m.Title.Contains(wish.Title)))
				{
					obj.Tag = -2;
					obj.Message = "标题已存在!";
					return obj;
				}

				wish.Status = WishStatus.WaitAudit;
				wish.DateStart = DateTime.Now;
				wish.ApplyUserID = 0;

				db.Wishes.Add(wish);
				db.SaveChanges();

				obj.Tag = 1;

				var user = db.Users.FirstOrDefault(m => m.ID == wish.UserID);
				user.Heart += 1;

				db.SaveChanges();
			}
			catch (Exception e)
			{
				obj.Tag = -1;
				obj.Message = e.Message;
			}

			return obj;
		}

		public BaseObject ApplyWish(int id, int applyID)
		{
			var obj = new BaseObject();
			if (applyID == 0)
			{
				obj.Tag = -1;
				obj.Message = "认领失败,请联系管理员!";

				return obj;
			}

			try
			{
				var wish = db.Wishes.FirstOrDefault(m => m.ID == id);
				wish.ApplyUserID = applyID;
				wish.Status = WishStatus.AlreadyApply;

				var applyUser = db.Users.FirstOrDefault(m => m.ID == applyID);
				applyUser.Heart += 1;

				db.SaveChanges();
				obj.Tag = 1;
			}
			catch (Exception e)
			{
				obj.Tag = -1;
				obj.Message = e.Message;
			}

			return obj;
		}

		public WishEntity GetWishByID(int id)
		{
			var wish = db.Wishes.FirstOrDefault(m => m.ID == id);
			wish.Count += 1;
			db.SaveChanges();
			var wishEntity = new WishEntity()
			{
				ApplyUserID = wish.ApplyUserID,
				Count = wish.Count,
				DateStart = wish.DateStart,
				Description = wish.Description,
				Hope = wish.Hope,
				ID = wish.ID,
				Name = wish.Name,
				PictureFile = wish.PictureFile,
				Status = wish.Status,
				Support = wish.Support,
				Title = wish.Title,
				UserID = wish.UserID,
				WishDescription = wish.WishDescription
			};

			wishEntity.User = db.Users.FirstOrDefault(m => m.ID == wish.UserID);

			return wishEntity;
		}


		public BaseObject UpdateWish(Wish wish)
		{
			var obj = new BaseObject();
			try
			{
				if (db.Wishes.Any(m => m.Title.Equals(wish.Title) && m.ID != wish.ID))
				{
					obj.Tag = -2;
					obj.Message = "标题已存在!";
					return obj;
				}

				var w = db.Wishes.FirstOrDefault(m => m.ID == wish.ID);

				w.DateStart = DateTime.Now;
				w.Description = wish.Description;
				w.Hope = wish.Hope;
				w.Name = wish.Name;
				w.Title = wish.Title;
				w.WishDescription = wish.WishDescription;
				w.PictureFile = wish.PictureFile;

				db.SaveChanges();

				obj.Tag = 1;
			}
			catch (Exception e)
			{
				obj.Tag = -1;
				obj.Message = e.Message;
			}

			return obj;
		}

		public BaseObject DeleteWish(int id)
		{
			var obj = new BaseObject();
			try
			{
				var wish = db.Wishes.FirstOrDefault(m => m.ID == id);

				db.Wishes.Remove(wish);
				db.SaveChanges();

				obj.Tag = 1;
			}
			catch (Exception e)
			{
				obj.Tag = -1;
				obj.Message = e.Message;
			}

			return obj;
		}

		public BaseObject AuditWish(string ids)
		{
			var obj = new BaseObject();

			try
			{
				var list = ids.Split(';');
				foreach (var item in list)
				{
					var id = item.Uint();
					if (id != 0)
					{
						var wish = db.Wishes.FirstOrDefault(m => m.ID == id);

						if (wish == null)
						{
							continue;
						}
						wish.Status = WishStatus.Normal;
					}
				}

				db.SaveChanges();
				obj.Tag = 1;
			}
			catch (Exception e)
			{
				obj.Tag = -1;
				obj.Message = e.Message;
			}

			return obj;
		}

		public BaseObject TransferWish(int wishID, int userID)
		{
			var obj = new BaseObject();
			var wish = db.Wishes.FirstOrDefault(m => m.ID == wishID);

			if (wish == null)
			{
				obj.Tag = -1;
				return obj;
			}
			wish.ApplyUserID = userID;

			db.SaveChanges();
			obj.Tag = 1;

			return obj;
		}

		public void Support(int id, string type)
		{
			if (type.ToLower() == "wish")
			{
				var wish = db.Wishes.FirstOrDefault(m => m.ID == id);
				if (wish == null)
				{
					return;
				}
				wish.Support += 1;
				db.SaveChanges();
				return;
			}
			else if (type.ToLower() == "user")
			{
				var wish = db.Users.FirstOrDefault(m => m.ID == id);
				if (wish == null)
				{
					return;
				}
				wish.Support += 1;
				db.SaveChanges();
				return;
			}
		}

		public BaseObject MakeDone(int id)
		{
			var obj = new BaseObject();

			try
			{
				var wish = db.Wishes.FirstOrDefault(m => m.ID == id);
				if (wish == null)
				{
					obj.Tag = -1;
					obj.Message = "该记录不存在!";
					return obj;
				}

				wish.Status = WishStatus.Done;

				db.SaveChanges();
				obj.Tag = 1;

			}
			catch (Exception e)
			{
				obj.Tag = -1;
				obj.Message = e.Message;
			}

			return obj;
		}
	}
}
