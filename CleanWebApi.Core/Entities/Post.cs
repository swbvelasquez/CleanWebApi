﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CleanWebApi.Core.Entities
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
