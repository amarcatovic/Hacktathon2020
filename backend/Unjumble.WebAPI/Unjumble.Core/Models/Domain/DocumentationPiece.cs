using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;

namespace Unjumble.Core.Models.Domain
{
    public class DocumentationPiece
    {
        [Key]
        public int Id { get; set; }

        public Documentation Documentation { get; set; }

        [Required]
        public int DocumentationId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Purpose { get; set; }
    }
}
