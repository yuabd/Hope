using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Hammer.Logic.Models
{
	public class User
	{
		public int ID { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string RealName { get; set; }

		public string Phone { get; set; }

		public string Description { get; set; }

		public DateTime DateStart { get; set; }

		public DateTime LastLoginDate { get; set; }

		public int Heart { get; set; }

		public string HeadPicture { get; set; }

		public int Support { get; set; }
	}

	public class UserRole
	{
		public int ID { get; set; }

		public string RoleName { get; set; }
	}

	public class UserRoleJoin
	{
		public int ID { get; set; }

		public int UserID { get; set; }

		public int UserRoleID { get; set; }
	}

	public class UserEntity
	{
		public int ID { get; set; }

		public int Publish { get; set; }

		public string UserName { get; set; }

		public int Apply { get; set; }

		public string RealName { get; set; }

		public string Phone { get; set; }

		public string Description { get; set; }

		public int Heart { get; set; }

		public string HeadPicture { get; set; }

		public int Support { get; set; }
	}
}