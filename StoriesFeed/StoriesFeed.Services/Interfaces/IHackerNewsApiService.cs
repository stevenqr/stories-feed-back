using StoriesFeed.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesFeed.Services.Interfaces
{
    public interface IHackerNewsApiService
    {
        Task<Item> GetItem(int itemId);
        Task<List<int>> GetNewStoriesIds();
    }
}
