using BongOliver.Models;

namespace BongOliver.Repositories.HairRepository
{
    public interface IHairRepository
    {
        void CreateHairStyle(HairStyle hairStyle);
        void UpdateHairStyle(HairStyle hairStyle);
        List<HairStyle> GetAllHairStyles(int? page = 1, int? pageSize = 10, string? key = "");
        int GetTotal();
        HairStyle GetHairStyleById(int id);
        void DeleteHairStyle(HairStyle hairStyle);
        bool IsSaveChanges();
    }
}
