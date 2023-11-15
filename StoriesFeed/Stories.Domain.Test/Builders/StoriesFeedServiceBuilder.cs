using StoriesFeed.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.Domain.Test.Builders
{
    public class StoriesFeedServiceBuilder
    {
        public static Item BuildItem()
        {
            return new Item()
            {
                By = "dhouston",
                Descendants = 71,
                Id = 8863,
                Kids = new List<int>() 
                {
                    8952, 
                    9224, 
                    8917, 
                    8884, 
                    8887
                },
                Score = 111,
                Time = 1175714200,
                Title = "My YC app: Dropbox - Throw away your USB drive",
                Type = "story",
                Url = "http://www.getdropbox.com/u/2/screencast.html"
            };
        }

        public static List<int> BuildNewStoriesIds()
        {
            return new List<int>()
            {
                38267879,
                38266912,
                38249730,
                38265647,
                38263175,
                38243949,
                38252963,
                38236198
            };
        }
    }
}
