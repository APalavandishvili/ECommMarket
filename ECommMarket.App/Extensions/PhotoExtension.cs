using ECommMarket.App.Models;

namespace ECommMarket.App.Extensions;

public static class PhotoExtension
{
    internal static async Task<List<PhotoViewModel>> UploadPhotos(List<IFormFile> photos)
    {
        List<PhotoViewModel> newPhotos = [];

        foreach (var photo in photos)
        {
            if (photo.Length > 0)
            {
                // Generate a unique filename for each photo to avoid conflicts
                var fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", @"images\products", fileName);

                // Save the file to the server
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
                newPhotos.Add(new PhotoViewModel()
                {
                    PhotoUrl = @"/images/products/" + fileName,
                    PhotoName = fileName
                });
            }
        }

        return newPhotos;
    }
}
