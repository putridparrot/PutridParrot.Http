using System;
using System.Collections.Generic;
using System.Text;

namespace PutridParrot.Http
{
    public static class Base64Encoding
    {
        public static string Encode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static string Decode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static string Encode(string text)
        {
            return Encode(Encoding.UTF8.GetBytes(text));
        }

        public static string Decode(string text)
        {
            return Decode(Encoding.UTF8.GetBytes(text));
        }
    }
}
