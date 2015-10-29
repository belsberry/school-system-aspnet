using System;

namespace SchoolInformationSystem.Common.Models
{
    public class ModelCreator : IModelCreator
    {
		IServiceProvider _provider;
		public ModelCreator(IServiceProvider provider)
		{
			_provider = provider;	
		}
        public T LoadModel<T>()
        {
			return (T)LoadModel(typeof(T));
        }
		public object LoadModel(Type type)
		{
			object model = _provider.GetService(type);
			if(model != null)
			{
				return model;
			}
			else
			{
				return Activator.CreateInstance(type);	
			}
		}
    }
}