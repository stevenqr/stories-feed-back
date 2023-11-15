using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StoriesFeed.Domain.Models;
using StoriesFeed.Services.Interfaces;

namespace StoriesFeed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesFeedController : ControllerBase
    {
        private readonly IStoriesFeedService storiesFeedService;
        private readonly IMemoryCache memoryCache;
        private readonly IConfiguration configuration;

        public StoriesFeedController(IStoriesFeedService storiesFeedService, IMemoryCache memoryCache, IConfiguration configuration)
        {
            this.storiesFeedService = storiesFeedService;
            this.memoryCache = memoryCache;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Story>>> Get()
        {
            var cacheKey = $"{this.configuration["StoriesFeedCache:key"]}";
            List<Story> stories;

            if (!this.memoryCache.TryGetValue(cacheKey, out stories))
            {
                stories = await this.storiesFeedService.GetStoriesFeed();

                TimeSpan expirationInSeconds = TimeSpan.FromSeconds(
                    double.Parse(this.configuration["StoriesFeedCache:expirationInSeconds"])
                );

                this.memoryCache.Set(
                    $"{this.configuration["StoriesFeedCache:key"]}",
                    stories,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(expirationInSeconds)
                );
            }

            return Ok(stories);
        }
    }
}
