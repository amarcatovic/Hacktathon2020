using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class CalendarEventReturnDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime EventStartDate { get; set; }

        public DateTime EventEndDate { get; set; }

        public string UserName { get; set; }

        public string UserPhotoUrl { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public string CompanyName { get; set; }

        public string CompanyId { get; set; }
    }
}
