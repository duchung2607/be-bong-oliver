using AutoMapper;
using BongOliver.DTOs.Response;
using BongOliver.Models;
using BongOliver.Repositories.ItemRepository;
using BongOliver.Repositories.ProductRepository;

namespace BongOliver.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        public ItemService(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public ResponseDTO CreateItem(Item item)
        {
            item.dateModify = DateTime.Now;
            _itemRepository.CreateItem(item);
            if(_itemRepository.IsSaveChanges()) return new ResponseDTO();
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }

        public ResponseDTO DeleteItem(int id)
        {
            var item = _itemRepository.GetItemById(id);
            if (item == null) return new ResponseDTO()
            {
                code = 400,
                message = "Item khong ton tai"
            };
            _itemRepository.DeleteItem(item);
            if (_itemRepository.IsSaveChanges()) return new ResponseDTO();
            return new ResponseDTO()
            {
                code = 400,
                message = "Faile"
            };
        }

        public ResponseDTO GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO GetItems()
        {
            var items = _itemRepository.GetItems();
            if (items.Count == 0) return new ResponseDTO()
            {
                code = 400,
                message = "Khong co item"
            };
            return new ResponseDTO()
            {
                data = items,
            };
        }

        public ResponseDTO UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
