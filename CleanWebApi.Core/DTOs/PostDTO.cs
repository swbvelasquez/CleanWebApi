using System;
using System.Collections.Generic;
using System.Text;

namespace CleanWebApi.Core.DTOs
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
    }
}
