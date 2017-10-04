using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWeb.Models.Domain
{
    public class User
    {
        public System.Guid UserRegistrationId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool isActive { get; set; }
        public System.DateTime creatTs { get; set; }
        public int userType { get; set; }
        public int TotalNewsUploaded { get; set; }
        public int TotalNewsApproved { get; set; }
    }
}