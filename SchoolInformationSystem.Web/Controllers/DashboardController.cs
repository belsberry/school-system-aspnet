using SchoolInformationSystem.Data;
using System.Linq;
using SchoolInformationSystem.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;

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
		
        //Get an aggregate of all of the dashboard data
		public IActionResult Get()
		{
            List<DashboardData> ddList = _context.DashboardData.ToList();

            DashboardData agg = new DashboardData();
            agg.AssignmentGrades = ddList.SelectMany(x => x.AssignmentGrades)
                .GroupBy(x => x.Grade)
                .Select(x => new AssignmentGrade() { Grade = x.Key, RecordCount = x.Sum(y => y.RecordCount) })
                .ToList();
            agg.Attendance = ddList.SelectMany(x => x.Attendance)
                .GroupBy(x => x.Day)
                .Select(x => new AttendanceCount() { Day = x.Key, Count = x.Sum(y => y.Count) })
                .ToList();

            agg.ReferralCounts = ddList.SelectMany(x => x.ReferralCounts)
                .GroupBy(x => x.GradeLevel)
                .Select(x => new ReferralCount() { GradeLevel = x.Key, NumberOfReferrals = x.Sum(y => y.NumberOfReferrals) })
                .ToList();

			return Ok(agg);
		}

        //Get dashboard data for the school
        [Route("{schoolId:Guid}")]
        public IActionResult Get(Guid schoolId)
        {
            return Ok(_context.DashboardData.FirstOrDefault(x => x.SchoolId == schoolId));
        }
	}
}