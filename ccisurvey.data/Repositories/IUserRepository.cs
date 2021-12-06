using ccisurvey.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.data.Repositories
{
    internal interface IUserRepository
    {
        Task<User> GetAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
