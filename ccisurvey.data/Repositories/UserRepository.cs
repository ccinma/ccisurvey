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
		public async Task<User> GetAsync(int id)
		{
			return await context.Set<int>().FindAsync(id);
		}

		public async Task<User> AddAsync(User user)
		{
			context.Set<User>().Add(user);
			await context.SaveChangesAsync();
			return user;
		}

		public async Task<User> UpdateAsync(User user)
		{
			context.Entry(user).State = EntityState.Modified;
			await context.SaveChangesAsync();
			return user;
		}
	}
}
