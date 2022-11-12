using System;
using System.Collections.Generic;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class UserReturnDto
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string PhotoUrl { get; set; }

        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Email { get; set; }
    }
}
