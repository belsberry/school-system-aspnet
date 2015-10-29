namespace SchoolInformationSystem.Common.Security
{
	public interface IEncryption
	{
		byte[] Hash(string value);
		string GenerateRandomString(int numberOfChars);
	}
}