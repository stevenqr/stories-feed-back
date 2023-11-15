using Moq;
using StoriesFeed.Domain.Dto;
using StoriesFeed.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stories.Services.Test.Mocks
{
    public class HackerNewsApiServiceMock : Mock<IHackerNewsApiService>
    {
        public void MockGetItem(Item item)
        {
            _ = Setup(x => x.GetItem(It.IsAny<int>())).Returns(Task.FromResult(item));
        }

        public void MockGetNewStoriesIds(List<int> storiesIds)
        {
            _ = Setup(x => x.GetNewStoriesIds()).Returns(Task.FromResult(storiesIds));
        }

        internal void MockGetNewStoriesIdsThrownsException()
        {
            _ = Setup(x => x.GetNewStoriesIds()).Throws(new Exception());
        }
    }
}
