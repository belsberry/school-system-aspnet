namespace SchoolInformationSystem.Common.Models
{
	public interface IModelCreator
	{
		T LoadModel<T>();
	}
}