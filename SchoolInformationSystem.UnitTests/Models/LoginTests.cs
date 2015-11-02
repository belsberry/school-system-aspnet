using Xunit;
using Moq;
using SchoolInformationSystem.Common.Security;
using SchoolInformationSystem.Models;

namespace SchoolInformationSystem.UnitTests.Models
{
	public class LoginTests
	{
		[Fact]
		public void ShouldSetAndCheckPassword()
		{
			//Arrange
			Mock<IEncryption> encryptionMock = new Mock<IEncryption>();
			encryptionMock.Setup(x => x.GenerateRandomString(It.IsAny<int>())).Returns("12345");
			encryptionMock.Setup(x => x.Hash(It.IsAny<string>())).Returns(new byte[]{ 1, 2, 3, 4 });
			
			User login = new User(encryptionMock.Object);
			
			//act
			login.SetPassword("password");
			
			//assert
			encryptionMock.Verify(y => y.Hash("password12345"), Times.Once);
			encryptionMock.Verify(y => y.GenerateRandomString(5), Times.Once);
			Assert.Equal(login.Salt, "12345");
			Assert.Equal(login.PasswordHash, new byte[] { 1, 2, 3, 4});
			
			//act
			bool isLogged = login.CheckPassword("password");
			
			//assert
			encryptionMock.Verify(y => y.Hash("password12345"), Times.Exactly(2));
			Assert.True(isLogged);
		}
		
		[Fact]
		public void ShouldFailIfCheckPasswordDifferent()
		{
			//Arrange
			Mock<IEncryption> encryptionMock = new Mock<IEncryption>();
			encryptionMock.Setup(x => x.GenerateRandomString(It.IsAny<int>())).Returns("12345");
			
			encryptionMock.Setup(x => x.Hash(It.IsIn(new string[] { "password12345" }))).Returns(new byte[]{ 1, 2, 3, 4 });
			encryptionMock.Setup(x => x.Hash(It.IsNotIn(new string[] { "password12345" }))).Returns(new byte[]{ 4, 3, 2, 1 });
			
			User login = new User(encryptionMock.Object);
			
			//act
			login.SetPassword("password");
			bool isLogged = login.CheckPassword("notpassword");
			
			//Assert
			encryptionMock.Verify(y => y.Hash("password12345"), Times.Once);
			encryptionMock.Verify(y => y.Hash("notpassword12345"), Times.Once);
			Assert.False(isLogged);
			
			
		}
	}
}