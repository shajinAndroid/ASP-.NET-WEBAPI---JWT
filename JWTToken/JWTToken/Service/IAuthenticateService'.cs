using JWTToken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTToken.Service
{
	public interface IAuthenticateService
	{
		User Authenticate(string userName, string password);
	}
}
