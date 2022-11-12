using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unjumble.Core.Models.Dtos;

namespace Unjumble.Core.Interfaces
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUserAsync(RegistrationDto registrationDto);

        Task<bool> ActivateUserAccountAsync(string activationCode);
    }
}
