using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Unjumble.Core.Interfaces;
using Unjumble.Core.Models;
using Unjumble.Core.Models.Dtos;
using Unjumble.Data;

namespace Unjumble.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly DateTime _tokenExpiry;

        public AuthService(
            UserManager<User> userManager,
            IConfiguration configuration,
            IMapper mapper,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
            _tokenExpiry = DateTime.Now.AddDays(30);
        }

        public async Task<LoginResponseDto> AuthenticateUserAsync(LoginDto loginDto)
        {
            var userFromDb = await _userManager.FindByEmailAsync(loginDto.Email);
            if (userFromDb == null || !await _userManager.CheckPasswordAsync(userFromDb, loginDto.Password))
                return null;

            var role = await _userManager.GetRolesAsync(userFromDb);
            var _options = new IdentityOptions();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim("userId", userFromDb.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }
                ),
                Expires = _tokenExpiry,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("TokenSettings:Token").Value.ToString())),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return CrateLoginResponse(token, userFromDb);
        }

        public async Task<List<UserChatReturnDto>> GetUsersForChat(string companyId)
        {
            var users = await _context.UserCompanies
                .Include(us => us.User)
                .Where(us => us.ComapnyId == companyId)
                .ToListAsync();

            var result = new List<UserChatReturnDto>();
            foreach(var user in users)
            {
                var resultItem = new UserChatReturnDto
                {
                    Id = user.UserId,
                    Name = user.User.FullName,
                    PhotoUrl = user.User.PhotoUrl
                };

                result.Add(resultItem);
            }

            return result;
        }

        private LoginResponseDto CrateLoginResponse(string token, User user)
        {
            var tokenResponseDto = new LoginTokenResponseDto(token, _tokenExpiry);
            var userReturnDto = _mapper.Map<UserReturnDto>(user);

            return new LoginResponseDto
            {
                Token = tokenResponseDto,
                User = userReturnDto
            };
        }
    }
}
