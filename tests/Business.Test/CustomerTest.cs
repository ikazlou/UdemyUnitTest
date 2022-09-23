using Business;
using Business.TestDouble.Testable;
using Business2.Test.TestDoubles;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;

namespace Business2.Test
{
    [TestFixture]
    public class CustomerTestsWithMockingFramefork
    {
        [Test]
        public void CalculateWage_HorlyPayed_ReturnCorrectWage_FullDescripeNSubstitute()
        {
            // Arrange
            var gateway = Substitute.For<IDbGateway>();
            var workingStatistics = new WorkingStatistics() { PayHourly = true, HourSalary = 100, WorkingHours = 10 };
            var anyId = 1;

            gateway.GetWorkingStatistics(anyId /*Arg.Any<int>() */).ReturnsForAnyArgs(workingStatistics);
            //Arg.Any<int>() - показывает что не важно какое значение мыы туда передаем
            gateway.Connected.Returns(true);


            const decimal expectedWage = 100 * 10;
            var sut = new Customer(gateway, Substitute.For<ILogger>());

            // Act
            decimal actual = sut.CalculateWage(/*Arg.Any<int>()*/ anyId);

            // Assert
            Assert.That(actual, Is.EqualTo(expectedWage).Within(0.1));
        }

        [Test]
        public void CalculateWage_HorlyPayed_ReturnCorrectWage()
        {
            // Arrange
            var gateway = Substitute.For<IDbGateway>();
            var workingStatistics = new WorkingStatistics() { PayHourly = true, HourSalary = 100, WorkingHours = 10 };
            const int anyId = 1;
            gateway.GetWorkingStatistics(anyId).ReturnsForAnyArgs(workingStatistics);

            const decimal expectedWage = 100 * 10;
            var sut = new Customer(gateway, Substitute.For<ILogger>());

            // Act
            decimal actual = sut.CalculateWage(anyId);

            // Assert
            Assert.That(actual, Is.EqualTo(expectedWage).Within(0.1));
        }

        [Test]
        public void Calculate_ThrowsException_Returns0()
        {
            // Arrange
            int id = 1;
            var gateway = Substitute.For<IDbGateway>();

            // Act
            gateway.GetWorkingStatistics(id).Throws(new InvalidOperationException());
            var sut = new Customer(gateway, Substitute.For<ILogger>());

            // Assert
            decimal actual = sut.CalculateWage(id);
            Assert.That(actual, Is.EqualTo(0));
        }

        [Test]
        public void CalculateWage_PassesCorrectId()
        {
            // Arrange
            const int id = 1;

            var gateway = new DbGatewaySpy();
            gateway.SetWorkingStatistics(new WorkingStatistics());

            //Set gateWay
            var gatewayNSub = Substitute.For<IDbGateway>();
            // Seeting gateWay
            gatewayNSub.GetWorkingStatistics(id).ReturnsForAnyArgs(new WorkingStatistics());


            // Act
            var sut = new Customer(gateway, new LoggerDummy());
            sut.CalculateWage(id);

            // Make test system and run act
            var sutNSub = new Customer(gatewayNSub, Substitute.For<ILogger>());
            sutNSub.CalculateWage(id);

            // Assert
            Assert.That(id, Is.EqualTo(gateway.Id));

            //Attention in that case we are not using Assert, we call method NSubstitute
            gatewayNSub.Received().GetWorkingStatistics(id);
        }
    }

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
