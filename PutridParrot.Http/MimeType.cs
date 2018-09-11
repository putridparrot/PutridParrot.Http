using System;
using System.Collections.Generic;
using System.Text;

namespace PutridParrot.Http
{
    public class MimeType
    {
        public sealed class Application
        {
            public const string All = "application/*";
            public const string Json = "application/json";
            public const string Xml = "application/xml";
        }

        public sealed class Text
        {
            public const string All = "text/*";
            public const string Plain = "text/plain";
        }

        public sealed class Image
        {
            public const string Gif = "image/gif";
            public const string JPeg = "image/jpeg";
            public const string Png = "image/png";
        }
    }
}
