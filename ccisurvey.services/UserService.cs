using ccisurvey.data.Models;
using ccisurvey.data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _repo;
		public UserService(IUserRepository repo)
		{
			_repo = repo;
		}
		public async Task<User> GetById(int id)
		{
			var user = await _repo.GetAsync(id);
			return user;
		}
	}
}
