using System;
using System.Collections.Generic;
using SchoolInformationSystem.Common.Data;

namespace SchoolInformationSystem.Models
{
	public class DashboardData : IHaveId
	{
		public Guid Id { get; set; }
		public List<AssignmentGrade> AssignmentGrades { get; set; }
		public List<ReferralCount> ReferralCounts { get; set; }
		public List<AttendanceCount> Attendance { get; set; }
        public Guid SchoolId { get; set; }
    }
	
	public class AssignmentGrade
	{
		public string Grade { get; set; }
		public int RecordCount { get; set; }
	}
	
	public class ReferralCount
	{
		public string GradeLevel { get; set; }
		public int Id { get; set; }
		public int NumberOfReferrals { get; set; }
		
	}
	
	public class AttendanceCount
	{
		public string Day { get; set; }
		public int Count { get; set; }
	}
}