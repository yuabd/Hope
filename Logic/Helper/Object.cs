using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hammer.Logic.Helper
{
	public class KeyName
	{
		public int ID { set; get; }

		public string Name { set; get; }

		//public string Pic { set; get; }
	}

	public class KeyValue
	{
		public KeyValue()
		{

		}
		public KeyValue(string key, string value)
		{
			Key = key;
			Value = value;
		}
		public string Key { get; set; }
		public string Value { get; set; }
	}
	public class KeyValue<T>
	{
		public string Key { get; set; }
		public string Value { get; set; }
		public T Main { get; set; }
	}

	public class GetReportDataParams : ReportDataParamsBase
	{
		/// <summary>
		/// 报表名称
		/// </summary>

		public string ReportName { get; set; }
		/// <summary>
		/// 排序字段
		/// </summary>

		public string Order { get; set; }

		public int UserID { get; set; }

		public GetReportDataParams()
		{
			this.PageSize = 10;
			this.Where = new List<KeyValue>();
		}
	}

	public class GetReportExportDataParams : ReportDataParamsBase
	{

		public string ReportName { get; set; }

		public string Order { get; set; }

		public int UserID { get; set; }
	}
	public class ReportDataParamsBase
	{
		/// <summary>
		/// 页面大小
		/// </summary>

		public int PageSize { get; set; }
		/// <summary>
		/// 页面索引
		/// </summary>

		public int PageIndex { get; set; }

		public List<KeyValue> Where { get; set; }
	}
}
