using SchoolInformationSystem.Common.Data;
using System;
using SchoolInformationSystem.Models.Domain;

namespace SchoolInformationSystem.Models
{
	public class Student : IHaveId
	{
		public Guid Id { get; set; }
		
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public RaceComposite Race { get; set; }
		public string Gender { get; set; }
		public DateTime Dob { get; set; }
		public string Usid { get; set; }
		public string GradeId { get; set; }
		public string GradeDescription { get; set; }
	}
	
	public class RaceComposite
	{
		public bool White { get; set; }
		public bool Black { get; set; }
		public bool Asian { get; set; }
		public bool PacificIslander { get; set; }
		public bool AmericanIndian { get; set; }
		
		public RaceDomain ReportedRace
		{
			get
			{
				if(this.Black)
				{
					return RaceDomain.Black;	
				}
				else if(this.Asian)
				{
					return RaceDomain.Asian;
				}
				else if(this.AmericanIndian)
				{
					return RaceDomain.AmericanIndian;
				}
				else if(this.PacificIslander)
				{
					return RaceDomain.PacificIslander;
				}
				else if(this.White)
				{
					return RaceDomain.White;
				}
				else
				{
					return RaceDomain.Unspecified;
				}
			}
		}
	}
}