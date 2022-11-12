using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class DocumentationCraeteDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile PdfFile { get; set; }

        [Required]
        public string CompanyId { get; set; }
    }
}
