using System;
using SchoolInformationSystem.Common.Data;

namespace SchoolInformationSystem.Models
{
	public class User : IHaveId
	{
		public User(){}
		
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string LicenseNumber { get; set; }
		public string Email { get; set; }
		public ScopeLevel ScopeLevel { get; set; }
		public Guid SchoolID { get; set; }
	}
}