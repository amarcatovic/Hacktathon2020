using System;
using System.Collections.Generic;
using System.Text;

namespace Unjumble.Core.Models.Domain
{
    public class UserCompany
    {
        public User User { get; set; }

        public string UserId { get; set; }

        public string ComapnyId { get; set; }

        public Company Company { get; set; }

        public bool IsAproved { get; set; }

        public DateTime RequestDate { get; set; }

        public UserCompany()
        {
            IsAproved = false;
            RequestDate = DateTime.Now;
        }
    }
}
