using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using static Business.Tests.ParametrizeMethodTests;

namespace Business.Tests
{
    [TestFixture]
    public class ParametrizeMethodTests
    {
        private Character _character;

        [SetUp]
        public void Setup()
        {
            _character = new Character(Type.Elf);
        }



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

        [TestCase(100, 45)]
        [TestCase(80, 65)]
        public void Common_Damage_ToElf_Return_TrueValue(int damage, int expectedHealth)
        {
            // Arrange

            // Act
            _character.Damage(damage);

            // Assert
            Assert.That(_character.Health, Is.EqualTo(expectedHealth));
        }

        [TestCaseSource(typeof(DamageSource))]
        public void Common_Damage_ToElf_Return_TrueValue_WithTestCaseSource(int damage, int expectedHealth)
        {
            // Arrange

            // Act
            _character.Damage(damage);

            // Assert
            Assert.That(_character.Health, Is.EqualTo(expectedHealth));
        }

        public class DamageSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new int[] { 100, 45 };
                yield return new int[] { 80, 65 };
            }
        }
    }
}
