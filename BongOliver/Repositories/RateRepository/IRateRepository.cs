using BongOliver.Models;

namespace BongOliver.Repositories.RateRepository
{
    public interface IRateRepository
    {
        public List<Rate> GetRates(int? page = 1, int? pageSize = 10);
        public int GetTotalRate();
        public Rate GetRateById(int id);
        public List<Rate> GetRateByService(int id);
        public void CreateRate(Rate rate);
        public void DeleteRate(Rate rate);
        public void UpdateRate(Rate rate);
        public bool IsSaveChanges();
    }
}
