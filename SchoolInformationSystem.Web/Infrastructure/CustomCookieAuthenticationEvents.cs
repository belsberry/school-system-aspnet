using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Cookies;
namespace SchoolInformationSystem.Web.Infrastructure
{
	class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
	{
		public override async Task RedirectToLogin(CookieRedirectContext context)
		{
			if(context.Request.Path.Value.ToLower().StartsWith("/api")) //return the correct stuff for api results
			{
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;	
			}
			else
			{
				await base.RedirectToLogin(context);
			}
			
		}
	}	
}
