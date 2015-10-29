using SchoolInformationSystem.Common.Data;
using System;
namespace SchoolInformationSystem.Models
{
	public class School : IHaveId
	{
		public Guid _id { get; set; }
		public string Name { get; set; }
	}
}