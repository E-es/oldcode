using System.Web.Http;
using FluentAssertions;
using Xunit;
using HR.WebApi.Data.Entities;

namespace HR.WebApi.Register.AccountTests
{
    public class AccountTest:ApiTestsBase
    {
        [Fact]
        public void Should_get_hello_message()
        {
            var result = ApiInvoker.InvokeGetRequest<string>("hello");

            result.Should().Be("Hello, World");
        }

        [Fact]
        public void Should_regester_an_account(UserModel usermodel)
        {
            var result = ApiInvoker.InvokePostRequest<IHttpActionResult, UserModel>("account/register", usermodel);
        }
    }
}