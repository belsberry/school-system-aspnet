using SchoolInformationSystem.Data;
using Microsoft.AspNet.Mvc;
using SchoolInformationSystem.Models;
using System.Linq;
using System;
using SchoolInformationSystem.Web.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;

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
			return View(new LoginVM() { Next = next });
		}
		
		[Route("login")]
		[HttpPost]
		public ActionResult Login(string userName, string password, string next = "")
		{
			User userLogin = _context
				.Users
				.FirstOrDefault(x => x.UserName == userName);
			if(userLogin != null && userLogin.CheckPassword(password))
			{
				
				SignIn(userLogin);
				if(String.IsNullOrEmpty(next))
				{
					return Redirect("/"); //Redirect to the root page	
				}
				else
				{
					return Redirect("/#" + next);
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