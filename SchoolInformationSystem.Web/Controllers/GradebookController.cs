using Microsoft.AspNet.Mvc;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolInformationSystem.Web.Controllers
{
	[Route("api/[controller]")]
	public class GradebookController : BaseController
	{
		private SchoolDataContext _context;
		public GradebookController(SchoolDataContext context)
		{
			_context = context;
		}
		
		[HttpGet]
		[Route("")]
		public GradebookDashboardData Get()
		{
			return _context.GradebookDashboardData.FirstOrDefault();
		}
		
		[HttpGet]
		[Route("classrosters")]
		public IEnumerable<ClassRoster> GetClassRosters()
		{
			return _context.ClassRosters.ToList();
		}
		
		
		[HttpPost]
		[Route("classrosters")]
		public ClassRoster AddClassRoster([FromBody]ClassRoster roster)
		{
			roster.Id = Guid.NewGuid();
			_context.Create(roster);
			return roster;
		}
		
		
	}
}