using System;
using System.Collections.Generic;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class LoginResponseDto
    {
        public LoginTokenResponseDto Token { get; set; }
        public UserReturnDto User { get; set; }
    }
}
