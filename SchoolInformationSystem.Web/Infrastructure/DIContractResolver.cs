using System;
using System.Reflection;
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
		
		
		class DIObjectConstructor
		{
			Type _type;
			IModelCreator _creator;
			public DIObjectConstructor(Type type, IModelCreator creator)
			{
				_type = type;	
				_creator = creator;
			}
			
			public object ConstructObject(params object[] args)
			{
				Console.WriteLine("In Create Object");
				return _creator.LoadModel(_type);
			}
		}
		
		protected override JsonObjectContract CreateObjectContract(Type objectType)
		{
			Console.WriteLine(objectType);	
			var contract = base.CreateObjectContract(objectType);
			
			DIObjectConstructor di = new DIObjectConstructor(objectType, _creator);
			MethodInfo meth = typeof(DIObjectConstructor).GetMethod("ConstructObject");
			ObjectConstructor<object> constructor = (ObjectConstructor<object>)Delegate
				.CreateDelegate(typeof(ObjectConstructor<object>), di, meth, true);
			contract.OverrideCreator = constructor;
			return contract;
		}
	}
}