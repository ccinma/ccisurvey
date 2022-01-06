using ccisurvey.data.Models;
using ccisurvey.services.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ccisurvey.services
{
	public interface IAuthService
	{
		Task<bool> Register(User user);
		Task<bool> CheckEmailExists(string email);
		Task<bool> Login(LoginViewModel model);
		Task Logout();
	}
}
