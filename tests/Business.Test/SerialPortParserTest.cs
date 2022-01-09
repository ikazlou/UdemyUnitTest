using Business;
using NUnit.Framework;
using System;

namespace Business.Tests
{
    [TestFixture]
    public class SerialPortParserTest
    {
        [Test]
        public void ParserPort_COM1_Should_Return1()
        {
            int result = SerialPortParser.ParsePort("COM1");

            Assert.That(result, Is.EqualTo(1));
            // older style in NUnit is
            // Assert.AreEqual(1, result);
        }

        [Test]
        public void ParsePort_WrongPortName_Should_Returns_Exeption()
        {
            // Arrange
            string portName = "POR1";

            // Act
            TestDelegate action = () => SerialPortParser.ParsePort(portName);

            // Assert
            Assert.Throws<FormatException>(action);
        }
    }
}
