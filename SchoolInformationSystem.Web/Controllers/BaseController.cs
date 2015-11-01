using Microsoft.AspNet.Mvc;
using SchoolInformationSystem.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using Microsoft.AspNet.Authentication.Cookies;

namespace SchoolInformationSystem.Web.Controllers
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
					u.Id = new Guid(id.FindFirst("Id").Value);
					u.LicenseNumber = id.FindFirst("LicenseNumber").Value;
					u.ScopeLevel = (ScopeLevel)Int32.Parse(id.FindFirst("ScopeLevel").Value);
					
					return u;
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex);
					throw;
				}
				
			}
		}
		
		protected void SignIn(User u)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim("FirstName", u.FirstName ?? ""),
				new Claim("LastName", u.LastName ?? ""),
				new Claim("Id", u.Id.ToString()),
				new Claim("LicenseNumber", u.LicenseNumber ?? ""),
				new Claim("ScopeLevel", ((int)u.ScopeLevel).ToString())
			};
			ClaimsIdentity id = new ClaimsIdentity(claims, "local", "name", "role");
			ClaimsPrincipal principal = new ClaimsPrincipal(id);
			HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();			
		}		
		
		protected void SignOut()
		{
			HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
		}
		
	}
}