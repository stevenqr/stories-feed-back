using FluentAssertions;
using Newtonsoft.Json;
using Stories.Services.Test.Builders;
using StoriesFeed.Domain.Helpers;
using StoriesFeed.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Stories.API.Test.Controllers
{    
    public class StoriesFeedControllerTest
    {

        public HttpResponseMessage Response;

        [Fact]
        public void StoriesFeed_ShouldGetStoriesFeed()
        {
            using (var storiesFeedTestServer = new StoriesFeedTestServer() )
            {
                var expectedStoriesFeedResponse = StoriesFeedServiceBuilder.BuildStoriesFeed();
                GivenStoriesFeedRequestIsDone(storiesFeedTestServer, expectedStoriesFeedResponse);

                WhenGetStoriesFeed(storiesFeedTestServer);

                ThenStatusCodeIs(HttpStatusCode.OK);
                ThenStoriesFeedResponseIsEquivalentTo(expectedStoriesFeedResponse);
            }
        }

        [Fact]
        public void StoriesFeed_ShouldThrownInternalServerError()
        {
            var exception = new Exception(ExceptionMessages.ExceptionMessage);
            using (var storiesFeedTestServer = new StoriesFeedTestServer())
            {                
                GivenStoriesFeedRequestThrowsException(storiesFeedTestServer, exception);

                WhenGetStoriesFeed(storiesFeedTestServer);

                ThenStatusCodeIs(HttpStatusCode.InternalServerError);
                ThenErrorIs(ExceptionMessages.ExceptionMessage);
            }
        }

        private async void ThenErrorIs(string expectedMessage)
        {
            string responseBody = await this.Response.Content.ReadAsStringAsync();
            responseBody.Contains(expectedMessage);
        }

        private void GivenStoriesFeedRequestThrowsException(StoriesFeedTestServer storiesFeedTestServer, Exception exception)
        {
            storiesFeedTestServer.StoriesFeedServiceMock.Setup(s => s.GetStoriesFeed()).Throws(exception);
        }

        private void GivenStoriesFeedRequestIsDone(StoriesFeedTestServer storiesFeedTestServer, List<Story> expectedStoriesFeedResponse)
        {
            storiesFeedTestServer.StoriesFeedServiceMock
                .Setup(x => x.GetStoriesFeed())
                .Returns(Task.FromResult(expectedStoriesFeedResponse));
        }

        private void WhenGetStoriesFeed(StoriesFeedTestServer storiesFeedTestServer)
        {
            var client = storiesFeedTestServer.testServer.CreateClient();
            client.DefaultRequestHeaders.Add("Accept", "text/plain");
            this.Response = client.GetAsync("api/StoriesFeed").Result;
        }

        private void ThenStatusCodeIs(HttpStatusCode statusCode)
        {
            this.Response.StatusCode.Should().Be(statusCode);            
        }

        private async void ThenStoriesFeedResponseIsEquivalentTo(List<Story> expectedStoriesFeedResponse)
        {
            string responseBody = await this.Response.Content.ReadAsStringAsync();
            var jsonString = JsonConvert.SerializeObject(expectedStoriesFeedResponse);
            responseBody.ToLower().Should().Be(jsonString.ToLower());
        }
    }
}
