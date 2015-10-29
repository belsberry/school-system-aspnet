using System.Linq;
using SchoolInformationSystem.Models;
using System;
using SchoolInformationSystem.Common.Data;

namespace SchoolInformationSystem.Data
{
	public class SchoolDataContext : DocumentContext
	{
		public SchoolDataContext(IDocumentProvider provider, 
			IServiceProvider serviceProvider) : base(provider, serviceProvider)
		{	
		}
		
		public IQueryable<User> Users { get; set; }
		public IQueryable<Login> Logins { get; set; }
		public IQueryable<School> Schools { get; set; }
		public IQueryable<DashboardData> DashboardData { get; set; }
		public IQueryable<StudentDomain> StudentDomain { get; set; }		
	}
}