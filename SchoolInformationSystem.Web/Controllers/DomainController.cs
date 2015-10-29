using Microsoft.AspNet.Mvc;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.Models;
using System.Linq;

namespace SchoolInformationSystem.Web.Controllers
{
	[Route("api/domain")]
	public class DomainController : BaseController
	{
		private SchoolDataContext _context;
		public DomainController(SchoolDataContext context)
		{
			_context = context;
		}
		
		[HttpGet]
		[Route("students")]
		public StudentDomain GetStudentDomain()
		{
			return _context.StudentDomain.FirstOrDefault();
		}
		
		
	}
}