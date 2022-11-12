using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class CalendarEventUpdateDto
    {
        [Required]
        public DateTime Pocetni { get; set; }

        [Required]
        public DateTime Krajnji { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
