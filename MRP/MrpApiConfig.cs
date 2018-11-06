using SharpCompress.Compressors.Deflate;

namespace MRP
{
    public class MrpApiConfig
    {
        public bool UseCompression { get; set; } = true;

        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Default;

        public bool UseEncryption => !string.IsNullOrEmpty(SecretKey);

        public string SecretKey { get; set; } = default;

        public string Url { get; set; }
    }
}
