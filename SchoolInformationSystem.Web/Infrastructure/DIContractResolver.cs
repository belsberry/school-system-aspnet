using System;
using Newtonsoft.Json.Serialization;
using SchoolInformationSystem.Common.Models;
namespace SchoolInformationSystem.Web.Infrastructure
{
	public class DIContractResolver : CamelCasePropertyNamesContractResolver
	{
		private IModelCreator _creator;
		public DIContractResolver(IModelCreator creator)
		{
			_creator = creator;
		}
		protected override JsonObjectContract CreateObjectContract(Type objectType)
		{
			var contract = base.CreateObjectContract(objectType);
			contract.DefaultCreator = () => {
				return _creator.LoadModel(objectType);
			};
			return contract;
		}
	}
}