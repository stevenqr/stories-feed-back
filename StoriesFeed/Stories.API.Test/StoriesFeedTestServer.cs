using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Moq;
using StoriesFeed.Services.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace Stories.API.Test
{
    public class StoriesFeedTestServer : IDisposable
    {        
        public TestServer testServer;
        public Mock<IStoriesFeedService> StoriesFeedServiceMock;

        public StoriesFeedTestServer()
        {
            this.StoriesFeedServiceMock = new Mock<IStoriesFeedService>();
            var path = Assembly.GetAssembly(typeof(StoriesFeedTestServer)).Location;

            var hostbuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb => 
                    cb.AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables())
                .UseStartup<StoriesFeedTestStartup>()
                .ConfigureTestServices(services =>
                {
                    _= services.AddSingleton(this.StoriesFeedServiceMock.Object);
                });
            this.testServer = new TestServer(hostbuilder);
        }

        public void Dispose()
        {
            this.testServer?.Dispose();
        }
    }
}
