using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unjumble.Core.Models.Domain
{
    public class IssueBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public Company Company { get; set; }

        [Required]
        public string CompanyId { get; set; }

        public User User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public byte Code { get; set; }

        public ICollection<IssueBaseComment> issueBaseComments { get; set; }
    }
}
