using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PutridParrot.Http;

namespace Tests.PutridParrot.Http
{
    [ExcludeFromCodeCoverage]
    [TestFixture]   
    public class HttpConnectTests
    {
        public class HelloResponse
        {
            public string Result { get; set; }
        }

        [Test]
        public void T()
        {
            var httpConnect = new HttpConnect();
            var response = httpConnect.InvokeAsync<HelloResponse>(HttpMethod.Get, "http://127.0.0.1:8088/hello/Mark").Result;

            //Assert.AreEqual();
        }
    }
}
