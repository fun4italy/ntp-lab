using NUnit.Framework;

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
        [TestCase(10.0, TestName = "Тестирование FuelConsumption при присваивании 10.0")]
        public void FuelConsumptionTest(double consumption)
        {
            _ = new Helicopter(consumption, 1.0);
        }
    }
}
