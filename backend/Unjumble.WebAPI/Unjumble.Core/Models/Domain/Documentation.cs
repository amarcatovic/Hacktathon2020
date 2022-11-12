using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unjumble.Core.Models.Domain
{
    public class Documentation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PdfUrl { get; set; }

        [Required]
        public string Name { get; set; }

        public Company Company { get; set; }

        [Required]
        public string CompanyId { get; set; }

        public int StarCount { get; set; }

        public ICollection<DocumentationPiece> DocumentationPieces { get; set; }
    }
}
