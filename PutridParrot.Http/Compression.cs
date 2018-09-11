using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace PutridParrot.Http
{
    /// <summary>
    /// 
    /// https://stackoverflow.com/questions/7343465/compression-decompression-string-with-c-sharp
    /// </summary>
    public class Compression
    {
        public byte[] Compress(string data)
        {
            using (var bytesStream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var gs = new GZipStream(memoryStream, CompressionMode.Compress))
                    {
                        CopyTo(bytesStream, gs);
                    }

                    return memoryStream.ToArray();
                }
            }
        }

        public string Decompress(byte[] bytes)
        {
            using (var bytesStream = new MemoryStream(bytes))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var gs = new GZipStream(bytesStream, CompressionMode.Decompress))
                    {
                        CopyTo(gs, memoryStream);
                    }

                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }

        private static void CopyTo(Stream from, Stream to)
        {
            var bytes = new byte[4096];
            int i;
            while ((i = from.Read(bytes, 0, bytes.Length)) != 0)
            {
                to.Write(bytes, 0, i);
            }
        }
    }

}
