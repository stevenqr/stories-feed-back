using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Stories.API.Test.Controllers
{
    [Collection("Api UnitTests")]
    public class BaseApiTest
    {
        protected HttpResponseMessage Response;
        private string responseContent;

        protected string ResponseContent
        {
            get
            {
                if (this.ResponseContent == null)
                {
                    this.responseContent = this.Response.Content.ReadAsStringAsync().Result;
                }

                return this.responseContent;
            }
        }

        protected T GetModelFromResponse<T>()
        {
            var model = JsonConvert.DeserializeObject<T>(ResponseContent);
            return model;
        }
    }
}
