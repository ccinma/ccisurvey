using System;
using ccisurvey.data.Models;
using Microsoft.EntityFrameworkCore;

namespace ccisurvey.data
{
	public class AppDBContext : DbContext
	{
		public DbSet<User> User { get; set; }
		public DbSet<Survey> Survey { get; set; }
		public DbSet<Proposition> Proposition { get; set; }


		public AppDBContext(DbContextOptions options) : base(options)
		{

		}

	}
}
