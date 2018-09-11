using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PutridParrot.Http
{
    public class HttpConnect
    {
        private readonly IDictionary<string, ITypeGenerator> _contentGenerators;

        public HttpConnect()
        {
            _contentGenerators = new Dictionary<string, ITypeGenerator>
            {
                //{MimeType.Application.Json, new JsonTypeGenerator()}
            };
        }

        public IDictionary<string, ITypeGenerator> ContentGenerators =>
            _contentGenerators;

        public Task<HttpResponse<T>> InvokeAsync<T>(HttpMethod method, string url, string request = null)
        {
            return InvokeAsync<T>(method.ToString(), url, request);
        }

        public Task<HttpResponse<T>> InvokeAsync<T>(string method, string url, string request = null)
        {
            var httpClient = new HttpClient();

            foreach (var acceptType in _contentGenerators.Keys)
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptType));
            }

            OnBeforeRequest(httpClient);

            switch (method.ToUpper())
            {
                case "GET":
                    return InvokeGet<T>(httpClient, url);
                case "HEAD":
                    return InvokeHead<T>(httpClient, url);
                case "POST":
                    return InvokePost<T>(httpClient, url, request);
                case "PUT":
                    return InvokePut<T>(httpClient, url, request);
                case "DELETE":
                    return InvokeDelete<T>(httpClient, url);
                default:
                    return InvokeGet<T>(httpClient, url);
            }
        }

        protected virtual void OnBeforeRequest(HttpClient request)
        {
            //var ssoToken = _singleSignOnService.RequestToken(_applicationContext.SsoTokenDetails.Application).FirstOrDefaultAsync().Wait();
            //request.Headers.Add("sso_token", ssoToken.TokenString);
            //request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
        }

        protected virtual void OnAfterResponse(HttpClient response)
        {
        }

        private async Task<HttpResponse<T>> InvokeHead<T>(HttpClient httpClient, string url)
        {
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStreamAsync();

            return HandleResponse<T>(httpClient, response, result, response.Content.Headers);
        }


        private async Task<HttpResponse<T>> InvokeGet<T>(HttpClient httpClient, string url)
        {
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStreamAsync();

            return HandleResponse<T>(httpClient, response, result, response.Content.Headers);
        }

        private async Task<HttpResponse<T>> InvokeDelete<T>(HttpClient httpClient, string url)
        {
            var response = await httpClient.DeleteAsync(url);
            var result = await response.Content.ReadAsStreamAsync();

            return HandleResponse<T>(httpClient, response, result, response.Content.Headers);
        }

        private async Task<HttpResponse<T>> InvokePut<T>(HttpClient httpClient, string url, string request)
        {
            var content = new StringContent(request);
            content.Headers.ContentType = new MediaTypeHeaderValue(MimeType.Application.Json);
            content.Headers.ContentLength = request.Length;

            var response = await httpClient.PutAsync(url, content);
            var result = await response.Content.ReadAsStreamAsync();

            return HandleResponse<T>(httpClient, response, result, response.Content.Headers);
        }

        private async Task<HttpResponse<T>> InvokePost<T>(HttpClient httpClient, string url, string request)
        {
            var content = new StringContent(request);
            content.Headers.ContentType = new MediaTypeHeaderValue(MimeType.Application.Json);
            content.Headers.ContentLength = request.Length;

            var response = await httpClient.PostAsync(url, content);
            var result = await response.Content.ReadAsStreamAsync();

            return HandleResponse<T>(httpClient, response, result, response.Content.Headers);
        }

        private HttpResponse<T> HandleResponse<T>(
            HttpClient httpClient,
            HttpResponseMessage response,
            Stream stream,
            HttpContentHeaders headers)
        {
            OnAfterResponse(httpClient);

            var resultString = String.Empty;
            if (stream != null)
            {
                if (response.Content.Headers.ContentEncoding.Contains("gzip"))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        var compression = new Compression();
                        resultString = compression.Decompress(memoryStream.ToArray());
                    }
                }
                else
                {
                    var streamReader = new StreamReader(stream);
                    resultString = streamReader.ReadToEnd();
                }
            }

            return CreateReponse<T>(
                resultString,
                response.StatusCode,
                response.ReasonPhrase,
                headers.ContentType.MediaType);
        }

        private HttpResponse<T> CreateReponse<T>(
            string response,
            HttpStatusCode statusCode,
            string statusDescription,
            string contentType)
        {
            return new HttpResponse<T>(
                _contentGenerators.ContainsKey(contentType) ?
                    _contentGenerators[contentType].ToObject<T>(response) :
                    default(T),
                statusCode,
                statusDescription);
        }
    }
}
