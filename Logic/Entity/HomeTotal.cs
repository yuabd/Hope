using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hammer.Logic.Helper;
using Hammer.Logic.Models;

namespace Hammer.Logic.Entity
{
	public class HomeTotal
	{
		public int wish_num { get; set; }

		public int weaker_num { get; set; }

		public int finished { get; set; }

		public int give_num { get; set; }

		public List<Wish> unfinished { get; set; }

		public List<KeyValue<int>> ListPeople { get; set; }

		public List<News> News { get; set; }
	}
}
