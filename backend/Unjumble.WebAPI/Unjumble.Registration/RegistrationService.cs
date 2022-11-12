using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Unjumble.Core.Interfaces;
using Unjumble.Core.Models;
using Unjumble.Core.Models.Domain;
using Unjumble.Core.Models.Dtos;
using Unjumble.Data;

namespace Unjumble.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IActivationService _activationService;
        private readonly IEmailService _mailService;
        private readonly ApplicationDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public RegistrationService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IActivationService activationService,
            IEmailService mailService,
            ApplicationDbContext context,
            ICloudinaryService cloudinaryService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _activationService = activationService;
            _mailService = mailService;
            _context = context;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<bool> RegisterUserAsync(RegistrationDto registrationDto)
        {
            var userByEmail = await _userManager.FindByEmailAsync(registrationDto.Email);
            if (userByEmail != null)
                return false;

            var user = new User()
            {
                FullName = registrationDto.Name,
                Email = registrationDto.Email,
                UserName = registrationDto.Email,
                PhotoUrl = registrationDto.PhotoUrl
            };

            try
            {
                var createdUser = await _userManager.CreateAsync(user, registrationDto.Password);
                if (createdUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    var activationCode = await _activationService.AddActivationAsync(user.Email);
                    await _mailService.SendEmailAsync(user.Email, "Welcome to Unjumble", activationCode.Id, true, user.FullName);
                }

                return createdUser.Succeeded;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> ActivateUserAccountAsync(string activationCode)
        {
            var activationCodeFromDb = await _activationService.GetActivationCodeAsync(activationCode);
            if (activationCodeFromDb != null && activationCodeFromDb.ExpiryDate > DateTime.Now)
            {
                var userByEmail = await _userManager.FindByEmailAsync(activationCodeFromDb.Email);
                if (userByEmail != null)
                {
                    userByEmail.EmailConfirmed = true;
                    await _userManager.UpdateAsync(userByEmail);
                    _activationService.DeleteActivationCode(activationCodeFromDb);
                    return true;
                }
            }

            return false;
        }
    }
}
