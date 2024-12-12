﻿using AutoMapper;
using EcommMarket.Application.Dto;
using ECommMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommMarket.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Photo, PhotoDto>();
    }
}
