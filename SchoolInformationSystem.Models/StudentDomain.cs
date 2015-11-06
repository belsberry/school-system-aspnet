using System.Collections.Generic;
using SchoolInformationSystem.Common.Data;
using System;

namespace SchoolInformationSystem.Models
{
	public class StudentDomain : IHaveId
	{
		public Guid Id { get; set; }
		private List<Grade> _grades;
		public List<Grade> Grades 
		{ 
			get
			{
				if(_grades == null)
				{
					_grades = new List<Grade>();
				}
				return _grades;
			}
			set
			{
				_grades = value;
			}
		}
	}
	
	public class Grade
	{
		private Guid _id;
		public Guid Id 
		{ 
			get
			{
				if(_id == Guid.Empty)
				{
					_id = Guid.NewGuid();
				}
				return _id;
			} 
			set
			{
				_id = value;	
			}
		}
		//Alias
		public Guid GradeId { get { return Id; } }
		
		public string Description { get; set; }
	}
}