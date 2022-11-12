using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Unjumble.Core.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body, bool isHTML, string fullName);
    }
}
