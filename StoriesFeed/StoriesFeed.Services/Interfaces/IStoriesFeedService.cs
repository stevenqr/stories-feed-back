using StoriesFeed.Domain.Models;

namespace StoriesFeed.Services.Interfaces
{
    public interface IStoriesFeedService
    {
        Task<List<Story>> GetStoriesFeed();
    }
}
