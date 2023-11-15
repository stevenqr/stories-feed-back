using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoriesFeed.API;

namespace Stories.API.Test
{
    public class StoriesFeedTestStartup : Startup
    {
        public static IConfiguration TestConfiguration;

        public StoriesFeedTestStartup(IConfiguration env) : base(env)
        {
            TestConfiguration = env;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            base.Configure(app, env);
        }
    }
}
