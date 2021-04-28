using System;
using System.Collections.Generic;
using System.Text;

namespace CleanWebApi.Core.QueryFilters
{
    public class PostQueryFilter
    {
        public int? UserId { get; set; } //el ? es nuleable
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
    }
}
