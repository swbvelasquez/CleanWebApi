using System;
using System.Collections.Generic;
using System.Text;

namespace CleanWebApi.Core.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
