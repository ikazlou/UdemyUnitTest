using NUnit.Framework;

namespace Business.Tests
{
    [TestFixture]
    public class SerialPortParserTest
    {
        [Test]
        public void ParserPort_COM1_Should_Return_1()
        {
            int result = SerialPortParser.ParsePort("COM1");

            Assert.That(result, Is.EqualTo(1));
            // older style in NUnit is
            // Assert.AreEqual(1, result);
        }
    }
}
