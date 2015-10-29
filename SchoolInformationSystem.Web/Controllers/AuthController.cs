using System;
using Microsoft.AspNet.Mvc;
using System.Linq;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.Models;

namespace SchoolInformationSystem.Controllers
{
	[Route("api/[controller]")]
	public class AuthController : BaseController
	{
		
		private SchoolDataContext _context;
		public AuthController(SchoolDataContext context)
		{
			_context = context;
			
		}
		
		[Route("login")]
		[HttpPost] 
		public void Login(string userName, string password)
		{
			Login userLogin = _context
				.Logins
				.FirstOrDefault(x => x.UserName == userName);
			
			SetUser(new User{
				FirstName = "Foo",
				LastName = "Bar",
				LicenseNumber = "123123123",
				_id = Guid.NewGuid()
			});
		}
		
		[Route("profile")]
		[HttpGet]
		public User GetProfile()
		{
			return this.ApplicationUser;
		}
		
		[Route("profile")]
		[HttpGet]
		public User UpdateProfile(User user)
		{
			User currentUser = _context.Users
				.FirstOrDefault(u => u._id == user._id);
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
			Guid currentSchoolID = new Guid(HttpContext.Session.Get("CurrentSchoolID"));
			return new { currentSchoolID = currentSchoolID };
		}
		
		[Route("session/currentschoolid")]
		[HttpPost]
		public void SetCurrentSchoolID(Guid currentSchoolID)
		{
			HttpContext.Session.Set("CurrentSchoolID", currentSchoolID.ToString());
		}
		
		[Route("setpassword")]
		[HttpPost]
		public void SetPassword(string newPassword)
		{
			Guid currentUserID = this.ApplicationUser._id;
			Login login = _context.Logins.FirstOrDefault(lg => lg.UserID == currentUserID);
			
		}
	}
}