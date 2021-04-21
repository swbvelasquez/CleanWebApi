﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CleanWebApi.Core.DTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public String Description { get; set; }
        public bool IsActive { get; set; }
    }
}
