using BongOliver.Models;
using BongOliver.Services.FileService;
using BongOliver.Services.ItemService;
using BongOliver.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BongOliver.Controllers
{
    [Route("api/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet]
        public ActionResult Get()
        {
            var res = _itemService.GetItems();
            return StatusCode(res.code, res);
        }
        [HttpPost]
        public ActionResult Create(Item item)
        {
            var res = _itemService.CreateItem(item);
            return StatusCode(res.code, res);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var res = _itemService.DeleteItem(id);
            return StatusCode(res.code, res);
        }
    }
}
