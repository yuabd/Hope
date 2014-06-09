using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hammer.Logic.Models;

namespace Hammer.Logic.DataAccess
{
	public abstract class DbAccess : IDisposable
	{
		protected SiteDataContext db { get; set; }

		public DbAccess(SiteDataContext _db = null)
		{
			if (db == null)
			{
				db = new SiteDataContext();
			}
			else
			{
				db = _db;
			}
		}


		#region IDisposable 成员

		public void Dispose()
		{
			db.Database.Connection.Close();
			db.Dispose();
			db = null;
		}

		#endregion
	}
}