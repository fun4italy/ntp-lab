﻿using System;

using NUnit.Framework;
using NUnit.Framework.Internal;

using Model;

namespace UnitTests.Model
{
    /// <summary>
    /// Набор тестов для класса Helicopter
    /// </summary>
    [TestFixture]
    internal class HelicopterTest
    {
        /// <summary>
        /// Тестирование свойства FuelConsumption
        /// </summary>
        /// <param name="consumption"></param>
        [Test]
        [TestCase(1.0, TestName = "Тестирование FuelConsumption при присваивании 1.0")]
        [TestCase(10.0, TestName = "Тестирование FuelConsumption при присваивании 10.0")]
        [TestCase(100.0, TestName = "Тестирование FuelConsumption при присваивании 100.0")]
        [TestCase(double.MaxValue - 1.0, TestName = "Тестирование FuelConsumption при присваивании MaxValue-1.0")]
        [TestCase(double.MaxValue, TestName = "Тестирование FuelConsumption при присваивании MaxValue")]
        [TestCase(0.0, TestName = "Тестирование FuelConsumption при присваивании 0.0")]
        [TestCase(-1.0, TestName = "Тестирование FuelConsumption при присваивании -1.0")]
        [TestCase(double.MinValue, TestName = "Тестирование FuelConsumption при присваивании MinValue")]
        public void FuelConsumptionTest(double consumption)
        {
            if (consumption <= 0)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    _ = new Helicopter(consumption, 100.0);
                });
            }
            else
            {
                _ = new Helicopter(consumption, 100.0);
            }
        }

        /// <summary>
        /// Тестирование свойства Speed
        /// </summary>
        /// <param name="speed"></param>
        [Test]
        [TestCase(1.0, TestName = "Тестирование Speed при присваивании 1.0")]
        [TestCase(10.0, TestName = "Тестирование Speed при присваивании 10.0")]
        [TestCase(100.0, TestName = "Тестирование Speed при присваивании 100.0")]
        [TestCase(double.MaxValue - 1.0, TestName = "Тестирование Speed при присваивании MaxValue-1.0")]
        [TestCase(double.MaxValue, TestName = "Тестирование Speed при присваивании MaxValue")]
        [TestCase(0.0, TestName = "Тестирование Speed при присваивании 0.0")]
        [TestCase(-1.0, TestName = "Тестирование Speed при присваивании -1.0")]
        [TestCase(double.MinValue, TestName = "Тестирование Speed при присваивании MinValue")]
        public void SpeedTest(double speed)
        {
            if (speed <= 0)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    _ = new Helicopter(100.0, speed);
                });
            }
            else
            {
                _ = new Helicopter(100.0, speed);
            }
        }

        /// <summary>
        /// Тестирование метода FuelByDistance
        /// </summary>
        /// <param name="distance"></param>
        [Test]
        [TestCase(100.0, ExpectedResult = 100.0, TestName = "Тестирование FuelByDistance при distance = 100.0")]
        [TestCase(1000.0, ExpectedResult = 1000.0, TestName = "Тестирование FuelByDistance при distance = 1000.0")]
        [TestCase(0.0, ExpectedResult = double.NaN, TestName = "Тестирование FuelByDistance при distance = 0.0")]
        [TestCase(-1.0, ExpectedResult = double.NaN, TestName = "Тестирование FuelByDistance при distance = -1.0")]
        [TestCase(double.MinValue, ExpectedResult = double.NaN, TestName = "Тестирование FuelByDistance при distance = MinValue")]
        public double FuelByDistanceTest(double distance)
        {
            var obj = new Helicopter(100.0, 100.0);

            if (distance <= 0)
            {
                Assert.Throws<ArgumentException>(() => obj.FuelByDistance(distance));
                return double.NaN;
            }
            else
            {
                return obj.FuelByDistance(distance);
            }
        }
    }
}
