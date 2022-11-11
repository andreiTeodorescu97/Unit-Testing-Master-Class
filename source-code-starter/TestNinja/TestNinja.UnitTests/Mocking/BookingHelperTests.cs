using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;
using TestNinja.Mocking.Mocks;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests
    {
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            var list = new List<Booking>();
            list.Add(new Booking
            {
                Status = "Overlapped",
                Id = 1,
                ArrivalDate = DateTime.Now,
                DepartureDate = DateTime.Now.AddDays(1),
                Reference = "ABCD123"
            });
            list.Add(new Booking
            {
                Status = "Good",
                Id = 2,
                ArrivalDate = DateTime.Now.AddDays(2),
                DepartureDate = DateTime.Now.AddDays(4),
                Reference = "ABCD123"
            });
            list.Add(new Booking
            {
                Status = "Good",
                Id = 3,
                ArrivalDate = DateTime.Now.AddDays(2),
                DepartureDate = DateTime.Now.AddDays(4),
                Reference = "ABCDE123"
            });
            list.Add(new Booking
            {
                Status = "Overlapped",
                Id = 4,
                ArrivalDate = DateTime.Now,
                DepartureDate = DateTime.Now.AddDays(1),
                Reference = "XXXX"
            });

            _unitOfWork.Setup(u => u.Query<Booking>()).Returns(list.AsQueryable());
        }

        [Test]
        public void OverlappingBookingsExist_BookingIsCancelled_ReturnsCancelled()
        {
            var booking = new Booking
            {
                Status = "Cancelled",
                Id = 1,
                ArrivalDate = DateTime.Now,
                DepartureDate = DateTime.Now.AddDays(1),
                Reference = "ABCD123"
            };

            var bookingRepository = new BookingRepository(_unitOfWork.Object);

            var result = BookingHelper.OverlappingBookingsExist(booking, bookingRepository);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_BookingExists_ReturnsReferenceXXXX()
        {
            var booking = new Booking
            {
                Status = "Initial",
                Id = 1,
                ArrivalDate = DateTime.Now,
                DepartureDate = DateTime.Now.AddDays(1),
                Reference = "ABCD123"
            };

            var bookingRepository = new BookingRepository(_unitOfWork.Object);

            var result = BookingHelper.OverlappingBookingsExist(booking, bookingRepository);

            Assert.That(result, Is.EqualTo("XXXX"));
        }

        [Test]
        public void OverlappingBookingsExist_BookingDoesNotExist_ReturnsEmptyString()
        {
            var booking = new Booking
            {
                Status = "Initial",
                Id = 1,
                ArrivalDate = DateTime.Now.AddDays(15),
                DepartureDate = DateTime.Now.AddDays(16),
                Reference = "ABCD123"
            };

            var bookingRepository = new BookingRepository(_unitOfWork.Object);

            var result = BookingHelper.OverlappingBookingsExist(booking, bookingRepository);

            Assert.That(result, Is.EqualTo(string.Empty));
        }
    }
}
