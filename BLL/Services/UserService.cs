using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        protected readonly ProjectContext _context;
        protected readonly IMapper _mapper;

        public UserService(ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task BlockUser(int id)
        {
            var result = await _context.Users.Include(t => t.Team).FirstOrDefaultAsync(c => c.Id == id);
            result.Role = "Blocked";
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteUser(int id)
        {
            User item = _mapper.Map<User>(await GetUser(id));
            if (item == null)
                throw new System.Exception("Invalid id");

            _context.Users.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UserDto>> GetAllUsers()
        {
            ICollection<User> users = await _context.Users.Include(t => t.Team).ToListAsync();
            var result = _mapper.Map<ICollection<UserDto>>(users);
            return result;
        }

        public async Task<UserDto> GetUser(int id)
        {
            var result = await _context.Users.Include(t => t.Team).FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> UpdateUser(int id, UserDto item)
        {
            User user = _mapper.Map<User>(await GetUser(id));
            if (user == null)
                throw new System.Exception("Invalid id");
            user.FirstName = item.FirstName;
            user.LastName = item.LastName;
            user.Email = item.Email;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }
    }
}
