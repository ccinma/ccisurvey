using ccisurvey.data;
using ccisurvey.data.Repositories;
using ccisurvey.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ccisurvey
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDBContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("AppDBContext"));
			});

			services.AddHttpContextAccessor();

			services.AddAuthentication("Cookie")
				.AddCookie("Cookie", config =>
				{
					config.LoginPath = "/auth/login";
					config.LogoutPath = "/auth/logout";
					config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
					config.SlidingExpiration = true;
					config.Cookie.IsEssential = true;
				});

			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IPropositionRepository, PropositionRepository>();
			services.AddScoped<ISurveyRepository, SurveyRepository>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IPasswordService, PasswordService>();

			services.AddControllersWithViews().AddRazorRuntimeCompilation();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetRequiredService<AppDBContext>();
				context.Database.Migrate();
			}

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}