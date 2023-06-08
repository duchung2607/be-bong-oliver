using BongOliver.Models;

namespace BongOliver.Repositories.ItemRepository
{
    public interface IItemRepository
    {
        public List<Item> GetItems();
        public Item GetItemById(int id);
        public void CreateItem(Item item);
        public void DeleteItem(Item item);
        public void UpdateItem(Item item);
        public bool IsSaveChanges();
    }
}
