using System.Collections.Generic;
using SchoolInformationSystem.Common.Data;
using System;

namespace SchoolInformationSystem.Models
{
	public class StudentDomain : IHaveId
	{
		public Guid Id { get; set; }
		public List<Grade> Grades { get; set; }
	}
	
	public class Grade
	{
		public string Description { get; set; }
		public int GradeId { get; set; }
	}
}