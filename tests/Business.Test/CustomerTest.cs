using Business;
using Business.TestDouble.Testable;
using Business2.Test.TestDoubles;
using NUnit.Framework;

namespace Business2.Test
{
    [TestFixture]
    public class CustomerTest
    {
        [Test]
        public void CalculateWage_When_WagePerHour_ShouldReturn_Multiply()
        {
            // Arrange
            var loger = new Logger();


            // Act
            // Assert
        }

        [Test]
        public void CalculateWage_HorlyPayed_ReturnCorrectWage()
        {
            // Arrange
            const int anyId = 1;

            DbGatewayStub dbGateway = new DbGatewayStub();

            dbGateway.SetWorkingStatistic(new WorkingStatistics()
            {
                PayHourly = true,
                HourSalary = 100,
                WorkingHours = 10
            });

            const decimal expectedWage = 100 * 10;

            var sut = new Customer(dbGateway, new LoggerDummy());

            // Assert
            decimal actual = sut.CalculateWage(anyId);

            // Assert
            Assert.That(actual, Is.EqualTo(expectedWage).Within(0.1));
        }

        [Test]
        public void CalculateWage_PassesCorrectId()
        {
            // Arrange
            const int id = 1;

            var gateway = new DbGatewaySpy();
            gateway.SetWorkingStatistics(new WorkingStatistics());

            // Act
            var sut = new Customer(gateway, new LoggerDummy());
            sut.CalculateWage(id);

            // Assert
            Assert.That(id, Is.EqualTo(gateway.Id));
        }

        [Test]
        public void CalculateWage_PassesCorrectId2()
        {
            // Arrange
            const int id = 2;
            var gateway = new DbGatewayMock();
            gateway.SetWorkingStatistic(new WorkingStatistics());

            var sut = new Customer(gateway, new LoggerDummy());
            sut.CalculateWage(id);

            Assert.That(gateway.VerifyCalledWithProperId(id), Is.True);
        }
    }
}
