using System;
using System.Text;
using System.Security.Cryptography;

namespace SchoolInformationSystem.Common.Security
{
    public class Encryption : IEncryption
    {
        public string GenerateRandomString(int numberOfChars)
        {
            string valids = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			StringBuilder sb = new StringBuilder();
			Random rnd = new Random();
			for(int i =0; i < numberOfChars; i++)
			{
				sb.Append(valids[rnd.Next(valids.Length)]);
			}
			return sb.ToString();
        }

        public byte[] Hash(string value)
        {
            SHA256 hashAlg = SHA256.Create();
			return hashAlg.ComputeHash(Encoding.UTF8.GetBytes(value));
        }
    }
}