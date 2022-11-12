using System;
using System.Collections.Generic;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class LoginTokenResponseDto
    {
        public string AccessToken { get; set; }

        public DateTime ExpiryDate { get; set; }

        public LoginTokenResponseDto()
        {

        }

        public LoginTokenResponseDto(string token, DateTime expires)
        {
            AccessToken = token;
            ExpiryDate = expires;
        }
    }
}
