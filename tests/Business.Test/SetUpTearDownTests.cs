using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace Business.Tests
{
    [TestFixture]
    public class SetUpTearDownTests
    {
        private Character _character;

        [SetUp] // Этот атрибут указывает механизму исполнения NUnit
                // что-бы он исполнялся каждый раз перерд исполнение каждого метода
        public void Setup()
        {
            _character = new Character(Type.Elf);
        }
         
        [TearDown] // Этот атрибут для очистки неуправляеммыми ресурсами
        public void Teardown()
        {
            _character.Dispose();
            _character = null;
        }

        #region Character Number params checks

        [Test]
        public void DefaultHealthIs100()
        {
            // Arrange
            const int expectedHealth = 100;

            // Act


            // Assert
            Assert.That(_character.Health, Is.EqualTo(expectedHealth));
        }

        [Test]
        public void DefaultWearIs15()
        {
            // Arrange
            const int expectedWear = 15;

            // Act

            // Assert
            Assert.That(_character.Wear, Is.EqualTo(expectedWear));
        }

        [Test]
        public void Ork_SpeedIsCorrect()
        {
            // Arrange
            const double expectedSpeedOrk = 1.4;

            // Act
            Character character = new Character(Type.Ork);

            // Assert
            Assert.That(character.Speed, Is.EqualTo(expectedSpeedOrk));
        }

        [Test]
        public void Elf_SpeedIsCorrect()
        {
            // Arrange
            const double expectedSpeedElf = 1.7;

            // Act

            // Assert
            Assert.That(_character.Speed, Is.EqualTo(expectedSpeedElf));
        }

        [Test]
        public void Ork_SpeedIsCorrecrWithTolerance()
        {
            // Arrange
            const double expectedSpeed = 0.3 + 1.1;
            // Becouse 1.4 not equal to 1.1 + 0.3

            // Act
            Character c = new Character(Type.Ork);

            // Assert
            //Assert.That(c.Speed, Is.EqualTo(expectedSpeed)); // False
            Assert.That(c.Speed, Is.EqualTo(expectedSpeed).Within(0.5));
            Assert.That(c.Speed, Is.EqualTo(expectedSpeed).Within(1).Percent);
        }

        #endregion
        #region Null And Boolean
        [Test]
        public void DefaultNameIsNull()
        {
            // Arrange
            // Act

            // Assert
            Assert.That(_character.Name, Is.Null);
        }

        [Test]
        public void IsDead_KillCharacter_ReturnTrue()
        {
            // Arrange

            // Act
            _character.Damage(500);

            // Assert
            Assert.That(_character.IsDead, Is.True);
            Assert.IsTrue(_character.IsDead);
        }

        #endregion
        #region Collections

        [Test]
        public void CollectionTeests()
        {
            // Arrange

            // Act
            _character.Weaponry.Add("Knife");
            _character.Weaponry.Add("Pistol");

            // Assert
            Assert.That(_character.Weaponry, Is.All.Not.Empty);
            Assert.That(_character.Weaponry, Contains.Item("Knife"));
            Assert.That(_character.Weaponry, Has.Exactly(2).Length);
            Assert.That(_character.Weaponry, Has.Exactly(1).EndsWith("tol"));
            Assert.That(_character.Weaponry, Is.Unique);
            Assert.That(_character.Weaponry, Is.Ordered);

            var c2 = new Character(Business.Type.Elf);
            c2.Weaponry.Add("Knife");
            c2.Weaponry.Add("Pistol");

            Assert.That(_character.Weaponry, Is.EquivalentTo(c2.Weaponry));
        }
        #endregion
        #region Reference Equality
        [Test]
        public void SameCharacters_AreEqualByReference()
        {
            // Arrange
            // Act
            Character character = new Character(Business.Type.Elf);
            Character character1 = character;

            // Assert
            Assert.That(character, Is.SameAs(character1));
        }

        #endregion
        #region Types
        [Test]
        public void TestObjectOfCharacterType()
        {
            // Arrange
            // Act

            // Assert
            Assert.That(_character, Is.TypeOf<Character>());
        }
        #endregion
        #region Range
        [Test]
        public void DefaultCharacterArmorShouldBeGreaterThan30AndLess100()
        {
            // Arrange
            // Act

            // Assert
            Assert.That(_character.Armor, Is.GreaterThan(30).And.LessThan(100));
        }
        #endregion
        #region Exeptions
        [Test]
        public void Damage_1000_ThrowsArgumentOutOfRange()
        {
            // Arrange
            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _character.Damage(1001));
            Assert.That(() => _character.Damage(1001), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        #endregion

        #region Problem private field
        [Test]
        public void Damage_100ToElf_Return_45Health()
        {
            // Arrange
            var expactedHealth = 45;

            // Act
            _character.Damage(100);

            // Assert
            Assert.That(_character.Health, Is.EqualTo(expactedHealth));
        }

        [Test]
        public void Damage_80ToElf_Return_65Health()
        {
            // Arrange
            var expactedHealth = 65;

            // Act
            _character.Damage(80);

            // Assert
            Assert.That(_character.Health, Is.EqualTo(expactedHealth));
        }

        #endregion
    }
}
