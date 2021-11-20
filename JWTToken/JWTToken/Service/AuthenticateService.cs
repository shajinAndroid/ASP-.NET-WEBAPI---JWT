using JWTToken.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTToken.Service
{
	public class AuthenticateService : IAuthenticateService
	{
		private readonly AppSettings _appSettings;

		public AuthenticateService(IOptions<AppSettings> appSettings)
		{
			_appSettings = appSettings.Value;
		}

		private readonly List<User> userList = new List<User>
		{
			new User { UserId = 1, UserName= "test", Password="test" }
		};

		public User Authenticate(string userName, string password)
		{
			var user = userList.SingleOrDefault(x => x.UserName.Equals(userName) && x.Password.Equals(password));

			if (user == null)
				return null;

			var tokenHandler = new JwtSecurityTokenHandler();

			var key = Encoding.ASCII.GetBytes(_appSettings.Key);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[] {
					new Claim(ClaimTypes.Name, user.UserName.ToString()),
					new Claim(ClaimTypes.Role, "Admin"),
					new Claim(ClaimTypes.Version, "V3.1")
				}),
				Expires = DateTime.UtcNow.AddDays(2),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			user.Token = tokenHandler.WriteToken(token);

			user.Password = null;

			return user;
		}
	}
}
