namespace MRP.Tests;

using System;
using MRP;
using Shouldly;
using Xunit;

public class CryptographyTests
{
    /* Values taken from https://www.mrp.cz/software/ucetnictvi/ks/autonomni-rezim.asp#PRIKLAD_VYPOCTU_SIFROVANI_A_AUTENTIZACE */
    private const string EncryptedTextHex = "01-EC-4D-BE-B1-04-CD-38-E9-0A-4E-CC-C5-C5-35-9C-D0-AA-D8-AF";
    private const string OpenTextHex = "01-02-03-04-05-06-07-08-09-0A-0B-0C-0D-0E-0F-10-11-12-13-14";
    private const string SecretKey = "bRtFEufmEgrJyhai6ltDSV9svtpN3Jb/5oWBBYhDJ31=";
    private const string VariantKeyHex = "1F-5A-C7-7E-D3-0C-C0-A5-F7-5B-B0-35-FF-05-66-A5-0D-B2-12-7A-AB-32-D8-62-4E-0D-A4-D4-18-6E-7F-2F";
    private readonly Cryptography crypto;

    public CryptographyTests() => this.crypto = new Cryptography(SecretKey, Convert.ToBase64String(ConvertHexToByteArray(VariantKeyHex)));

    [Fact]
    public void Encryption()
    {
        var encryptedBytes = this.crypto.EncryptData(ConvertHexToByteArray(OpenTextHex));
        BitConverter.ToString(encryptedBytes).ShouldBe(EncryptedTextHex);

        var decryptedBytes = this.crypto.DecryptData(encryptedBytes);
        BitConverter.ToString(decryptedBytes).ShouldBe(OpenTextHex);
    }

    private static byte[] ConvertHexToByteArray(string hex) => Array.ConvertAll(hex.Split('-'), s => Convert.ToByte(s, 16));
}
