using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hammer.Logic.Models
{
	public class News
	{
		public int ID { get; set; }

		public int CategoryID { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public DateTime DateCreated { get; set; }

		public int Count { get; set; }

		public int SortOrder { get; set; }
	}

	public class Category
	{
		public int ID { get; set; }

		public string CategoryName { get; set; }

		public int SortOrder { get; set; }
	}
}
