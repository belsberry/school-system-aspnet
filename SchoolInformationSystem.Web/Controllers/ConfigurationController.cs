using Microsoft.AspNet.Mvc;
using SchoolInformationSystem.Data;
using System.Linq;
using SchoolInformationSystem.Models;
using System;

namespace SchoolInformationSystem.Web.Controllers
{
	[Route("api/configuration")]
	public class ConfigurationController : BaseController
	{
        SchoolDataContext _context;

        public ConfigurationController(SchoolDataContext context)
		{
            _context = context;
        }
		
		[HttpGet]
		[Route("systemsetup")]
		public IActionResult GetSystemSetup()
		{
			return Ok(_context.StudentDomain.FirstOrDefault());
		}
		[HttpPut]
		[Route("systemsetup")]
		public IActionResult UpdateSystemSetup([FromBody]StudentDomain systemSetup)
		{
			if(systemSetup.Id != Guid.Empty)
			{
				_context.Update(systemSetup);
			}
			else
			{
				systemSetup.Id = Guid.NewGuid();
				_context.Create(systemSetup);	
			}
			
			return Ok();
		}
	}
}