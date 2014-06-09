using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Hammer.Logic.Helper
{
	public class Functions
	{
		private static Random Ran { get; set; }

		public static string GetRandomName(int max = 999)
		{
			return DateTime.Now.ToString("yyyyMMddhhmmss") + new Random().Next(max).ToString();
		}

		/// <summary>
		/// 获得IP
		/// </summary>
		/// <returns></returns>
		public static string GetIP()
		{
			string ip = string.Empty;
			if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
			{
				ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
			}
			else
			{
				ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
			}
			return ip;
		}

		/// <summary>
		/// 获得浏览器信息
		/// </summary>
		/// <returns></returns>
		public static string GetBrowser()
		{
			return HttpContext.Current.Request.Browser.Version.ToString();
		}

		/// <summary>
		/// 给url 添加参数
		/// </summary>
		/// <param name="url"></param>
		/// <param name="par"></param>
		/// <returns></returns>
		public static string AddUrlPar(string url, string par)
		{
			if (string.IsNullOrEmpty(url))
			{
				return "";
			}

			if (url.IndexOf("?") == -1)
			{
				url = url + "?" + par;
			}
			else
			{
				var paraString = url.Split('?')[1];
				var baseUrl = url.Split('?')[0];
				url = baseUrl + "?" + paraString + "&" + par;
			}

			return url;
		}

		/// <summary>
		/// 获取参数
		/// </summary>
		/// <param name="Request"></param>
		/// <returns></returns>
		public List<KeyValue> GetParam(HttpRequestBase Request)
		{
			List<KeyValue> where = new List<KeyValue>();

			//IReportDataParamsExtension.handleOperatorPermission(where);

			foreach (var item in Request.Form)
			{
				var str = item.ToString();
				if (str.ToLower() == "page" || str.ToLower() == "rows" || str.ToLower() == "order")
					continue;
				if (!string.IsNullOrEmpty(Request[str]))
				{
					where.Add(new KeyValue { Key = str, Value = Request[str].Trim() });
				}
			}

			return where;
		}

		/// <summary>
		/// 数字类型转金钱字符串。用户界面显示。截断办法。
		/// </summary>
		/// <param name="number">数字类型 eg：8569853259.5623</param>
		/// <returns>eg：8,569,853,259.56 </returns>
		public static string MoneyFormat(dynamic number)
		{
			if (number == null)
				return "";
			return Convert.ToDecimal(number).ToString("N");
		}

		/// <summary>
		/// 数字类型转金钱字符串。用户界面显示。截断办法。
		/// </summary>
		/// <param name="number">数字类型 eg：8569853259.5623</param>
		/// <returns>eg：8,569,853,259.56 </returns>
		public static string NumberFormat(dynamic number)
		{
			if (number == null)
				return "";
			return Convert.ToDecimal(number).ToString("0.00");
		}

		public static string NumberFormat(dynamic number, string df)
		{
			if (number == null)
				return df;
			return Convert.ToDecimal(number).ToString("0.00");
		}
		/// <summary>
		/// 时间格式化
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static string DateFormat(DateTime? date)
		{
			if (date == null)
				return string.Empty;

			return ((DateTime)date).ToString("yyyy-MM-dd");
		}

		public static string IntFormat(dynamic number)
		{
			if (number == null)
				return "0";
			if (Convert.ToInt32(number) == number)
			{
				return Convert.ToInt32(number).ToString("0");
			}
			else
			{
				return Convert.ToDecimal(number).ToString("0.00");
			}
		}

	}
}
