using BongOliver.Models;

namespace BongOliver.Repositories.RateRepository
{
    public interface IRateRepository
    {
        public List<Rate> GetRates();
        public Rate GetRateById(int id);
        public void CreateRate(Rate rate);
        public void DeleteRate(Rate rate);
        public void UpdateRate(Rate rate);
        public bool IsSaveChanges();
    }
}
