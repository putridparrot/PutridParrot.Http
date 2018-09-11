using System;
using Newtonsoft.Json;

namespace PutridParrot.Http.Json
{
    public class JsonTypeGenerator : ITypeGenerator
    {
        public T ToObject<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
