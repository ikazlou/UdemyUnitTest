using Business;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Business2.Test
{
    [TestFixture]
    class DeviceTests
    {
        [Test]
        public void Connect_FailedThrise_ThreeTries()
        {
            // Arrange
            var provider = Substitute.For<IProtocol>();
            provider.Connect(Arg.Any<string>()).Returns(false);
            var sut = new Device(provider);

            provider.Connect(Arg.Is("COM1")).Returns(true);
            provider.Connect(Arg.Is<string>(x => x.StartsWith("Com"))).Returns(true);

            // Act
            sut.Connect(string.Empty);

            // Assert
            provider.Received(3).Connect(Arg.Any<string>());
        }

        [Test]
        public void Find_FoundOnCOM1_ReturnsCOM1()
        {
            // Arrange
            var provider = Substitute.For<IProtocol>();
            var sut = new Device(provider);
            Task<string> task = sut.Find();

            const string portName = "COM1";

            // Act
            provider.SearchingFinished += Raise.Event<EventHandler<DeviceSearchingEventArgs>>(provider, new DeviceSearchingEventArgs(portName));

            // Assert
            Assert.That(task.Result, Is.EqualTo(portName));
        }
    }
}
