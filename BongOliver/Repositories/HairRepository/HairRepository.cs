using BongOliver.Models;
using Firebase.Auth;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Globalization;

namespace BongOliver.Repositories.HairRepository
{
    public class HairRepository : IHairRepository
    {
        private readonly DataContext _context;

        public HairRepository(DataContext context)
        {
            _context = context;
        }
        public void CreateHairStyle(HairStyle hairStyle)
        {
            _context.HairStyles.Add(hairStyle);
        }

        public void DeleteHairStyle(HairStyle hairStyle)
        {
            _context.HairStyles.Remove(hairStyle);
        }

        public List<HairStyle> GetAllHairStyles(int? page = 1, int? pageSize = 10, string? key = "")
        {
            var query = _context.HairStyles.AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(u => u.name.ToLower().Contains(key.ToLower()));
            }

            query = query.OrderByDescending(u => u.id);

            if (page == null || pageSize == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        public HairStyle GetHairStyleById(int id)
        {
            return _context.HairStyles.FirstOrDefault(h => h.id == id);
        }

        //public List<HairStyle> GetHairStylesByUser(int id)
        //{
        //    return _context.HairStyles.Include(h=>h.Users).Where(h => h.Users.id == id).ToList();
        //}

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateHairStyle(HairStyle hairStyle)
        {
            _context.Entry(hairStyle).State = EntityState.Modified;
        }

        public int GetTotal()
        {
            return _context.HairStyles.Count();
        }
    }
}
