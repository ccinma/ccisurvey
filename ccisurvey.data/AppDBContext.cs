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


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany(u => u.Surveys)
				.WithOne(s => s.User)
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Survey>()
				.HasMany(s => s.Propositions)
				.WithOne(p => p.Survey)
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Proposition>()
				.HasMany(p => p.Participants)
				.WithMany(u => u.Propositions);
			//modelBuilder.Entity<Survey>()
			//	.Ignore(s => s.Participants);
		}

		public AppDBContext(DbContextOptions options) : base(options)
		{

		}

	}
}
