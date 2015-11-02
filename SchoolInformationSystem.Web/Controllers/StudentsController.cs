using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.Models;
using System;

namespace SchoolInformationSystem.Web.Controllers
{
	[Route("api/[controller]")]
	public class StudentsController : BaseController
	{
        SchoolDataContext _context;

        public StudentsController(SchoolDataContext context)
		{
			_context = context;
		}
		
		[HttpGet]
		public IEnumerable<Student> Get(string search = "")
		{
			
			IQueryable<Student> studentQuery = _context.Students;
			if(search != null)
			{						
				string[] searchParts = search.Split(' ');
				foreach(string part in searchParts)
				{
					studentQuery.Where(s => s.FirstName.Contains(part) || s.LastName.Contains(part));
				}
			}
			return studentQuery.ToList();
			
		}
		
		[Route("{id:Guid}")]
		[HttpGet]
		public Student GetStudent(Guid id)
		{
			return _context.Students.FirstOrDefault(s => s.Id == id);
		}
		
		[Route("{id:Guid}")]
		[HttpPost]
		public Student UpdateStudent(Guid id, [FromBody]Student stu)
		{
			if(stu.Id != id){
				Console.WriteLine("Ids don't match");
			}
			_context.Update(stu);
			return stu;
		}
		
		[HttpPost]
		public Student Post([FromBody]Student student)
		{
			student.Id = Guid.NewGuid();
			_context.Create(student);
			return student;
		}
		
	}
}