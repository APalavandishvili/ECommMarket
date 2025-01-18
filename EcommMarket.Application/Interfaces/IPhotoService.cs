using EcommMarket.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommMarket.Application.Interfaces;

public interface IPhotoService
{
    Task Delete(int id);
    Task<PhotoDto> GetById(int id);
}
