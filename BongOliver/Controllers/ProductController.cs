using BongOliver.DTOs.Product;
using BongOliver.Services.FileService;
using BongOliver.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IProductService _productService;
        public ProductController(IProductService productService, IFileService fileService)
        {
            _productService = productService;
            _fileService = fileService;
        }
        [HttpGet]
        public ActionResult GetProduct(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var res = _productService.GetProducts(page, pageSize, key, sortBy);
            return StatusCode(res.code, res);
        }
        [HttpGet("{id}")]
        public ActionResult GetProductById(int id)
        {
            var res = _productService.GetProductById(id);
            return StatusCode(res.code, res);
        }
        [HttpPost]
        public ActionResult CreateProduct(ProductDTO productDTO)
        {
            var res = _productService.CreateProduct(productDTO);
            return StatusCode(res.code, res);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(ProductDTO productDTO, int id)
        {
            var res = _productService.UpdateProduct(productDTO,id);
            return StatusCode(res.code, res);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var res = _productService.DeleteProduct(id);
            return StatusCode(res.code, res);
        }
        [HttpGet("type")]
        public ActionResult GetTypes()
        {
            var res = _productService.GetTypes();
            return StatusCode(res.code, res);
        }



        [HttpPut("image")]
        public async Task<ActionResult> UploadFile(IFormFile file, int id)
        {
            FileInfo a = new FileInfo(file.FileName);
            return Ok(a);
            //FileStream stream;
            //string path = Path.Combine(System.IO.Path.GetTempPath(), file.FileName);

            //stream = new FileStream("D:\\FPT\\Mock\\File\\" + file.FileName, FileMode.Open);
            ////stream = new FileStream(Path.Combine(path), FileMode.Open);
            //var link = await Task.Run(() => _fileService.UploadFile(stream, file.FileName));

            //var res = _productService.UpdateImageProduct(link, id);
            //return StatusCode(res.code, res);
        }
    }
}
