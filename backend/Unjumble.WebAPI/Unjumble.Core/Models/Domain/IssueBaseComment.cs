using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unjumble.Core.Models.Domain
{
    public class IssueBaseComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public IssueBase IssueBase { get; set; }

        [Required]
        public int IssueBaseId { get; set; }

        public User User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public byte Code { get; set; }
    }
}
