using SchoolInformationSystem.Data;
using System.Linq;
using SchoolInformationSystem.Models;
using Microsoft.AspNet.Mvc;

namespace SchoolInformationSystem.Web.Controllers
{
	[Route("api/dashboard")]
	public class DashboardController : BaseController
	{
		private SchoolDataContext _context;
		public DashboardController(SchoolDataContext context)
		{
			_context = context;
		}	
		
		public DashboardData Get()
		{
			return _context.DashboardData.FirstOrDefault();
		}
	}
}