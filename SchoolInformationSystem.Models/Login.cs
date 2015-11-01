using SchoolInformationSystem.Common.Security;
using System;
using SchoolInformationSystem.Common.Data;
using System.Linq;
namespace SchoolInformationSystem.Models
{
	public class Login : IHaveId
	{
		private IEncryption _encryptionProvider;
		public Login(IEncryption enc)
		{
			_encryptionProvider = enc;
		}
		
		public Guid Id { get; set; }
		public Guid UserID { get; set; }
		public string UserName { get; set; }
		public byte[] PasswordHash { get; set; }
		public string Salt { get; set; }
		
		public void SetPassword(string newPassword)
		{
			
			string salt = _encryptionProvider.GenerateRandomString(5);
			this.Salt = salt;
			this.PasswordHash = _encryptionProvider.Hash(newPassword + this.Salt);
						
		}
		
		public bool CheckPassword(string password)
		{
			return Enumerable.SequenceEqual(_encryptionProvider.Hash(password + this.Salt), this.PasswordHash);
		}
	}
	
}