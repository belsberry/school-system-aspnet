using SchoolInformationSystem.Data;
namespace SchoolInformationSystem.Web.Controllers
{
	public class EnrollmentController : BaseController
	{
		private SchoolDataContext _context;
		public EnrollmentController(SchoolDataContext context)
		{
			_context = context;
		}
		
		
	}
}