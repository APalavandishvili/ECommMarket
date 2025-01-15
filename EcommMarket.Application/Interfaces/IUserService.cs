using EcommMarket.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommMarket.Application.Interfaces;

public interface IUserService
{
    Task<string> Login(LoginDto request);
}
