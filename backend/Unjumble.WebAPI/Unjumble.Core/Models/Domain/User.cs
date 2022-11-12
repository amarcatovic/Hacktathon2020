using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Unjumble.Core.Models.Domain;

namespace Unjumble.Core.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public string PhotoUrl { get; set; }

        public ICollection<UserCompany> UserCompanies { get; set; }

        public ICollection<IssueBase> IssueBases { get; set; }

        public ICollection<IssueBaseComment> IssueBaseComments { get; set; }

        public ICollection<CalendarEvent> CalendarEvents { get; set; }
    }
}
