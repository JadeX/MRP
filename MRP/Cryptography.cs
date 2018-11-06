using System;
using System.Linq;
using System.Security.Cryptography;
using MRP.Xml;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace MRP
{
    public class Cryptography
    {
        public Cryptography(string secretKey, string variantKey = null)
        {
            if (!string.IsNullOrEmpty(secretKey))
            {
                SecretKey = Convert.FromBase64String(secretKey);
            }

            if (variantKey != null)
            {
                VariantKey = Convert.FromBase64String(variantKey);
            }
            else
            {
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(VariantKey);
                }
            }
        }

        public byte[] AuthenticationKey => Hmac_Sha256(SecretKey, PrivateEncryptionKey.Concat(new byte[] { 0x02 }).ToArray());

        public byte[] EncryptionKey => Hmac_Sha256(PrivateEncryptionKey, VariantKey);

        public byte[] PrivateEncryptionKey => Hmac_Sha256(SecretKey, new byte[] { 0x01 });

        public byte[] SecretKey { get; set; }

        public byte[] VariantKey { get; set; } = new byte[32];

        public byte[] Hmac_Sha256(byte[] key, byte[] payload)
        {
            using (var hash = new HMACSHA256(key))
            {
                return hash.ComputeHash(payload);
            }
        }

        public byte[] EncryptData(byte[] data) => EncryptOrDecryptData(true, data);

        public byte[] DecryptData(byte[] data) => EncryptOrDecryptData(false, data);

        private byte[] EncryptOrDecryptData(bool encrypt, byte[] data)
        {
            byte[] iV;

            using (var sha = SHA256.Create())
            {
                iV = sha.ComputeHash(VariantKey).Take(16).ToArray();
            }

            var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");

            cipher.Init(encrypt, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", EncryptionKey), iV));

            return cipher.DoFinal(data);
        }

        public string SignData(byte[] data) => Convert.ToBase64String(Hmac_Sha256(AuthenticationKey, data));

        public bool HasValidSignature(MrpEnvelope envelope)
        {
            var encodingParams = Convert.FromBase64String(envelope.EncodedBody.EncodingParams);
            var encodedData = Convert.FromBase64String(envelope.EncodedBody.EncodedData);

            return envelope.EncodedBody.AuthCode == SignData(encodingParams.Concat(encodedData).ToArray());
        }
    }
}
