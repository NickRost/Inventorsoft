using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.Models;
using DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private protected readonly ProjectContext _context;
        private protected readonly IMapper _mapper;
        private protected readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserAuthDto> Login(UserLoginDto user)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (userEntity == null)
            {
                throw new Exception(nameof(user));
            }

            if (!VerifyPasswordHash(user.Password, userEntity.PasswordHash, userEntity.PasswordSalt))
            {
                throw new Exception("Wrong password");
            }

            UserAuthDto authDto = new UserAuthDto
            {
                User = _mapper.Map<UserDto>(userEntity),
                AccessToken = CreateToken(userEntity)
            };
            return authDto;
        }

        public async Task<UserDto> Register(UserRegisterDto user)
        {
            User newUser = _mapper.Map<User>(user);
            newUser.Role = "User";
            newUser.RegisteredAt = DateTime.Now;

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(newUser);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
