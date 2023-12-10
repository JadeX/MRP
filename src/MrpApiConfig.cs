namespace MRP
{
    using System;
    using SharpCompress.Compressors.Deflate;

    public class MrpApiConfig
    {
        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Default;
        public string SecretKey { get; set; }
        public TimeSpan Timeout { get; set; }
        public string Url { get; set; }
        public bool UseCompression { get; set; }
        public bool UseEncryption => !string.IsNullOrEmpty(this.SecretKey);
    }
}
