using ccisurvey.data.Models;
using ccisurvey.data.Repositories;
using ccisurvey.services;
using ccisurvey.services.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace ccisurvey.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _auth;
		private readonly IUserRepository _urepo;
		public AuthController(IAuthService service, IUserRepository urepo)
		{
			_auth = service;
			_urepo = urepo;
		}

		[HttpGet]
		public IActionResult Register(string returnUrl)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
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
				var emailExists = await _auth.CheckEmailExists(model.Email);

				if (!emailExists)
				{
					await _auth.Register(user);
					return string.IsNullOrEmpty(model.ReturnUrl) ? Redirect("/auth/login") : Redirect($"/auth/login?ReturnUrl={model.ReturnUrl}");
				} else
				{
					var participant = await _urepo.GetByEmailAsync(model.Email);

					//if (!participant.IsApprouved)
					//{
					//	ViewData["User"] = participant;
					//	return View("AlreadyUsedMail");
					//}

					errors.Add("Email déjà existante.");
				} 

			} else
			{
				errors.Add("Les champs suivis d'une * sont obligatoires.");
			}

			ViewData["Errors"] = errors;
			return View();
		}

		[HttpGet]
		public IActionResult Login(string returnUrl)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
            {
				var result = await _auth.Login(model);

				if (result == true)
                {
					return string.IsNullOrEmpty(model.ReturnUrl) ? Redirect("/home") : Redirect(model.ReturnUrl);
                }
            }
			return View();
		}

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			if (HttpContext.User != null)
			{
				await _auth.Logout();
			}
			return Redirect("/auth/login");
		}
	}
}
