using BongOliver.Models;

namespace BongOliver.Repositories.RateRepository
{
    public class RateRepository : IRateRepository
    {
        private readonly DataContext _context;
        public RateRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateRate(Rate rate)
        {
            _context.Rates.Add(rate);
        }

        public void DeleteRate(Rate rate)
        {
            throw new NotImplementedException();
        }

        public Rate GetRateById(int id)
        {
            return _context.Rates.FirstOrDefault(r => r.id == id);
        }

        public List<Rate> GetRates()
        {
            return _context.Rates.ToList();
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateRate(Rate rate)
        {
            throw new NotImplementedException();
        }
    }
}
