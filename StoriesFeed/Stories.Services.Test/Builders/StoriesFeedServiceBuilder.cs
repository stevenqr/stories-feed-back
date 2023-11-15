using StoriesFeed.Domain.Models;
using System.Collections.Generic;

namespace Stories.Services.Test.Builders
{
    public class StoriesFeedServiceBuilder
    {
        public static List<Story> BuildStoriesFeed()
        {
            List<Story> stories = new List<Story>()
            {
                new Story
                {
                    Id = 38267879,
                    Title = "My YC app: Dropbox - Throw away your USB drive",
                    Link = "http://www.getdropbox.com/u/2/screencast.html"
                },
                new Story
                {
                    Id = 38271252,
                    Title = "Who should pay for open-access publishing? APC alternatives emerge",
                    Link = "https://www.nature.com/articles/d41586-023-03506-4"
                },
                new Story
                {
                    Id = 38270745,
                    Title = "Vatnik",
                    Link = "https://en.wikipedia.org/wiki/Vatnik"
                }
            };
            return stories;
        }
    }
}
