using System;
using System.Linq;
using System.Security.Cryptography;
using Shouldly;
using Xunit;

namespace MRP.Tests
{
    public class TestCryptography
    {
        private const string AesIvVectorHex = "09-18-29-E9-98-18-6D-9F-B0-78-72-2E-0B-91-44-06";
        private const string AuthenticationCodeHex = "B5-75-94-6C-75-DC-A6-5C-E0-DA-8A-C2-AC-F4-73-72-34-09-68-99-53-6E-88-05-51-CA-D1-AE-99-DE-F3-6A";
        private const string AuthenticationKeyHex = "5B-DF-74-9A-16-63-DF-20-6A-1E-9E-36-03-96-33-75-92-FD-D8-2F-66-05-CF-3A-F8-D4-D4-54-6B-64-05-06";
        private const string EncryptedTextHex = "01-EC-4D-BE-B1-04-CD-38-E9-0A-4E-CC-C5-C5-35-9C-D0-AA-D8-AF";
        private const string EncryptionKeyHex = "00-06-29-E3-9E-79-F3-F5-2B-05-D8-58-72-40-C3-81-CA-14-A0-EC-17-27-A9-5A-FA-D4-80-EB-D5-6E-1C-40";
        private const string OpenTextHex = "01-02-03-04-05-06-07-08-09-0A-0B-0C-0D-0E-0F-10-11-12-13-14";
        private const string PrivateEncryptionKeyHex = "DE-B5-81-AB-EC-C4-A5-A5-5D-C7-6C-08-A9-75-49-62-BD-A0-54-10-E1-A3-0D-5E-99-05-AD-FA-65-6C-F2-C9";
        private const string SecretKey = "bRtFEufmEgrJyhai6ltDSV9svtpN3Jb/5oWBBYhDJ30=";
        private const string SecretKeyHex = "6D-1B-45-12-E7-E6-12-0A-C9-CA-16-A2-EA-5B-43-49-5F-6C-BE-DA-4D-DC-96-FF-E6-85-81-05-88-43-27-7D";
        private const string SequenceHex = "01-02-03-04-05-06-07-08-09-0A-0B-0C-0D-0E-0F-10-11-12-13-14";
        private const string VariantKeyHex = "1F-5A-C7-7E-D3-0C-C0-A5-F7-5B-B0-35-FF-05-66-A5-0D-B2-12-7A-AB-32-D8-62-4E-0D-A4-D4-18-6E-7F-2F";
        private readonly Cryptography _crypto;

        public TestCryptography() => _crypto = new Cryptography(SecretKey, Convert.ToBase64String(ConvertHexToByteArray(VariantKeyHex)));

        [Fact]
        public void AESCTREncryption()
        {
            var variantKey = ConvertHexToByteArray(VariantKeyHex);

            byte[] iV;

            using (var sha = SHA256.Create())
            {
                iV = sha.ComputeHash(variantKey).Take(16).ToArray();

                AesIvVectorHex.ShouldBe(BitConverter.ToString(iV));
            }

            var inputBytes = ConvertHexToByteArray(OpenTextHex);

            var encryptedBytes = _crypto.EncryptData(inputBytes);

            EncryptedTextHex.ShouldBe(BitConverter.ToString(encryptedBytes));

            var decryptedBytes = _crypto.EncryptData(encryptedBytes);

            inputBytes.ShouldNotBe(encryptedBytes);
            inputBytes.ShouldBe(decryptedBytes);
        }

        [Fact]
        public void Authentication() =>
            AuthenticationCodeHex.ShouldBe(BitConverter.ToString(_crypto.Hmac_Sha256(ConvertHexToByteArray(AuthenticationKeyHex), ConvertHexToByteArray(SequenceHex))));

        [Fact]
        public void VerifyCryptoHexes()
        {
            // Secret key
            SecretKeyHex.ShouldBe(BitConverter.ToString(Convert.FromBase64String(SecretKey)));

            // Private encryption key
            PrivateEncryptionKeyHex.ShouldBe(BitConverter.ToString(_crypto.PrivateEncryptionKey));

            // Authentication key
            AuthenticationKeyHex.ShouldBe(BitConverter.ToString(_crypto.AuthenticationKey));

            // Encryption key
            EncryptionKeyHex.ShouldBe(BitConverter.ToString(_crypto.Hmac_Sha256(ConvertHexToByteArray(PrivateEncryptionKeyHex), ConvertHexToByteArray(VariantKeyHex))));
        }

        private byte[] ConvertHexToByteArray(string hex) => Array.ConvertAll(hex.Split('-'), s => Convert.ToByte(s, 16));
    }
}
