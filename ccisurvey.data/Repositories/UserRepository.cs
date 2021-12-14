using ccisurvey.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Repositories
{
	public class UserRepository
    {
		private readonly AppDBContext _db;
		public async Task<User> GetAsync(int id)
		{
			return await _db.Set<User>().FindAsync(id);
		}

		public async Task<User> AddAsync(User user)
		{
			_db.Set<User>().Add(user);
			await _db.SaveChangesAsync();
			return user;
		}

		public async Task<User> UpdateAsync(User user)
		{
			_db.Entry(user).State = EntityState.Modified;
			await _db.SaveChangesAsync();
			return user;
		}
	}
}
