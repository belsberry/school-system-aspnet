using System;
namespace SchoolInformationSystem.Common.Models
{
	public interface IModelCreator
	{
		T LoadModel<T>();
		object LoadModel(Type type);
	}
}