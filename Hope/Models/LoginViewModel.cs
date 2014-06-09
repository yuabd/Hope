using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Hope.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "必填")]
		public string UserID { get; set; }

		[Required(ErrorMessage = "必填")]
		public string Password { get; set; }

		public string ValidCode { get; set; }

		public bool IsMemberMe { get; set; }

		public LoginViewModel()
		{
		}
	}
}