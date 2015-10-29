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
            Login login = creator.LoadModel<Login>();
            login.SetPassword("password");
            
            Console.WriteLine(login.Salt);
            Console.WriteLine(login.PasswordHash[0]);
            
            SchoolDataContext context = provider.GetService<SchoolDataContext>();
            context.Create(login);
            
            Login first = context.Logins.FirstOrDefault();
            first.SetPassword("flackity");
            first.UserName = "admin";
            context.Update(first);
            
            Login final = context.Logins.FirstOrDefault(l => l.UserName == "admin");
            Console.WriteLine(final.CheckPassword("flackity"));
        }
    }
}
