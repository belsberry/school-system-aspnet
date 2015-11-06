using System;
using System.Collections.Generic;
using SchoolInformationSystem.Common.Data;

namespace SchoolInformationSystem.Models
{
	public class SchoolDemographics : IHaveId
	{
		public Guid Id { get; set; }
		public List<SchoolDemographicGrade> Grades { get; set; }
		public List<SchoolDemographicGender> Genders { get; set; }
		public List<SchoolDemographicReportedRace> ReportedRaces { get; set; }
	}
	
	public class SchoolDemographicGrade
	{
		public Guid Id { get; set; }
		public string GradeId { get; set; }
		public string GradeDescription { get; set; }
		public int Count { get; set; }
	}
	
	public class SchoolDemographicGender
	{
		public Guid Id { get; set; }
		public string GenderId { get; set; }
		public string GenderDescription { get; set; }
		public int Count { get; set; }
	}
	
	public class SchoolDemographicReportedRace
	{
		public Guid Id { get; set; }
		public string ReportedRaceId { get; set; }
		public string ReportedRaceDescription { get; set; }
		public int Count { get; set; }
	}
}