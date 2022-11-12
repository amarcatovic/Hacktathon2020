using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class CalendarEventCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime EventStartDate { get; set; }

        public DateTime EventEndDate { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public string UserId { get; set; }

        public string CompanyId { get; set; }
    }
}
