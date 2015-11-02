using System;

namespace SchoolInformationSystem.Web.DTO
{
	public class CurrentSchoolIDPost
	{
		public Guid? CurrentSchoolID { get; set; }
	}
	
	public class NewPasswordPost
	{
		public string NewPassword { get; set; }
	}
	
}