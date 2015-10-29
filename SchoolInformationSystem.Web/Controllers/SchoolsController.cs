using Microsoft.AspNet.Authorization;
using SchoolInformationSystem.Data;
using System.Collections.Generic;
using SchoolInformationSystem.Models;
using System.Linq;
using Microsoft.AspNet.Mvc;

namespace SchoolInformationSystem.Web.Controllers
{
	[Authorize]
	[Route("api/configuration/[controller]")]
	public class SchoolsController : BaseController
	{
		private SchoolDataContext _context;
		public SchoolsController(SchoolDataContext context)
		{
			_context = context;
		}
		
		[HttpGet]
		public IEnumerable<School> Get()
		{
			return _context.Schools.ToList();
		}
		
		public void Post(School school)
		{
			_context.Create(school);
		}
	}
}