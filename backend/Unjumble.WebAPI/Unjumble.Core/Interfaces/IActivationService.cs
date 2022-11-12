using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unjumble.Core.Models;

namespace Unjumble.Core.Interfaces
{
    public interface IActivationService
    {
        Task<ActivationCode> AddActivationAsync(string email);

        Task<ActivationCode> GetActivationCodeAsync(string id);

        void DeleteActivationCode(ActivationCode code);
    }
}
