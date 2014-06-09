using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Hammer.Logic.Helper;
using Hammer.Logic.DataAccess;
using Hammer.Logic.Models;

namespace Hammer.Logic
{
	public class NewsLogic : DbAccess
	{
		public List<News> GetNewsList(GetReportDataParams param, out int totalCount)
		{
			DataSet ds = MSSqlHelper.GetReportData("NewsList", param, XMLID.Admin, out totalCount);
			var dt = ds.Tables[0];
			if (dt == null)
				return new List<News>();

			var article = (from l in dt.AsEnumerable()
						   select new News
						   {
							   ID = l.Field<int>("ID"),
							   Count = l.Field<int>("Count"),
							   Title = l.Field<string>("Title"),
							   Content = l.Field<string>("Content"),
							   SortOrder = l.Field<int>("SortOrder"),
							   DateCreated = l.Field<DateTime>("DateCreated")
						   }).ToList();
			return article;
		}

		public News GetNewsByID(int id)
		{
			var news = db.News.Find(id);
			news.Count += 1;
			db.SaveChanges();

			return news;
		}

		public BaseObject InsertNews(News news)
		{
			var obj = new BaseObject();
			if (db.News.Any(m => m.Title == news.Title))
			{
				obj.Tag = -1;
				obj.Message = "标题重复!";
				return obj;
			}
			try
			{
				news.DateCreated = DateTime.Now;
				db.News.Add(news);
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

		public BaseObject UpdateNews(News news)
		{
			var obj = new BaseObject();

			var n = db.News.FirstOrDefault(m => m.ID == news.ID);
			try
			{
				n.Content = news.Content;
				n.DateCreated = DateTime.Now;
				n.SortOrder = news.SortOrder;
				n.Title = news.Title;

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

		public BaseObject DeleteNews(int id)
		{
			var obj = new BaseObject();

			try
			{
				var news = db.News.FirstOrDefault(m => m.ID == id);

				db.News.Remove(news);

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
