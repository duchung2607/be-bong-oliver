using BongOliver.Models;
using Microsoft.EntityFrameworkCore;

namespace BongOliver.Repositories.PaymentRepository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DataContext _context;

        public PaymentRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> CreatePaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return true;
        }

        public void DeletePayment(Payment payment)
        {
            _context.Payments.Remove(payment);
        }

        public async Task<List<Payment>> GetPaymentListAsync()
        {
            try
            {
                return await _context.Payments.OrderByDescending(p=>p.time).ToListAsync();
            }catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> IsPaymentOfBookingAlreadyExists(int bookingId)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.bookingId == bookingId);
            if (payment == null) return false;
            return true;
        }

        public async Task<bool> IsSaveChange()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Payment> PaymentGetPaymentById(int id)
        {
            return await _context.Payments.FindAsync(id);
        }
    }
}
