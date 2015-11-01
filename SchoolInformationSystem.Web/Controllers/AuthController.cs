using System;
using Microsoft.AspNet.Mvc;
using System.Linq;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.Models;
using Microsoft.AspNet.Authorization;

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
			return new { currentSchoolID = currentSchoolID };
		}
		
		[Route("session/currentschoolid")]
		[HttpPost]
		public void SetCurrentSchoolID([FromBody]Guid currentSchoolID)
		{
			HttpContext.Session.Set("CurrentSchoolID", currentSchoolID.ToString());
		}
		
		[Route("setpassword")]
		[HttpPost]
		public void SetPassword(string newPassword)
		{
			Guid currentUserID = this.ApplicationUser.Id;
			Login login = _context.Logins.FirstOrDefault(lg => lg.UserID == currentUserID);
			login.SetPassword(newPassword);
			_context.Update(login);
		}
	}
}