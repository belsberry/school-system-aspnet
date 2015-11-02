using System;
using Microsoft.AspNet.Mvc;
using System.Linq;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.Models;
using Microsoft.AspNet.Authorization;
using SchoolInformationSystem.Web.DTO;
using System.Collections.Generic;

namespace SchoolInformationSystem.Web.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class AuthController : BaseController
	{
		
		private SchoolDataContext _context;
		public AuthController(SchoolDataContext context)
		{
			_context = context;
		}
				
		[Route("profile")]
		[HttpGet]
		public User GetProfile()
		{
			User currentUser = _context.Users.FirstOrDefault(u => u.Id == this.ApplicationUser.Id);
			return currentUser;
		}
		
		[Route("profile")]
		[HttpPost]
		public User UpdateProfile([FromBody]User user)
		{
			User currentUser = _context.Users
				.FirstOrDefault(u => u.Id == user.Id);
			currentUser.FirstName = user.FirstName;
			currentUser.LastName = user.LastName;
			currentUser.LicenseNumber = user.LicenseNumber;
			currentUser.Email = user.Email;
			_context.Update(currentUser);
			return user;
		}
		
		[Route("session/currentschoolid")]
		[HttpGet]
		public object GetCurrentSchoolID()
		{
			Guid currentSchoolID;
			Guid.TryParse(HttpContext.Session.Get("CurrentSchoolID"), out currentSchoolID);
			return new CurrentSchoolIDPost() { CurrentSchoolID = currentSchoolID == Guid.Empty ? null : (Guid?)currentSchoolID };
		}
		
		[Route("session/currentschoolid")]
		[HttpPost]
		public void SetCurrentSchoolID([FromBody]CurrentSchoolIDPost schoolPost)
		{
			Guid? currentSchoolID = schoolPost.CurrentSchoolID;
			
			HttpContext.Session.Set("CurrentSchoolID", currentSchoolID.ToString());
		}
		
		[Route("setpassword")]
		[HttpPost]
		public void SetPassword([FromBody]NewPasswordPost pwdPost)
		{
			string newPassword = pwdPost.NewPassword;
			Guid currentUserID = this.ApplicationUser.Id;
			User login = _context.Users.FirstOrDefault(lg => lg.Id == currentUserID);
			login.SetPassword(newPassword);
			_context.Update(login);
		}
		
		[Route("users")]
		[HttpGet]
		public IEnumerable<User> GetUsers(string searchString = "")
		{
			//TODO make current school specific
			
			string[] searchParts = searchString.Split(' ');
			IQueryable<User> userQuery = _context.Users;
			foreach(string part in searchParts)
			{
				userQuery = userQuery.Where(u => u.LastName.Contains(part) || u.FirstName.Contains(part));
			}
			return userQuery.ToList();
		}
		
		[Route("users")]
		[HttpPost]
		public User AddUser([FromBody]User user)
		{
			user.Id = Guid.NewGuid();
			user.SetPassword("password");
			_context.Create(user);
			return user;
			
		}
	}
}