using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<UserDto>> GetAllUsers();
        Task<UserDto> GetUser(int id);
        Task<UserDto> UpdateUser(int id, UserDto user);
        Task DeleteUser(int id);
        Task BlockUser(int id);
    }
}
