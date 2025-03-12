using System.ComponentModel.DataAnnotations;

namespace TestWebApplication.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
            ApplyFormatInEditMode = true)]
        public DateTime Created_at { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
            ApplyFormatInEditMode = true)]
        public DateTime Updated_at { get; set; }
    }
}
