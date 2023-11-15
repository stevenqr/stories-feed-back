using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StoriesFeed.Domain.Dto;
using StoriesFeed.Domain.Enum;
using StoriesFeed.Services.Interfaces;

namespace StoriesFeed.Services
{
    public class HackerNewsApiService : IHackerNewsApiService
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient = new HttpClient();

        public HackerNewsApiService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<Item> GetItem(int itemId)
        {                
            HttpResponseMessage response = await httpClient.GetAsync($"{SetHackerNewsApiUrl(UrlTypeEnum.GetItem)}/{itemId}.json");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Item>(responseBody);
        }

        public async Task<List<int>> GetNewStoriesIds()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{SetHackerNewsApiUrl(UrlTypeEnum.GetNewStoriesIds)}.json");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<int>>(responseBody);
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
