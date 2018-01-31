namespace HR.WebApi.Register
{
    public abstract class ApiTestsBase
    {
        public WebApiInvoker ApiInvoker { get; private set; }
        public ApiTestsBase()
        {
            ApiInvoker = new WebApiInvoker(ApiBaseAddress);
        }

        public  string ApiBaseAddress { get { return "http://localhost:/api/"; }}
    }
}