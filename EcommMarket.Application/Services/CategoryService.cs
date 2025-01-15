using AutoMapper;
using EcommMarket.Application.Dto;
using EcommMarket.Application.Interfaces;
using ECommMarket.Persistence.Interface;
using ECommMarket.Persistence.Repositories;

namespace EcommMarket.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        IEnumerable<ECommMarket.Domain.Entities.Category> enumerable = await categoryRepository.GetAllAsync();
        List<CategoryDto> categories = enumerable.Select(x => new CategoryDto()
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();

        return categories;
    }
}
