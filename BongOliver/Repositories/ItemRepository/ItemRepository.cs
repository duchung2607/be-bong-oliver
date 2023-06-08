using BongOliver.Models;

namespace BongOliver.Repositories.ItemRepository
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;

        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateItem(Item item)
        {
            _context.Items.Add(item);
        }

        public void DeleteItem(Item item)
        {
            _context.Items.Remove(item);
        }

        public Item GetItemById(int id)
        {
            return _context.Items.FirstOrDefault(i => i.id == id);
        }

        public List<Item> GetItems()
        {
            return _context.Items.ToList();
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateItem(Item item)
        {
            _context.Items.Update(item);
        }
    }
}
