
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace StoriesFeed.Domain.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
    }
}
