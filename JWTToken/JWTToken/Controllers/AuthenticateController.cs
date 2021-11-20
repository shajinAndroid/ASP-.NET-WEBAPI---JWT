using JWTToken.Models;
using JWTToken.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTToken.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthenticateController : ControllerBase
	{
		private IAuthenticateService _authenticateService;

		public AuthenticateController(IAuthenticateService authenticateService)
		{
			_authenticateService = authenticateService;
		}

		[HttpPost]
		public IActionResult Post([FromBody] User model)
		{
			var user = _authenticateService.Authenticate(model.UserName, model.Password);

			if (user == null)
			{
				return BadRequest("User Name / Password incorrect.");
			}

			return Ok(user);
		}
	}
}
