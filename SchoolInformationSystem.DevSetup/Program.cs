using System;
using Microsoft.Framework.DependencyInjection;
using SchoolInformationSystem.Common.Models;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.Common.Security;
using SchoolInformationSystem.Models;
using SchoolInformationSystem.Common.Data;
using System.Linq;

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
            
        }
    }
}
