using BongOliver.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;

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
            _context.Rates.Remove(rate);
        }

        public Rate GetRateById(int id)
        {
            return _context.Rates.Include(r => r.User).Include(r => r.Service).FirstOrDefault(r => r.id == id);
        }

        public List<Rate> GetRateByService(int id)
        {
            return _context.Rates.Include(r => r.User).Include(r => r.Service).Where(r => r.Service.id == id).ToList();
        }

        public List<Rate> GetRates(int? page = 1, int? pageSize = 10)
        {
            var query = _context.Rates.Include(r => r.User).Include(r => r.Service).AsQueryable();

            query = query.OrderByDescending(r => r.create);

            if (page == null || pageSize == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
            //return _context.Rates.ToList();
        }

        public int GetTotalRate()
        {
            return _context.Rates.Count();
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateRate(Rate rate)
        {
            _context.Entry(rate).State = EntityState.Modified;
        }
    }
}
