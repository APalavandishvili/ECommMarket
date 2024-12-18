﻿using AutoMapper;
using EcommMarket.Application.Interfaces;
using EcommMarket.Application.Dto;
using ECommMarket.Persistence.Interface;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using ECommMarket.Domain.Entities;

namespace EcommMarket.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<ProductDto> AddAsync(ProductDto entity)
    {
        await productRepository.AddAsync(new()
        {
            Description = entity.Description,
            Price = entity.Price,
            ProductName = entity.ProductName,
            Quantity = entity.Quantity,
            Timestamp = DateTime.Now,
        });

        return entity;
    }

    public async Task Delete(int id)
    {
        await productRepository.Delete(id);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        IEnumerable<ECommMarket.Domain.Entities.Product> enumerable = await productRepository.GetAllAsync();
        List<ProductDto> products = enumerable.Select(x => new ProductDto()
        {
            Id = x.Id,
            Description = x.Description,
            ProductName = x.ProductName,
            Quantity = x.Quantity,
            Price = x.Price,
            Photos = x.Photos?.Select(p => new PhotoDto()
            {
                PhotoName = p.PhotoName,
                PhotoUrl = p.PhotoUrl,
            }).ToList(),
        }).ToList();

        return products;
    }

    public async Task<List<ProductDto>> GetAllByIdAsync(List<int> productIds)
    {
        List<Product> enumerable = await productRepository.GetAllByIdAsync(productIds);
        var c = mapper.Map<List<Product>, List<ProductDto>>(enumerable);
        List<ProductDto> products = enumerable.Select(x => new ProductDto()
        {
            Id = x.Id,
            Description = x.Description,
            ProductName = x.ProductName,
            Quantity = x.Quantity,
            Price = x.Price,
            Photos = x.Photos?.Select(p => new PhotoDto()
            {
                PhotoName = p.PhotoName,
                PhotoUrl = p.PhotoUrl,
            }).ToList(),
        }).ToList();

        return products;
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        ECommMarket.Domain.Entities.Product product = await productRepository.GetByIdAsync(id);
        return mapper.Map<ProductDto>(product);
    }

    public async Task Update(ProductDto entity)
    {
        ECommMarket.Domain.Entities.Product product = await productRepository.GetByIdAsync(entity.Id);
        product.Quantity = entity.Quantity;
        product.Price = entity.Price;
        product.Description = entity.Description;
        product.ProductName = entity.ProductName;
        product.UpdateTimestamp = DateTime.Now;

        await productRepository.Update(product);
    }

    public async Task UpdatePhotos(int productId, List<PhotoDto> photos)
    {
        ECommMarket.Domain.Entities.Product product = await productRepository.GetByIdAsync(productId);
        foreach (PhotoDto photo in photos)
        {
            product.Photos.Add(new()
            {
                PhotoName = photo.PhotoName,
                PhotoUrl = photo.PhotoUrl,
            });
        }

        await productRepository.Update(product);
    }

    public Task UpdatePhotos(List<PhotoDto> photos)
    {
        throw new NotImplementedException();
    }
}
