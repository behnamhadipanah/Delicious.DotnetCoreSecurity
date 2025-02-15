using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Cryptography;

namespace SecurityApp.Api.Algorithms
{
    public class CustomAuthenticatedEncryptorFactory : IAuthenticatedEncryptorFactory
    {
        public IAuthenticatedEncryptor? CreateEncryptorInstance(IKey key)
        {
            //var algorithm=new SymmetricAlgorithm()
            //{
            //    Key=new byte[],
            //    IV=new byte[]
            //};
            //return new CustomAuthenticatedEncryptor(algorithm);


            return new CustomAuthenticatedEncryptor(null);
        }
    }
}
