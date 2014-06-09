using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hammer.Logic.Helper
{
	public class Enum
	{
	}

	public static class PublicType
	{
		public const string Yes = "Y";
		public const string No = "N";
	}

	public static class WishStatus
	{
		/// <summary>
		/// 待审核
		/// </summary>
		public const int WaitAudit = 0;
		/// <summary>
		/// 待认领
		/// </summary>
		public const int Normal = 1;
		/// <summary>
		/// 进行中
		/// </summary>
		public const int AlreadyApply = 2;
		/// <summary>
		/// 已结束
		/// </summary>
		public const int Done = 3;
	}
}
