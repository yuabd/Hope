using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hammer.Logic.Helper
{
	public class ReportHelp
	{
		public ReportHelp()
		{

		}
		/// <summary>
		/// 报表统计
		/// </summary>
		/// <param name="reportName"> 对应的名称 </param>
		/// <param name="systemID"> 系统ID</param>
		/// <param name="where"> 查询条件 </param>
		/// <param name="type"> 报表路径 </param>
		/// <param name="Totalstartsql"> Sql头(select order ,count(*) as total from ) </param>
		/// <param name="Totalendsql"> Sql(group by order)</param>
		/// <returns></returns>
		public static DataSet GetReportTotal(string reportName, List<KeyValue> where, int systemID, bool optimize = false)
		{
			if (where != null)
			{
				foreach (var item in where)
				{
					item.Value = item.Value.Trim();
				}
			}

			return MSSqlHelper.GetReportTotal(reportName, where, systemID);
		}
		/// <summary>
		/// 前台需要点击排序的时候调用
		/// </summary>
		/// <param name="reportName"> XML对?应?|的??名?称? </param>
		/// <param name="where"> 查??询??条??件t </param>
		/// <param name="pageSize"> 页?3面?打???小? </param>
		/// <param name="pageIndex"> 页?3面?索??引?y </param>
		/// <param name="order"></param>
		/// <param name="systemID"> 系??统?3ID</param>
		/// <param name="totalCount"></param>
		/// <returns></returns>
		public static DataSet GetReportData(string reportName, GetReportDataParams param,
			int systemID, out int totalCount, bool optimize = false)
		{
			if (param.Where != null)
			{
				foreach (var item in param.Where)
				{
					item.Value = item.Value.Trim();
				}
			}
			DataSet ds;
			
			ds = MSSqlHelper.GetReportData(reportName, param, systemID, out totalCount);

			return ds;

		}

		private static string ForSql(string sql, string order, int start, int end)
		{
			sql = @" SELECT * FROM ( SELECT Row_Number() OVER ({0}) row, * from ( select * FROM (" + sql + " )  tt) t ) item "
					 + " WHERE item.row BETWEEN " + start + " AND " + end + " ";

			sql = string.Format(sql, order);
			return sql;
		}
	}


	public enum ReportConnectionType
	{
		Business = 1,
		Log = 2
	}

	public class XMLID
	{
		public static int SuperAdmin = 1;  //超级管理员
		public static int Admin = 2;   //管理员
		public static int Log = 4;   //日志 备用
	}
}
