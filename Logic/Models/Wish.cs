using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Hammer.Logic.Models
{
	public class Wish
	{
		public int ID { get; set; }
		/// <summary>
		/// 许愿人名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 许愿人简介
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 愿望介绍
		/// </summary>
		public string WishDescription { get; set; }
		/// <summary>
		/// 愿望标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime? DateStart { get; set; }
		/// <summary>
		/// 发起人寄语
		/// </summary>
		public string Hope { get; set; }
		/// <summary>
		/// 浏览量
		/// </summary>
		public int Count { get; set; }
		/// <summary>
		/// 赞数
		/// </summary>
		public int Support { get; set; }
		/// <summary>
		/// 发起人ID
		/// </summary>
		public int UserID { get; set; }

		public string PictureFile { get; set; }

		public int ApplyUserID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
		public int Status { get; set; }

	}

	public class WishList
	{
		public int ID { get; set; }
		/// <summary>
		/// 愿望标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime? DateStart { get; set; }
		/// <summary>
		/// 浏览量
		/// </summary>
		public int Count { get; set; }
		/// <summary>
		/// 赞数
		/// </summary>
		public int Support { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 发起人ID
		/// </summary>
		public int UserID { get; set; }

		//public string IsAudit { get; set; }
		/// <summary>
		/// 发起人名称
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// 发起人ID
		/// </summary>
		public int ApplyUserID { get; set; }

		public string ApplyUser { get; set; }

		public int StatusID { get; set; }

		public string Status { get; set; }

		public string PictureFile { get; set; }

	}

	public class WishEntity
	{
		public int ID { get; set; }
		/// <summary>
		/// 许愿人名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 许愿人简介
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 愿望介绍
		/// </summary>
		public string WishDescription { get; set; }
		/// <summary>
		/// 愿望标题
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime? DateStart { get; set; }
		/// <summary>
		/// 发起人寄语
		/// </summary>
		public string Hope { get; set; }
		/// <summary>
		/// 浏览量
		/// </summary>
		public int Count { get; set; }
		/// <summary>
		/// 赞数
		/// </summary>
		public int Support { get; set; }
		/// <summary>
		/// 发起人ID
		/// </summary>
		public int UserID { get; set; }

		public string PictureFile { get; set; }

		public int ApplyUserID { get; set; }

		public int Status { get; set; }

		public User User { get; set; }
	}
}