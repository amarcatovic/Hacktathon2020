using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class DocumentationPieceCreateDto
    {
        [Required]
        public int DocumentationId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Purpose { get; set; }
    }
}
