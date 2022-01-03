using ccisurvey.data.Models;
using ccisurvey.services;
using ccisurvey.services.VMs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ccisurvey.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _auth;
		public AuthController(IAuthService service)
		{
			_auth = service;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
		{
			var errors = new List<string>();
			var user = new User()
			{
				Name = model.Name,
				Email = model.Email,
				Password = model.Password
			};

			if (ModelState.IsValid)
			{
				var emailExists = await _auth.CheckEmailExists(user);

				if (!emailExists)
				{
					await _auth.Register(user);
					return (string.IsNullOrEmpty(returnUrl) ? Redirect("/home") : Redirect(returnUrl));
				} else
				{
					errors.Add("Email déjà existante.");
				} 

			} else
			{
				errors.Add("Les champs suivis d'une * sont obligatoires.");
			}

			ViewData["Errors"] = errors;
			return View();
		}
	}
}
