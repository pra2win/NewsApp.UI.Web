using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWeb.Models.Domain
{
    public class Constant
    {
        public enum UserType
        {
            User = 1,
            Admin = 2,
            SuperAdmin = 3
        }
    }
}