using ccisurvey.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
    }
}
