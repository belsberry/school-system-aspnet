using Microsoft.AspNet.Mvc;
using MongoDB.Driver;
using SchoolInformationSystem.Models;

namespace SchoolInformationSystem.Controllers
{
	[Route("api/[controller]")]
	public class UserController : Controller
	{
        IMongoDatabase _database;

        public UserController(IMongoDatabase database)
		{
			_database = database;
		}
		
		[HttpGet]
		public User Get()
		{
			return new User
			{
				FirstName = "User"	
			};
				
		}
	}
}