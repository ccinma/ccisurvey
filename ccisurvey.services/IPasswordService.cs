using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography;
using Konscious.Security.Cryptography;

namespace ccisurvey.services
{
	public interface IPasswordService
	{
		string HashPassword(string password);
		public bool VerifyHash(string hashedPassword, string providedPassword);
	}
}
