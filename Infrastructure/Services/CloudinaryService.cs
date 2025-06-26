using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;
    public CloudinaryService(string cloudName, string apiKey, string apiSecret)
    {
        _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
    }

    public async Task<string> UploadResumeAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;
        if (!file.ContentType.Equals("application/pdf"))
            return null;
        using (var stream = file.OpenReadStream())
        {
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "resumes"
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl?.ToString();
        }
    }
}