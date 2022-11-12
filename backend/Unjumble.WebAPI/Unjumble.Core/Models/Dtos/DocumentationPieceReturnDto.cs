using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class DocumentationPieceReturnDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Purpose { get; set; }
    }
}
