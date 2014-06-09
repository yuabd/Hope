using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hammer.Entity;
using Hammer.Logic;
using Hammer.Logic.DataAccess;

namespace Hope.Helpers
{
	public class SystemHelper : DbAccess
	{
		public List<MenuTree> GetTreeMenu(int? systemID)
		{
			using (MenuLogic logic = new MenuLogic(db))
			{
				return logic.GetTreeMenu(systemID);
			}
		}
	}
}