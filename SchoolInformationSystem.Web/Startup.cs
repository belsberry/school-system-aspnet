using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using SchoolInformationSystem.Data;
using SchoolInformationSystem.Common.Data;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Logging;
using System;
using SchoolInformationSystem.Common.Models;
using SchoolInformationSystem.Web.Infrastructure;
using SchoolInformationSystem.Models;
using SchoolInformationSystem.Common.Security;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            
            var defaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                .Build();
            services.AddLogging();
            
            IMvcBuilder mvcBuilder = services.AddMvc(options => {
                options.Filters.Add(new AuthorizeFilter(defaultPolicy));
            });
            string mongoConnection = Configuration.GetSection("Data:DefaultConnection:Mongo").Value;
            
            /**
            *   Register the things
            */
            services.AddSingleton(typeof(IDocumentProvider), x => {
                return new MongoDBDocumentProvider(mongoConnection);   
            });
            
            services.AddSingleton<SchoolDataContext>();
            
            services.AddAuthentication();
            services.AddSession();
            services.AddCaching();          
            services.AddTransient(typeof(User));
            services.AddTransient(typeof(IEncryption), typeof(Encryption));
            
            services.AddTransient(typeof(IModelCreator), typeof(ModelCreator));
            /**
            *   Build service provider
            */
            IServiceProvider provider = services.BuildServiceProvider();
            
            /**
            *   Setup serializer
            */
            mvcBuilder.AddJsonOptions(x => {
                
                x.SerializerSettings.ContractResolver = new DIContractResolver(provider.GetService<IModelCreator>());
            });
            
            services.AddAuthorization();
        }


        
        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //logger.AddConsole();
            
            // Configure the HTTP request pipeline.
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            
            app.UseCookieAuthentication(options => {
                //options.AutomaticAuthentication = true; // This makes it do.  Not sure why
                options.AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Events = new CustomCookieAuthenticationEvents();
            });
            
            app.UseSession();
            app.UseDeveloperExceptionPage();
            // Add MVC to the request pipeline.
            app.UseMvc();
        }
    }
}
