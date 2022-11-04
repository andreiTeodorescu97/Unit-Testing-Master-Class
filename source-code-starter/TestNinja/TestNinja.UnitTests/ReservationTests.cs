using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            //Arrange
            var reservation = new Reservation();

            //Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_UserIsTheSame_ReturnsTrue()
        {
            //Arrange
            var reservation = new Reservation();
            var user = new User { IsAdmin = false };
            reservation.MadeBy = user;

            //Act
            var result = reservation.CanBeCancelledBy(user);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_UserIsNotAllowedToCancel_ReturnsFalse()
        {
            //Arrange
            var reservation = new Reservation();
            var user = new User { IsAdmin = true };
            reservation.MadeBy = user;

            //Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = false });

            //Assert
            Assert.That(result, Is.False);
        }
    }
}
