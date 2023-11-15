using StoriesFeed.Services.Interfaces;
using StoriesFeed.Services;
using StoriesFeed.API.Extensions;

namespace StoriesFeed.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<IStoriesFeedService, StoriesFeedService>();
            services.AddScoped<IHackerNewsApiService, HackerNewsApiService>();
            services.AddMemoryCache();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            app.UseExceptionMiddleware();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x => x
            .AllowAnyOrigin());

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
