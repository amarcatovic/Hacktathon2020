using System;
using System.Collections.Generic;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class IssueBaseReturnDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserName { get; set; }

        public string UserImageUrl { get; set; }

        public byte Code { get; set; }

        public IEnumerable<IssueCommentReturnDto> Comments { get; set; }
    }
}
