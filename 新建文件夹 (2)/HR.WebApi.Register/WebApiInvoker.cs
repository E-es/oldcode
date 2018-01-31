using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web.Http;
using Newtonsoft.Json;
using HR.WebApi;

namespace HR.WebApi.Register
{
    public class WebApiInvoker
    {
        private readonly string _apiBaseAddress;

        public WebApiInvoker(string apiBaseAddress)
        {
            _apiBaseAddress = apiBaseAddress;
        }

        public TResult InvokeGetRequest<TResult>(string api)
        {
            using (var invoker = CreateMessageInvoker())
            {
                using (var cts = new CancellationTokenSource())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, _apiBaseAddress + api);
                    using (HttpResponseMessage response = invoker.SendAsync(request, cts.Token).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;
                            return JsonConvert.DeserializeObject<TResult>(result);
                        }
                        throw new HttpResponseException(response);
                    }
                }
            }
        }

        public TResult InvokePostRequest<TResult, TArguemnt>(string api, TArguemnt arg)
        {
            var invoker = CreateMessageInvoker();
            using (var cts = new CancellationTokenSource())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _apiBaseAddress + api);
                request.Content = new ObjectContent<TArguemnt>(arg, new JsonMediaTypeFormatter());
                using (HttpResponseMessage response = invoker.SendAsync(request, cts.Token).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<TResult>(result);  
                    }
                    throw new HttpResponseException(response);
                }
            }
        }

        private HttpMessageInvoker CreateMessageInvoker()
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            var server = new HttpServer(config);
            var messageInvoker = new HttpMessageInvoker(server);
            return messageInvoker;
        }
    }
}