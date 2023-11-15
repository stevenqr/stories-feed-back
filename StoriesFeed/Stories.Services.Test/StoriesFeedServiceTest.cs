using FluentAssertions;
using Stories.Services.Test.Builders;
using Stories.Services.Test.Mocks;
using StoriesFeed.Domain.Helpers;
using StoriesFeed.Domain.Models;
using StoriesFeed.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Stories.Services.Test
{
    public class StoriesFeedServiceTest
    {
        private readonly StoriesFeedService storiesFeedService;
        private readonly HackerNewsApiServiceMock hackerNewsApiServiceMock;
        private List<Story> storiesFeed;
        private Exception exception;

        public StoriesFeedServiceTest()
        {
            this.hackerNewsApiServiceMock = new HackerNewsApiServiceMock();
            this.storiesFeedService = new StoriesFeedService(this.hackerNewsApiServiceMock.Object);
        }

        [Fact]
        public void GetStoriesFeed_GivenStoriesRequest_ThenGetStoriesFeedFromHackerNewsApi()
        {            
            GivenGetNewStoriesIds();
            GivenGetItem();

            WhenGetStoriesFeed();

            ThenGetStoriesFeed();
        }

        [Fact]
        public void GetStoriesFeed_GivenRequestGetNewStoriesIdsFails_ThenExceptionIsThrown()
        {
            GivenGetNewStoriesIdsThrownsException();            

            WhenGetStoriesFeedException();

            ThenExceptionMessageContains();
        }

        private void ThenExceptionMessageContains()
        {
            this.exception.Message.Should().NotBeNull();
            this.exception.Message.Contains(ExceptionMessages.ExceptionMessage);
        }

        private void WhenGetStoriesFeedException()
        {
            this.exception = Assert.ThrowsAsync<Exception>(() => this.storiesFeedService.GetStoriesFeed()).Result;
        }

        private void GivenGetNewStoriesIdsThrownsException()
        {
            this.hackerNewsApiServiceMock.MockGetNewStoriesIdsThrownsException();
        }

        private void GivenGetNewStoriesIds()
        {
            this.hackerNewsApiServiceMock.MockGetNewStoriesIds(HackerNewsApiServiceBuilder.BuildNewStoriesIds());
        }

        private void GivenGetItem()
        {
            this.hackerNewsApiServiceMock.MockGetItem(HackerNewsApiServiceBuilder.BuildItem());
        }

        private void WhenGetStoriesFeed()
        {
           this.storiesFeed =  this.storiesFeedService.GetStoriesFeed().Result;
        }

        private void ThenGetStoriesFeed()
        {
            this.storiesFeed.Should().NotBeNull();
            this.storiesFeed.Any().Should().BeTrue();
        }
    }
}
