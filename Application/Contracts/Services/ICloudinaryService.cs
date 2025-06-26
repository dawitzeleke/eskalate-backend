using Microsoft.AspNetCore.Http;

public interface ICloudinaryService
{
    Task<UploadResult> UploadAsync(IFormFile file);
    Task<DeleteResult> DeleteAsync(string publicId);
    Task<string> GetImageUrlAsync(string publicId);
    Task<string> GetImageUrlAsync(string publicId, int width, int height, string cropMode = "fill");
}