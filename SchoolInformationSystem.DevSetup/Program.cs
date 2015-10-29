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
            collection.AddTransient(typeof(Login));
            IServiceProvider provider = collection.BuildServiceProvider();
            
            IModelCreator creator = provider.GetService<IModelCreator>();
            SchoolDataContext context = provider.GetService<SchoolDataContext>();
            
            Login l = context.Logins.FirstOrDefault(x => x.UserName == "admin");
            if(l == null)
            {
                Login login = creator.LoadModel<Login>();
                User user = creator.LoadModel<User>();
                //Setup user
                user._id = Guid.NewGuid();
                user.FirstName = "Admin";
                user.LastName = "User";
                user.Email = "ben.elsberry@gmail.com";
                user.ScopeLevel = ScopeLevel.SuperUser;
                
                //Setup user login
                login.UserID = user._id;            
                login.SetPassword("password");
                login.UserName = "admin";
                login._id = Guid.NewGuid();
                                
                context.Create(login);
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
