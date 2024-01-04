using StoriesFeed.Domain.Models;
using StoriesFeed.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Text.Json;

namespace StoriesFeed.Services
{
    public class StoriesFeedService : IStoriesFeedService
    {
        private readonly IHackerNewsApiService hackerNewsApiService;

        public StoriesFeedService(IHackerNewsApiService hackerNewsApiService)
        {
            this.hackerNewsApiService = hackerNewsApiService;
        }

        public async Task<List<Story>> GetStoriesFeed()
        {
            List<int> newStoriesIds;

            try
            {
                newStoriesIds = await this.hackerNewsApiService.GetNewStoriesIds();
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (JsonException ex)
            {
                throw new JsonException(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ConcurrentBag<Story> stories = new ConcurrentBag<Story>();

            try
            {
                var tasks = new List<Task>();
                foreach (int storyId in newStoriesIds)
                {
                    tasks.Add(Task.Run(async () => 
                    {
                        var item = await this.hackerNewsApiService.GetItem(storyId);

                        if (item != null && !string.IsNullOrEmpty(item.Url))
                        {
                            stories.Add(new()
                            {
                                Id = storyId,
                                Title = item.Title,
                                Link = item.Url
                            });
                        }
                    }));                    
                }
                
                await Task.WhenAll(tasks);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (JsonException ex)
            {
                throw new JsonException(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return stories.ToList();
        }
    }
}
