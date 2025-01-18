using EcommMarket.Application.Dto;
using EcommMarket.Application.Interfaces;
using ECommMarket.Persistence.Interface;

namespace EcommMarket.Application.Services;

public class PhotoService : IPhotoService
{
    private readonly IPhotoRepository photoRepository;
    public PhotoService(IPhotoRepository photoRepository)
    {
        this.photoRepository = photoRepository;
    }

    public async Task Delete(int id)
    {
        await photoRepository.Delete(id);   
    }

    public async Task<PhotoDto> GetById(int id)
    {
        var photo = await photoRepository.GetByIdAsync(id);

        return new PhotoDto()
        {
            Id = photo.Id,
            PhotoName = photo.PhotoName,
            PhotoUrl = photo.PhotoUrl
        };
    }
}
