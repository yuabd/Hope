using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hammer.Logic.DataAccess
{
	public class Connection
	{
		public static String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SiteDataContext"].ConnectionString;
	}
}