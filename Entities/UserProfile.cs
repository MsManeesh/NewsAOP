using System;
using System.Collections.Generic;

namespace Entities
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<News> NewsList { get; set; }
    }
}
