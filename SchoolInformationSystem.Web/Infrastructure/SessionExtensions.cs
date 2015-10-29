using Microsoft.AspNet.Http.Features;
using System.Text;

namespace SchoolInformationSystem
{
	public static class SessionExtensions
	{
		public static void Set(this ISession session, string key, string value)
		{
			session.Set(key, Encoding.ASCII.GetBytes(value));
		}
		
		public static string Get(this ISession session, string key)
		{
			byte[] data;
			if(session.TryGetValue(key, out data))
			{
				return Encoding.ASCII.GetString(data);
			}
			return "";
		}
	}
}