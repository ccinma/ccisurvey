using ccisurvey.data.Models;
using ccisurvey.data.Repositories;
using ccisurvey.services.VMs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ccisurvey.services
{
	public class AuthService : IAuthService
	{
		private readonly HttpContext _httpContext;
		private readonly IUserRepository _repo;
		private readonly IPasswordService _hasher;
		public AuthService(IHttpContextAccessor contextAccessor, IUserRepository repo, IPasswordService hasher)
		{
			_httpContext = contextAccessor.HttpContext;
			_repo = repo;
			_hasher = hasher;
		}

		public async Task<bool> Register(User user)
		{
			var hash = _hasher.HashPassword(user.Password);

			user.Password = hash;

			await _repo.AddAsync(user);
			return true;
		}

		public async Task<bool> CheckEmailExists(string email)
		{
			var result = await _repo.GetByEmailAsync(email);
			return (result == null) ? false : true;
		}

		public async Task<bool> Login(LoginViewModel model)
        {
			var user = await _repo.GetByEmailAsync(model.Email);
			if (user != null)
            {
				if(_hasher.VerifyHash(user.Password, model.Password))
                {
					var claims = new List<Claim>()
					{
						new Claim("Name", user.Name),
						new Claim("Email", user.Email),
					};
					var identity = new ClaimsIdentity(claims);
					var principal = new ClaimsPrincipal(identity);
					await AuthenticationHttpContextExtensions.SignInAsync(_httpContext, principal);

					return true;
                }
				else
                {
					return false;
                }
            }
            else
            {
				return false;
            }
		}

		public async Task Logout()
        {
			await _httpContext.SignOutAsync();
        }
	}
}
