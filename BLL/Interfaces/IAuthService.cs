using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        public Task<UserDto> Register(UserRegisterDto employer);

        public Task<UserAuthDto> Login(UserLoginDto employer);
    }
}
