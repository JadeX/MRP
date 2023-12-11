namespace MRP;

using System;
using System.Linq;
using System.Security.Cryptography;
using MRP.Xml;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

public class Cryptography
{
    public Cryptography(string secretKey, string variantKey = null)
    {
        if (!string.IsNullOrEmpty(secretKey))
        {
            this.SecretKey = Convert.FromBase64String(secretKey);
        }

        if (variantKey != null)
        {
            this.VariantKey = Convert.FromBase64String(variantKey);
        }
        else
        {
#if NETSTANDARD2_0
            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(this.VariantKey);
#else
            RandomNumberGenerator.Fill(this.VariantKey);
#endif
        }
    }

    private byte[] AuthenticationKey => HmacSha256(this.SecretKey, [.. this.PrivateEncryptionKey, .. new byte[] { 0x02 }]);

    private byte[] EncryptionKey => HmacSha256(this.PrivateEncryptionKey, this.VariantKey);

    private byte[] PrivateEncryptionKey => HmacSha256(this.SecretKey, [0x01]);

    private byte[] SecretKey { get; set; }

    private byte[] VariantKey { get; set; } = new byte[32];

    /// <summary>
    /// Decrypts envelope data.
    /// </summary>
    /// <param name="data">Data to decrypt.</param>
    /// <returns>Decrypted envelope data.</returns>
    public byte[] DecryptData(byte[] data) => this.EncryptOrDecryptData(false, data);

    /// <summary>
    /// Encrypts envelope data.
    /// </summary>
    /// <param name="data">Data to encrypt.</param>
    /// <returns>Encrypted envelope data.</returns>
    public byte[] EncryptData(byte[] data) => this.EncryptOrDecryptData(true, data);

    /// <summary>
    /// Retrieves variant key used by this cryptography instance.
    /// </summary>
    /// <returns>Base64String representation of used variant key.</returns>
    public string GetVariantKey() => Convert.ToBase64String(this.VariantKey);

    /// <summary>
    /// Creates valid AuthCode signature for data.
    /// </summary>
    /// <param name="data">Data to sign.</param>
    /// <returns>AuthCode signature.</returns>
    public string SignData(byte[] data) => Convert.ToBase64String(HmacSha256(this.AuthenticationKey, data));

    /// <summary>
    /// Verifies whether envelope data match the AuthCode signature.
    /// </summary>
    /// <param name="envelope">Envelope to verify.</param>
    /// <returns>True if valid.</returns>
    public bool VerifyEnvelopeSignature(MrpEnvelope envelope)
    {
        var encodingParams = Convert.FromBase64String(envelope.EncodedBody.EncodingParams);
        var encodedData = Convert.FromBase64String(envelope.EncodedBody.EncodedData);

        return envelope.EncodedBody.AuthCode == this.SignData([.. encodingParams, .. encodedData]);
    }

    private static byte[] HmacSha256(byte[] key, byte[] payload)
    {
        using var hash = new HMACSHA256(key);
        return hash.ComputeHash(payload);
    }

    private byte[] EncryptOrDecryptData(bool encrypt, byte[] data)
    {
        byte[] iV;

        using (var sha = SHA256.Create())
        {
            iV = sha.ComputeHash(this.VariantKey).Take(16).ToArray();
        }

        var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");

        cipher.Init(encrypt, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", this.EncryptionKey), iV));

        return cipher.DoFinal(data);
    }
}
