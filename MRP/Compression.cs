using System.IO;
using SharpCompress.Compressors;
using SharpCompress.Compressors.Deflate;

namespace MRP
{
    public class Compression
    {
        public static byte[] Inflate(byte[] data) => Zlib(data, CompressionMode.Decompress);

        public static byte[] Deflate(byte[] data, CompressionLevel level = CompressionLevel.Default) => Zlib(data, CompressionMode.Compress, level);

        private static byte[] Zlib(byte[] data, CompressionMode mode, CompressionLevel level = CompressionLevel.Default)
        {
            using (var ms = new MemoryStream())
            {
                using (var zlibStream = new ZlibStream(ms, mode, level))
                {
                    zlibStream.Write(data, 0, data.Length);
                }
                return ms.ToArray();
            }
        }
    }
}
