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
			return (T)_provider.GetService(typeof(T));
        }
    }
}