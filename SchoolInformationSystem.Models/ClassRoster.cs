using System;
using System.Collections.Generic;
using SchoolInformationSystem.Common.Data;

namespace SchoolInformationSystem.Models
{
	public class ClassRoster : IHaveId
	{
		public Guid Id { get; set; }
		public string ClassName { get; set; }
		public int Period { get; set; }
		public List<ClassRosterStudent> Students { get; set; }
		
		public List<Assignment> Assignments { get; set; }
	}
	
	public class ClassRosterStudent
	{
		public Guid StudentId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
	public class Assignment
	{
		public string Description { get; set; }
		public DateTime OpenDate { get; set; }
		public DateTime DueDate { get; set; }
		public float AverageGrade { get; set; }
	}
}