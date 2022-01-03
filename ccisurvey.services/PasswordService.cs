using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore;

namespace ccisurvey.services
{
	public class PasswordService : IPasswordService
	{
		public string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password, 12);
		}
		public bool VerifyHash(string hashedPassword, string providedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
		}
	}
}
