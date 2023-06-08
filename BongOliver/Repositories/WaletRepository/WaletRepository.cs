using BongOliver.Models;

namespace BongOliver.Repositories.WaletRepository
{
    public class WaletRepository : IWaletRepository
    {
        private readonly DataContext _context;

        public WaletRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateWalet(Walet walet)
        {
            _context.Walets.Add(walet);
        }

        public void DeleteWalet(int waletId)
        {
            var walet = _context.Walets.FirstOrDefault(w=>w.id == waletId);
            _context.Walets.Remove(walet);
        }

        public Walet GetWalet(int id)
        {
            return _context.Walets.Last();
        }

        public void UpdateWalet(Walet walet)
        {
            _context.Walets.Update(walet);
        }
    }
}
