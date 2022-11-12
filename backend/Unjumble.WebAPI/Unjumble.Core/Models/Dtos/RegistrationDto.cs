using System.ComponentModel.DataAnnotations;

namespace Unjumble.Core.Models.Dtos
{
    public class RegistrationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(7)]
        public string Password { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public RegistrationDto()
        {
            PhotoUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/User_icon_2.svg/800px-User_icon_2.svg.png";
        }
    }
}
