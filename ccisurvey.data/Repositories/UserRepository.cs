using ccisurvey.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Repositories
{
	public class UserRepository : IUserRepository
    {
		private readonly AppDBContext _db;

		public UserRepository(AppDBContext db)
		{
			_db = db;
		}

		public async Task<User> GetAsync(int id)
		{
			return await _db.User.FindAsync(id);
		}

		public async Task<User> GetByEmailAsync(string email)
		{
			var user = await _db.User.FirstOrDefaultAsync(u => u.Email == email);
			return user;
		}

		public async Task<bool> AddAsync(User user)
		{
			await _db.User.AddAsync(user);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UpdateAsync(User user)
		{
			try
			{
				_db.Entry(user).State = EntityState.Modified;
				await _db.SaveChangesAsync();
				return true;
			} catch (Exception ex)
			{
				return false;
			}
		}
	}
}
