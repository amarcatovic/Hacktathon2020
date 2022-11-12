using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Core.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> AuthenticateUserAsync(LoginDto loginDto);

        Task<List<UserChatReturnDto>> GetUsersForChat(string companyId);
    }
}
