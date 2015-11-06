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
		private List<ClassRosterStudent> _students;
		public List<ClassRosterStudent> Students 
		{ 
			get
			{
				if(_students == null)
				{
					_students = new List<ClassRosterStudent>();
				}
				return _students;
			} 
			set
			{
				_students = value;
			}
		}
		
		private List<Assignment> _assignments;
		public List<Assignment> Assignments 
		{ 
			get
			{
				if(_assignments == null)
				{
					_assignments = new List<Assignment>();
				}
				return _assignments;
			} 
			set
			{
				_assignments = value;
			}
		}
	}
	
	public class ClassRosterStudent
	{
		public Guid StudentId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
	public class Assignment
	{
		public Guid Id { get; set; }
		public string Description { get; set; }
		public DateTime OpenDate { get; set; }
		public DateTime DueDate { get; set; }
		public float AverageGrade { get; set; }
	}
}