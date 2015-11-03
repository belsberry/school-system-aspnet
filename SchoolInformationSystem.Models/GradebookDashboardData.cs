using System;
using System.Collections.Generic;
using SchoolInformationSystem.Common.Data;
namespace SchoolInformationSystem.Models
{
	public class GradebookDashboardData : IHaveId
	{
		public Guid Id { get; set; }
		public List<Assignment> OpenAssignments { get; set; }
		public List<Assignment> RecentAssignments { get; set; }
		public List<RosterAverage> QuickRosterAverages { get; set; }
		
	}
	
	public class RosterAverage
	{
		public string RosterName { get; set; }
		public int Period { get; set; }
		public List<RosterAverageGrade> Averages { get; set; }
	}
	public class RosterAverageGrade
	{
		public string GradeLetter { get; set; }
		public int NumberOfStudents { get; set; }
	}
}