using SchoolInformationSystem.Data;
using Microsoft.AspNet.Mvc;
using SchoolInformationSystem.Models;
using System.Linq;

namespace SchoolInformationSystem.Web.Controllers
{
	[Route("auth/login")]
	[AllowAnonymous]
	public class AuthLoginController : BaseController
	{
		private SchoolDataContext _context;
		public AuthLoginController(SchoolDataContext context)
		{
			_context = context;
		}
		
		[HttpGet]
		public ActionResult Get()
		{
			return View();
		}
		
		[HttpPost]
		public ActionResult Post(string userName, string password)
		{
			Login userLogin = _context
				.Logins
				.FirstOrDefault(x => x.UserName == userName);
			if(userLogin != null && userLogin.CheckPassword(password))
			{
				User user = _context.Users
					.FirstOrDefault(x => x._id == userLogin.UserID);
				if(user != null)
				{
					
					SetUser(user);
					return Redirect("/"); //Redirect to the root page
				}
				else
				{
					return View("User is not set up correctly.");	
				}	
			}
			else
			{
				return View("User Name or Password is incorrect");
			}
		}
	}
}