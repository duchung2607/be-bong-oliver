using BongOliver.DTOs.Response;
using BongOliver.Models;

namespace BongOliver.Services.ItemService
{
    public interface IItemService
    {
        ResponseDTO GetItems();
        ResponseDTO GetItemById(int id);
        ResponseDTO CreateItem(Item item);
        ResponseDTO DeleteItem(int id);
        ResponseDTO UpdateItem(Item item);
    }
}
