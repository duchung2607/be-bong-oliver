using BongOliver.Services.FileService;
using BongOliver.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        public FileController(IFileService fileService, IUserService userService)
        {
            _fileService = fileService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile? file, string username)
        {
            FileStream stream;
            string path = Path.Combine(System.IO.Path.GetTempPath(), file.FileName);

            //file.SaveAs(path);
            stream = new FileStream("D:\\FPT\\Mock\\File\\" + file.FileName, FileMode.Open);
            //stream = new MemoryStream(Path.Combine(path));
            var link = await Task.Run(() => _fileService.UploadFile(stream, file.FileName));

            return Ok(link);
        }
    }
}
