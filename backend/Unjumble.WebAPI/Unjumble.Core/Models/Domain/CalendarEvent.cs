using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Unjumble.Core.Models.Domain
{
    public class CalendarEvent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime EventStartDate { get; set; }

        public DateTime EventEndDate { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public User User { get; set; }

        [Required]
        public string UserId { get; set; }

        public Company Company { get; set; }

        public string CompanyId { get; set; }
    }
}
