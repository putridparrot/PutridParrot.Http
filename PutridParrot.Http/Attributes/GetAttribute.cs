using System;
using System.Collections.Generic;
using System.Text;

namespace PutridParrot.Http
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GetAttribute : Attribute
    {
    }
}
