using BongOliver.Models;
using Microsoft.EntityFrameworkCore;

namespace BongOliver.Repositories.BookingRepository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;

        public BookingRepository(DataContext context)
        {
            _context = context;
        }

        public int GetTotal()
        {
            return _context.Bookings.Count();
        }
        public void DeleteBooking(Booking booking)
        {
            _context.Bookings.Remove(booking);
        }

        public List<Booking> GetAllBooking(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var query = _context.Bookings.Include(b => b.Services).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(u => u.description.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                //case "name":
                //    query = query.OrderBy(u => u.name);
                //    break;
                default:
                    query = query.OrderBy(u => u.id);
                    break;
            }
            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
            //return _context.Bookings.ToList();
        }

        public List<Booking> GetAllBooking()
        {
            return _context.Bookings.Include(b => b.Services).ToList();
        }

        public Booking GetBookingById(int id)
        {
            return _context.Bookings.Include(b => b.Services).Include(b=>b.User).Include(b=>b.Stylist).FirstOrDefault(b => b.id == id);
        }

        public List<Booking> GetBookingByUser(string username, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            //var user = _context.Users.FirstOrDefault(u => u.username == username);
            var query = _context.Bookings.Include(b => b.Services).Where(b => b.User.username == username).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(u => u.description.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "time":
                    query = query.OrderBy(u => u.time);
                    break;
                case "create":
                    query = query.OrderByDescending(u => u.create);
                    break;
                default:
                    query = query.OrderBy(u => u.id);
                    break;
            }
            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();

            //var bookings = _context.Bookings.Include(b => b.Services).Where(b => b.User.username == username).ToList();
            //if (!bookings.Any()) { return null; }
            //return bookings;
        }
        public List<Booking> GetBookingByStylist(string username, int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var query = _context.Bookings.Include(b => b.Services).Include(b => b.User).Where(b => b.Stylist.username == username).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(u => u.description.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                //case "name":
                //    query = query.OrderBy(u => u.name);
                //    break;
                default:
                    query = query.OrderBy(u => u.id);
                    break;
            }
            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();

            //if (!bookings.Any()) { return null; }
            //return bookings;
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
        }

        public async Task<bool> IsSaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
        }

        public List<DateTime> GetSchedules(int stylistId, DateTime date)
        {
            // && b.time.Date.Equals(customDate.Date)
            var bookings = _context.Bookings.Where(b => b.Stylist.id == stylistId && b.time.Date.Equals(date.Date)).OrderBy(b => b.time).ToList();
            List<DateTime> result = new List<DateTime>();
            foreach (var booking in bookings)
            {
                result.Add(booking.time);
            }
            return result;
        }

        public List<object> GetCalendar()
        {
            DateTime today = DateTime.Today;

            var bookings = _context.Bookings.Where(b => b.create.Year == today.Year).ToList();
            List<object> result = new List<object>();
            foreach (var booking in bookings)
            {
                result.Add(new
                {
                    bookingId = booking.id,
                    time = booking.time,
                    status = booking.status,
                });
            }
            return result;
        }
    }
}
