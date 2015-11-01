using System;
using Newtonsoft.Json.Converters;
using SchoolInformationSystem.Common.Models;
namespace SchoolInformationSystem.Web.Infrastructure
{
    public class DIJsonConverter : CustomCreationConverter<object>
    {
		private IModelCreator _creator;
		public DIJsonConverter(IModelCreator creator)
		{
			_creator = creator;
		}
        public override object Create(Type objectType)
        {
            Console.WriteLine("Creating"); 
            return _creator.LoadModel(objectType);
        }
    }
}
