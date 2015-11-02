using Microsoft.AspNet.Mvc;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.Models;
using System.Linq;
using System;

namespace SchoolInformationSystem.Web.Controllers
{
	[Route("api/domain")]
	public class DomainController : BaseController
	{
		private SchoolDataContext _context;
		public DomainController(SchoolDataContext context)
		{
			_context = context;
		}
		
		[HttpGet]
		[Route("students")]
		public StudentDomain GetStudentDomain()
		{
			return _context.StudentDomain.FirstOrDefault();
		}
		
		[HttpPost]
		[Route("students/grades")]
		public void AddGrade([FromBody]Grade grade)
		{
			
			StudentDomain domain = _context.StudentDomain.FirstOrDefault();
			if(domain == null)
			{
				domain = new StudentDomain();
				domain.Id = Guid.NewGuid();
				_context.Create(domain);
			}
			grade.Id = Guid.NewGuid();
			domain.Grades.Add(grade);
			_context.Update(domain);
		}
		
		
	}
}