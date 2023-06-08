using BongOliver.Models;

namespace BongOliver.Repositories.WaletRepository
{
    public interface IWaletRepository
    {
        Walet GetWalet(int id);
        void CreateWalet(Walet walet);
        void UpdateWalet(Walet walet);
        void DeleteWalet(int waletId);
    }
}