using SchoolInformationSystem.Common.Data;
using System;
namespace SchoolInformationSystem.Models
{
	public class School : IHaveId
	{
		public School(){}
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}