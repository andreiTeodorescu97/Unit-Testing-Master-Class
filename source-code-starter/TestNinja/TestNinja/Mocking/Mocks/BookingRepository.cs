using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking.Mocks
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetOtherBookings(int? id);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IQueryable<Booking> GetOtherBookings(int? id)
        {
            var bookings =
                _unitOfWork.Query<Booking>()
                    .Where(
                        b =>  b.Status != "Cancelled");

            if(id.HasValue)
            {
                bookings = bookings.Where(b => b.Id != id);
            }

            return bookings;
        }
    }
}
