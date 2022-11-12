using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Unjumble.Core.Interfaces;
using Unjumble.Core.Models;
using Unjumble.Data;

namespace Unjumble.Activation
{
    public class ActivationService : IActivationService
    {
        private readonly ApplicationDbContext _context;

        public ActivationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActivationCode> AddActivationAsync(string email)
        {
            var activationCode = new ActivationCode
            {
                Email = email
            };

            await _context.ActivationCodes.AddAsync(activationCode);
            await _context.SaveChangesAsync();

            return activationCode;
        }

        public void DeleteActivationCode(ActivationCode code)
        {
            _context.ActivationCodes.Remove(code);
        }

        public async Task<ActivationCode> GetActivationCodeAsync(string id)
        {
            return await _context.ActivationCodes.SingleOrDefaultAsync(a => a.Id == id);
        }
    }
}
