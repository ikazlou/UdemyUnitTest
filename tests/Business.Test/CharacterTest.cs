using Business;
using NUnit.Framework;
using System;

namespace Business2.Test
{
    [TestFixture]
    public class CharacterTest
    {
        // Arrange
        // Act
        // Assert

        #region Check Name
        [Test]
        public void Should_Set_Name()
        {
            // Arrange
            const string expectedName = "John";

            // Act
            Character c = new Character(Business.Type.Elf, expectedName);

            // Assert
            Assert.That(c.Name, Is.EqualTo(expectedName));
            Assert.That(c.Name, Is.Not.Empty);
            Assert.That(c.Name, Contains.Substring("ohn"));
        }

        [Test]
        public void Should_SetName_CaseInvensitive()
        {
            // Arrange
            const string expectedUpperCase = "JOHN";
            const string expectedLowerCase = "john";

            // Act
            Character c = new Character(Business.Type.Elf, expectedUpperCase);

            // Assert
            Assert.That(c.Name, Is.EqualTo(expectedUpperCase).IgnoreCase);
        }
        #endregion
        #region Character Number params checks

        [Test]
        public void DefaultHealthIs100()
        {
            // Arrange
            const int expectedHealth = 100;
            
            // Act
            Character c = new Character(Business.Type.Elf);

            // Assert
            Assert.That(c.Health, Is.EqualTo(expectedHealth));
        }

        [Test]
        public void DefaultWearIs15()
        {
            // Arrange
            const int expectedWear = 15;

            // Act
            Character c = new Character(Business.Type.Elf);

            // Assert
            Assert.That(c.Wear, Is.EqualTo(expectedWear));
        }

        [Test]
        public void Ork_SpeedIsCorrect()
        {
            // Arrange
            const double expectedSpeedOrk = 1.4;

            // Act
            Character c = new Character(Business.Type.Ork);

            // Assert
            Assert.That(c.Speed, Is.EqualTo(expectedSpeedOrk));
        }

        [Test]
        public void Elf_SpeedIsCorrect()
        {
            // Arrange
            const double expectedSpeedElf = 1.7;

            // Act
            Character c = new Character(Business.Type.Elf);

            // Assert
            Assert.That(c.Speed, Is.EqualTo(expectedSpeedElf));
        }

        [Test]
        public void Ork_SpeedIsCorrecrWithTolerance()
        {
            // Arrange
            const double expectedSpeed = 0.3 + 1.1; 
            // Becouse 1.4 not equal to 1.1 + 0.3

            // Act
            Character c = new Character(Business.Type.Ork);

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
            Character c = new Character(Business.Type.Elf);

            // Assert
            Assert.That(c.Name, Is.Null);
        }

        [Test]
        public void IsDead_KillCharacter_ReturnTrue()
        {
            // Arrange
            Character c = new Character(Business.Type.Elf);

            // Act
            c.Damage(500);

            // Assert
            Assert.That(c.IsDead, Is.True);
            Assert.IsTrue(c.IsDead);
        }

        #endregion
        #region Collections

        [Test]
        public void CollectionTeests()
        {
            // Arrange
            var c = new Character(Business.Type.Elf);

            // Act
            c.Weaponry.Add("Knife");
            c.Weaponry.Add("Pistol");

            // Assert
            Assert.That(c.Weaponry, Is.All.Not.Empty);
            Assert.That(c.Weaponry, Contains.Item("Knife"));
            Assert.That(c.Weaponry, Has.Exactly(2).Length);
            Assert.That(c.Weaponry, Has.Exactly(1).EndsWith("tol"));
            Assert.That(c.Weaponry, Is.Unique);
            Assert.That(c.Weaponry, Is.Ordered);

            var c2 = new Character(Business.Type.Elf);
            c2.Weaponry.Add("Knife");
            c2.Weaponry.Add("Pistol");

            Assert.That(c.Weaponry, Is.EquivalentTo(c2.Weaponry));
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
            object character = new Character(Business.Type.Elf);

            // Assert
            Assert.That(character, Is.TypeOf<Character>());
        }
        #endregion
        #region Range
        [Test]
        public void DefaultCharacterArmorShouldBeGreaterThan30AndLess100()
        {
            // Arrange
            // Act
            Character c = new Character(Business.Type.Elf);

            // Assert
            Assert.That(c.Armor, Is.GreaterThan(30).And.LessThan(100));
        }
        #endregion
        #region Exeptions
        [Test]
        public void Damage_1000_ThrowsArgumentOutOfRange()
        {
            // Arrange
            // Act
            var c = new Character(Business.Type.Elf);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => c.Damage(1001));
            Assert.That(() => c.Damage(1001), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        #endregion
    }
}
