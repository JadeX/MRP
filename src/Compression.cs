namespace JadeX.MRP;

using System.IO;
using SharpCompress.Compressors;
using SharpCompress.Compressors.Deflate;

public static class Compression
{
    /// <summary>
    /// Compresses data.
    /// </summary>
    /// <param name="data">Data to compress.</param>
    /// <param name="level">Level of compression.</param>
    /// <returns>Compressed data.</returns>
    public static byte[] Deflate(byte[] data, CompressionLevel level = CompressionLevel.Default)
    {
        using var ms = new MemoryStream();
        using (var zlibStream = new ZlibStream(ms, CompressionMode.Compress, level))
        {
            zlibStream.Write(data, 0, data.Length);
        }

        return ms.ToArray();
    }

    /// <summary>
    /// Decompresses data.
    /// </summary>
    /// <param name="data">Data to decompress.</param>
    /// <returns>Decompressed data.</returns>
    public static byte[] Inflate(byte[] data)
    {
        using var ms = new MemoryStream();
        using (var zlibStream = new ZlibStream(ms, CompressionMode.Decompress))
        {
            zlibStream.Write(data, 0, data.Length);
        }

        return ms.ToArray();
    }
}
