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
		
		
		[HttpGet]
		[Route("classrosters/{id:Guid}/assignments")]
		public IActionResult GetClassRosterAssignments(Guid id)
		{
			ClassRoster roster = _context.ClassRosters.FirstOrDefault(x => x.Id == id);
			if(roster == null)
			{
				return HttpNotFound();
			}
			
			return Ok(roster.Assignments);
			
		}
		
		
		[HttpPost]
		[Route("classrosters/{id:Guid}/assignments")]
		public IActionResult AddClassRosterAssignment(Guid id, [FromBody]Assignment assignment)
		{
			Console.WriteLine(id);
			Console.WriteLine(assignment);
			if(id == Guid.Empty)
			{
				return HttpBadRequest();
			}
			ClassRoster roster = _context.ClassRosters.FirstOrDefault(x => x.Id == id);
			if(roster == null)
			{
				return HttpNotFound();
			}
			if(assignment == null)
			{
				return HttpBadRequest();
			}
			assignment.Id = Guid.NewGuid();
			roster.Assignments.Add(assignment);
			_context.Update(roster);
			return Ok(assignment);
		}
	}
}