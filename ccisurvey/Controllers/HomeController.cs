using ccisurvey.data.Models;
using ccisurvey.data.Repositories;
using ccisurvey.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ccisurvey.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ISurveyRepository _srepo;
		private readonly IUserRepository _urepo;

		public HomeController(ILogger<HomeController> logger, ISurveyRepository srepo, IUserRepository urepo)
		{
			_logger = logger;
			_srepo = srepo;
			_urepo = urepo;
		}
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpGet]
		public async Task<IActionResult> MySurveys()
		{
			var claims = HttpContext.User.Claims.ToArray();
			var userId = Int32.Parse(claims[0].Value, NumberStyles.Integer);
			var user = await _urepo.GetAsync(userId);

			var allSurveys = await _srepo.GetAllByUserAsync(user);
			var createdSurveys = new List<Survey>();
			var participatingSurveys = new List<Survey>();

			foreach (var survey in allSurveys)
			{
				if (survey.User.Equals(user))
				{
					createdSurveys.Add(survey);
				}
				else
				{
					participatingSurveys.Add(survey);
				}
			}


			ViewData["Created"] = createdSurveys;
			ViewData["Participating"] = participatingSurveys;
			return View();

		}
	}
}
