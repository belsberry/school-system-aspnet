using Microsoft.AspNet.Authorization;
using SchoolInformationSystem.Data;
using System.Collections.Generic;
using SchoolInformationSystem.Models;
using System.Linq;
using Microsoft.AspNet.Mvc;
using System;

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
		
		[HttpPost]
		public IActionResult Post([FromBody]School school)
		{
			school.Id = Guid.NewGuid();
			_context.Create(school);
			return Ok(school);
		}
	}
}