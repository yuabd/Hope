using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Hammer.Logic.Models
{
	public class SiteDataContext: DbContext
	{
		public DbSet<Wish> Wishes { get; set; }
		public DbSet<Apply> Applies { get; set; }

		public DbSet<Menu> Menus { get; set; }
		public DbSet<Page> Pages { get; set; }

		public DbSet<User> Users { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<UserRoleJoin> UserRoleJoins { get; set; }

		public DbSet<News> News { get; set; }

		// Twist our database
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			base.OnModelCreating(modelBuilder);
		}

        //public class SiteDataContextInitializer : DropCreateDatabaseAlways<SiteDataContext>
        //{
        //    protected override void Seed(SiteDataContext context)
        //    {
        //        context.SaveChanges();
        //    }
        //}
	}
}