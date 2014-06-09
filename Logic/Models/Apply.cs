using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Hammer.Logic.Models
{
	public class Apply
	{
		[Key]
		[ForeignKey("Wish")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int WishID { get; set; }
		[MaxLength(20)]
		[Required]
		public string ContactName { get; set; }
		[Required]
		[MaxLength(100)]
		public string ContactAddress { get; set; }
		//[Required]
		//[RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessage = "格式错误")]
		//public string ContactEmail { get; set; }
		[MaxLength(100)]
		[Required]
		public string ContactTel { get; set; }
		[MaxLength(500)]
		public string Message { get; set; }
		public DateTime Date { get; set; }

		public virtual Wish Wish { get; set; }
	}

	public class Menu
	{
		public int ID { get; set; }
		/// <summary>
		/// 菜单名称
		/// </summary>
		public string MenuName { get; set; }
		/// <summary>
		/// 父菜单ID
		/// </summary>
		public int? ParentID { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public string Enable { get; set; }
		/// <summary>
		/// 是否被默认选中
		/// </summary>
		public string Selected { get; set; }

		public int? SystemID { set; get; }
		/// <summary>
		/// 排序字段
		/// </summary>
		public int? OrderIndex { set; get; }
	}

	public class Page
	{
		public int ID { get; set; }
		public int MenuID { get; set; }
		[MaxLength(100)]
		public string PageName { get; set; }
		public string Enable { get; set; }
		public string PageUrl { get; set; }
		public int? OrderIndex { get; set; }
		public string Selected { get; set; }
		public int? SystemID { set; get; }
	}
}