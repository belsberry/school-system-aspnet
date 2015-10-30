using SchoolInformationSystem.Data;
using Microsoft.AspNet.Mvc;
using SchoolInformationSystem.Models;
using System.Linq;
using System;
using SchoolInformationSystem.Web.ViewModels;
using System.Collections.Generic;

namespace SchoolInformationSystem.Web.Controllers
{
	[Route("auth")]
	[AllowAnonymous]
	public class AuthLoginController : BaseController
	{
		private SchoolDataContext _context;
		public AuthLoginController(SchoolDataContext context)
		{
			_context = context;
		}
		
		[Route("login")]
		[HttpGet]
		public ActionResult Login(string next = "")
		{
			Console.WriteLine(next);	
			return View(new LoginVM() { Next = next });
		}
		
		[Route("login")]
		[HttpPost]
		public ActionResult Login(string userName, string password, string next = "")
		{
			Console.WriteLine("{0} {1} {2}", userName, password, next);
			Console.WriteLine(_context);
			Console.WriteLine(_context.Logins);
			
			Login userLogin = _context
				.Logins
				.FirstOrDefault(x => x.UserName == userName);
			Console.WriteLine(userLogin);
			if(userLogin != null && userLogin.CheckPassword(password))
			{
				User user = _context.Users
					.FirstOrDefault(x => x._id == userLogin.UserID);
				if(user != null)
				{
					
					SignIn(user);
					return Redirect("/"); //Redirect to the root page
				}
				else
				{
					return View(new LoginVM() { Next = next, ErrorMessage = "User is not set up correctly." });	
				}	
			}
			else
			{
				return View(new LoginVM() { Next = next, ErrorMessage = "User Name or Password is incorrect" });
			}
		}
		
		[Route("logout")]
		public ActionResult Logout()
		{
			SignOut();
			return RedirectToAction("Login");
		}
	}
}