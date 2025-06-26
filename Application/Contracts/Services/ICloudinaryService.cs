using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

public interface ICloudinaryService
{
    Task<UploadResult> UploadAsync(IFormFile file);
    Task<string> GetImageUrlAsync(string publicId);
    Task<string> GetImageUrlAsync(string publicId, int width, int height, string cropMode = "fill");
}