using BongOliver.DTOs.Response;

namespace BongOliver.Services.FileService
{
    public interface IFileService
    {
        Task<string> UploadFile(FileStream stream, string fileName);
        Task<string> UploadFile(MemoryStream stream, string fileName);
    }
}
