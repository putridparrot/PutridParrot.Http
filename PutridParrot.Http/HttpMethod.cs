namespace PutridParrot.Http
{
    /// <summary>
    /// HTTP request methods 
    /// 
    /// Taken from https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods
    /// & https://www.w3.org/Protocols/rfc2616/rfc2616-sec9.htm
    /// </summary>
    public enum HttpMethod
    {
        Get,
        Head,
        Post,
        Put,
        Delete,
        Trace,
        Connect,
        Options,
        Patch
    }
}
