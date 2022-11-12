using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Unjumble.Core.Models.Domain
{
    public class Company
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string City { get; set; }

        public string LogoUrl { get; set; }

        public string UserOwnerId { get; set; }

        public User UserOwner { get; set; }

        public ICollection<UserCompany> UserCompanies { get; set; }

        public ICollection<Documentation> Documentation { get; set; }

        public ICollection<IssueBase> IssueBases { get; set; }

        public ICollection<IssueBaseComment> IssueBaseComments { get; set; }

        public ICollection<CalendarEvent> CalendarEvents { get; set; }
    }
}
