using System;
using System.Collections.Generic;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class DocumentationReturnDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PdfUrl { get; set; }

        public int StarCount { get; set; }

        public IEnumerable<DocumentationPieceReturnDto> Pieces { get; set; }
    }
}
