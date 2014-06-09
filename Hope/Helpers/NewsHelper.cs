using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hammer.Logic.Helper;
using Hammer.Logic.Models;
using Hammer.Logic;

namespace Hope.Helpers
{
	public class NewsHelper
	{
		public List<News> GetNewsList(GetReportDataParams param, out int totalCount)
		{
			using (NewsLogic logic = new NewsLogic())
			{
				return logic.GetNewsList(param, out totalCount);
			}
		}

		public News GetNewsByID(int id)
		{
			using (NewsLogic logic = new NewsLogic())
			{
				return logic.GetNewsByID(id);
			}
		}

		public BaseObject InsertNews(News news)
		{
			using (NewsLogic logic = new NewsLogic())
			{
				return logic.InsertNews(news);
			}
		}

		public BaseObject UpdateNews(News news)
		{
			using (NewsLogic logic = new NewsLogic())
			{
				return logic.UpdateNews(news);
			}
		}


		public BaseObject DeleteNews(int id)
		{
			using (NewsLogic logic = new NewsLogic())
			{
				return logic.DeleteNews(id);
			}
		}
	}
}