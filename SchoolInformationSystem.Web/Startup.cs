using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json.Serialization;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.Common.Data;
using Microsoft.Framework.Configuration;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Logging;


namespace SchoolInformationSystem
{
    public class Startup
    {
        private IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            Configuration = config.Build();   
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            
            services.AddMvc().AddJsonOptions(x => {
                x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            
            string mongoConnection = Configuration.GetSection("Data:DefaultConnection:Mongo").Value;
            
            services.AddSingleton(typeof(IDocumentProvider), x => {
                return new MongoDBDocumentProvider(mongoConnection);   
            });
            
            services.AddSingleton<SchoolDataContext>();
            
            services.AddAuthentication();
            
            services.AddSession();
            services.AddCaching();          
            //IServiceProvider provider = services.BuildServiceProvider();
            
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            logger.AddConsole();
            
            // Configure the HTTP request pipeline.
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseCookieAuthentication(options => {
                options.AutomaticAuthentication = true; // This makes it do.  Not sure why
            });
            
            app.UseSession();
            
            // Add MVC to the request pipeline.
            app.UseMvc();
           
            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
        }
    }
}
