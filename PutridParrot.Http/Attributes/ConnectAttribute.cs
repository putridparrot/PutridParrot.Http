using System;

namespace PutridParrot.Http
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConnectAttribute : Attribute
    {
    }
}