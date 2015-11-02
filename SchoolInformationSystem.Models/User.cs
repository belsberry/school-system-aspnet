using System;
using System.Linq;
using Newtonsoft.Json;
using SchoolInformationSystem.Common.Data;
using SchoolInformationSystem.Common.Security;

namespace SchoolInformationSystem.Models
{
	public class User : IHaveId
	{
		IEncryption _encryptionProvider;
		public User(IEncryption enc)
		{
			_encryptionProvider = enc;
		}
		
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string LicenseNumber { get; set; }
		public string Email { get; set; }
		public ScopeLevel ScopeLevel { get; set; }
		public Guid SchoolID { get; set; }
		public string UserName { get; set; }
		[NonSerialized]
		private byte[] _passwordHash;
		[JsonIgnore]
		public byte[] PasswordHash { get { return _passwordHash; } set { _passwordHash = value; } }
		[NonSerialized]
		private string _salt;
		[JsonIgnore]
		public string Salt { get { return _salt; } set { _salt = value; } }
		
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