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

		public async Task<bool> CheckEmailExists(User user)
		{
			var result = await _repo.GetByEmailAsync(user.Email);
			return (result == null) ? false : true;
		}
	}
}
