using System;
using System.ComponentModel.DataAnnotations;

namespace CleanWebApi.Core.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
    }
}
