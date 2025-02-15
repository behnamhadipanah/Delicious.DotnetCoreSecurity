using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using System.Security.Cryptography;

namespace SecurityApp.Api.Algorithms
{
    public class CustomAuthenticatedEncryptor : IAuthenticatedEncryptor
    {
        readonly SymmetricAlgorithm _symmetricAlgorithm;
        public CustomAuthenticatedEncryptor(SymmetricAlgorithm symmetricAlgorithm)
        {
            _symmetricAlgorithm = symmetricAlgorithm;
        }


        public byte[] Decrypt(ArraySegment<byte> ciphertext, ArraySegment<byte> additionalAuthenticatedData)
        {
            using var dencryptor = _symmetricAlgorithm.CreateDecryptor(_symmetricAlgorithm.Key, _symmetricAlgorithm.IV);
            using var memoryStream = new MemoryStream(ciphertext.Array,ciphertext.Offset,ciphertext.Count);
            using var cryptoStream = new CryptoStream(memoryStream, dencryptor, CryptoStreamMode.Read);
            using var binaryReader = new BinaryReader(cryptoStream);


            byte[] result = binaryReader.ReadBytes(ciphertext.Count);
            return result;
        }

        public byte[] Encrypt(ArraySegment<byte> plaintext, ArraySegment<byte> additionalAuthenticatedData)
        {
            using var encryptor = _symmetricAlgorithm.CreateEncryptor(_symmetricAlgorithm.Key, _symmetricAlgorithm.IV);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using var binaryWriter = new BinaryWriter(cryptoStream);

            binaryWriter.Write(plaintext.Array, plaintext.Offset, plaintext.Count);
            binaryWriter.Flush();
            cryptoStream.FlushFinalBlock();
            
            byte[] result = memoryStream.ToArray();
            return result;

        }
    }
}
