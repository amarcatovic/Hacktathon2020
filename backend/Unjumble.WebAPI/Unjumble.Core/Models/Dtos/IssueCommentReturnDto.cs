using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class IssueCommentReturnDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserName { get; set; }

        public string UserImageUrl { get; set; }

        [Required]
        public byte Code { get; set; }
    }
}
