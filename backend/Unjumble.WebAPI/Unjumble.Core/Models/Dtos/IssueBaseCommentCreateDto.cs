using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class IssueBaseCommentCreateDto
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int IssueBaseId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public byte Code { get; set; }
    }
}
