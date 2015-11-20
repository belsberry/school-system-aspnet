using System;
using Microsoft.Framework.DependencyInjection;
using SchoolInformationSystem.Common.Models;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.Common.Security;
using SchoolInformationSystem.Models;
using SchoolInformationSystem.Common.Data;
using System.Linq;
using System.Collections.Generic;

namespace SchoolInformationSystem.DevSetup
{
    public class Program
    {
        public void Main(string[] args)
        {
            
            IServiceCollection collection = new ServiceCollection();
            collection.AddTransient(typeof(IModelCreator), typeof(ModelCreator));
            collection.AddSingleton(typeof(SchoolDataContext));
            collection.AddTransient(typeof(IDocumentProvider), x => {
               return new MongoDBDocumentProvider("mongodb://localhost"); 
            });
            collection.AddTransient(typeof(IEncryption), typeof(Encryption));
            collection.AddTransient(typeof(User));
            collection.AddTransient(typeof(School));
            IServiceProvider provider = collection.BuildServiceProvider();
            
            IModelCreator creator = provider.GetService<IModelCreator>();
            SchoolDataContext context = provider.GetService<SchoolDataContext>();
            
            User l = context.Users.FirstOrDefault(x => x.UserName == "admin");
            if(l == null)
            {
                User user = creator.LoadModel<User>();
                //Setup user
                user.Id = Guid.NewGuid();
                user.FirstName = "Admin";
                user.LastName = "User";
                user.Email = "ben.elsberry@gmail.com";
                user.ScopeLevel = ScopeLevel.SuperUser;
                user.UserName = "admin";
                user.SetPassword("password");
                
                                
                context.Create(user);
                
                Console.WriteLine("User/Login created successfully!");
            }
            else
            {
                Console.WriteLine("Login already created");
            }

            IEnumerable<School> schools = context.Schools;
            foreach(School sc in schools)
            {
                DashboardData dd = context.DashboardData.FirstOrDefault(x => x.SchoolId == sc.Id);
                if(dd == null)
                {
                    //Generate random dashboard data for each school
                    dd = new DashboardData();
                    dd.AssignmentGrades = new List<AssignmentGrade>();
                    Random rnd = new Random();
                    
                    foreach (string grd in new string[] { "A", "B", "C", "D", "F" })
                    {
                        dd.AssignmentGrades.Add(new AssignmentGrade()
                        {
                            Grade = grd,
                            RecordCount = rnd.Next(100)
                        });
                    }

                    dd.Attendance = new List<AttendanceCount>();
                    foreach(string day in new string[] { "1/2/2015", "1/3/2015", "1/4/2015" })
                    {
                        dd.Attendance.Add(new AttendanceCount()
                        {
                            Day = day,
                            Count = rnd.Next(100)
                        });
                    }

                    dd.ReferralCounts = new List<ReferralCount>();
                    foreach (string grd in new string[] { "9th", "10th", "11th", "12th" })
                    {
                        dd.ReferralCounts.Add(new ReferralCount()
                        {
                            GradeLevel = grd,
                            NumberOfReferrals = rnd.Next(10)
                        });
                    }


                    dd.SchoolId = sc.Id;
                    context.Create(dd);
                }
            }
            
        }
    }
}
