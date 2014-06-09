using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Hammer.Logic.Helper
{
	public class ConfigContext
	{
		public static string SuperAdminReportPath = HttpContext.Current.Server.MapPath("~/bin/Report/SuperAdminReport.xml");
		public static string AdminReportPath = HttpContext.Current.Server.MapPath("~/bin/Report/AdminReport.xml");

		public static string ReportConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["SiteDataContext"].ConnectionString;
	}
}
