using Microsoft.AspNet.Mvc;
using SchoolInformationSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System;
using Microsoft.AspNet.Authentication.Cookies;

namespace SchoolInformationSystem.Controllers
{
	public abstract class BaseController : Controller
	{
		protected User ApplicationUser
		{
			get
			{
				try
				{
					ClaimsIdentity id = HttpContext.User.Identity as ClaimsIdentity;
					User u = new User();
					u.FirstName = id.FindFirst("FirstName").Value;
					u.LastName = id.FindFirst("LastName").Value;
					u._id = new Guid(id.FindFirst("_id").Value);
					u.LicenseNumber = id.FindFirst("LicenseNumber").Value;
					return u;
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex);
					throw;
				}
				
			}
		}
		
		protected void SetUser(User u)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim("FirstName", u.FirstName),
				new Claim("LastName", u.LastName),
				new Claim("_id", u._id.ToString()),
				new Claim("LicenseNumber", u.LicenseNumber)
			};
			ClaimsIdentity id = new ClaimsIdentity(claims);
			ClaimsPrincipal principal = new ClaimsPrincipal(id);
			HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();			
		}		
		
	}
}