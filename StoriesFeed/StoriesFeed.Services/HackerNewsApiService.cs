using Microsoft.Extensions.Configuration;
using StoriesFeed.Domain.Dto;
using StoriesFeed.Domain.Enum;
using StoriesFeed.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace StoriesFeed.Services
{
    public class HackerNewsApiService : IHackerNewsApiService
    {
        private readonly IConfiguration configuration;        
        private readonly IHttpClientFactory httpClientFactory;

        public HackerNewsApiService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<Item> GetItem(int itemId)
        {            
            var httpClient = httpClientFactory.CreateClient();

            return await httpClient.GetFromJsonAsync<Item>(
                $"{SetHackerNewsApiUrl(UrlTypeEnum.GetItem)}/{itemId}.json",
                new JsonSerializerOptions(defaults: JsonSerializerDefaults.Web));
        }

        public async Task<List<int>> GetNewStoriesIds()
        {
            var httpClient = httpClientFactory.CreateClient();

            return await httpClient.GetFromJsonAsync<List<int>>(
                $"{SetHackerNewsApiUrl(UrlTypeEnum.GetNewStoriesIds)}.json",
                new JsonSerializerOptions(defaults: JsonSerializerDefaults.Web));
        }

        private string SetHackerNewsApiUrl(UrlTypeEnum urlTypeEnum)
        {
            string path;

            switch (urlTypeEnum)
            {
                case UrlTypeEnum.GetItem:
                    path = this.configuration["HackerApi:getItemPath"];
                    break;
                case UrlTypeEnum.GetNewStoriesIds:
                    path = this.configuration["HackerApi:getNewStoriesIdsPath"];
                    break;
                default:
                    throw new NotImplementedException();                    
            }

            return $"{this.configuration["HackerApi:baseUrl"]}/{path}";
        }
    }
}
