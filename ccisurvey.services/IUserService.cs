using ccisurvey.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.services
{
	public interface IUserService
	{
		Task<User> GetById(int id);
	}
}
