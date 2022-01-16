using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Diagnostics;

[SetUpFixture]
class AssemblySetupTearDown
{
    [OneTimeSetUp]
    public void AssamblySetup()
    {
        Trace.WriteLine("Assambly setup.");
    }

    [OneTimeTearDown]
    public void AssamblyTearDown()
    {
        Trace.WriteLine("Assambly TearDown.");
    }
}


namespace Business.Tests
{
    [TestFixture]
    public class OneTimeSetUpOneTimeTearDownTests
    {
        [OneTimeSetUp] 
        public void NameSpaceSetup()
        {
            Trace.WriteLine("NameSpace Setup");
        }

        [OneTimeTearDown] 
        public void NameSpaceTeardown()
        {
            Trace.WriteLine("NameSpace TearDown");
        }

        [Test]
        public void Should_Set_Name()
        {
            // Arrange
            const string expectedName = "John";

            // Act
            Character c = new Character(Type.Elf, expectedName);

            // Assert
            Assert.That(c.Name, Is.EqualTo(expectedName));
            Assert.That(c.Name, Is.Not.Empty);
            Assert.That(c.Name, Contains.Substring("ohn"));
        }
    }
}
