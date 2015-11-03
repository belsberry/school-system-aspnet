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
		public IQueryable<School> Schools { get; set; }
		public IQueryable<DashboardData> DashboardData { get; set; }
		public IQueryable<StudentDomain> StudentDomain { get; set; }		
		public IQueryable<Student> Students { get; set; }
		public IQueryable<ClassRoster> ClassRosters { get; set; }
		public IQueryable<GradebookDashboardData> GradebookDashboardData { get; set; }
	}
}