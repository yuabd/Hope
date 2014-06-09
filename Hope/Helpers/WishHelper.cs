using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hammer.Logic;
using Hammer.Logic.DataAccess;
using Hammer.Logic.Models;
using Hammer.Logic.Helper;
using Hammer.Logic.Entity;

namespace Hope.Helpers
{
	public class WishHelper : DbAccess
	{
		public BaseObject QuitWish(int id)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.QuitWish(id);
			}
		}
		public BaseObject MakeDone(int id)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.MakeDone(id);
			}
		}

		public void Support(int id, string type)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				logic.Support(id, type);
			}
		}

		public BaseObject ApplyWish(int id, int applyID)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.ApplyWish(id, applyID);
			}
		}

		public List<WishList> GetSiteWishList()
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.GetSiteWishList();
			}
		}

		public List<Wish> GetWishList(int? status, string title)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.GetWishList(status, title);
			}
		}

		public HomeTotal GetHomeTotal()
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.GetHomeTotal();
			}
		}

		public List<WishList> GetWishReport(GetReportDataParams param, out int totalCount)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.GetWishReport(param, out totalCount);
			}
		}

		public BaseObject AddWish(Wish wish)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.AddWish(wish);
			}
		}

		public WishEntity GetWishByID(int id)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.GetWishByID(id);
			}
		}

		public BaseObject UpdateWish(Wish wish)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.UpdateWish(wish);
			}
		}

		public BaseObject DeleteWish(int id)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.DeleteWish(id);
			}
		}

		public BaseObject AuditWish(string ids)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.AuditWish(ids);
			}
		}

		public BaseObject TransferWish(int wishID, int userID)
		{
			using (WishLogic logic = new WishLogic(db))
			{
				return logic.TransferWish(wishID, userID);
			}
		}
	}
}