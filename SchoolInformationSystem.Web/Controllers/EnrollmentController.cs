using SchoolInformationSystem.Data;
namespace SchoolInformationSystem.Controllers
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