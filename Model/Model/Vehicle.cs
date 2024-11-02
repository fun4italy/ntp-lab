using System;

namespace Model
{
    public class Vehicle : Transport
    {
        public new double FuelConsumption => base.FuelConsumption;

        public Vehicle(double consumption) : base(consumption)
        {
        }

        public override string TransportType()
        {
            return "Машина";
        }

        public override double FuelByDistance(double distance)
        {
            if (distance <= 0.0)
            {
                throw new ArgumentException(
                    "Значение расстояния должно быть положительным");
            }

            return FuelConsumption * distance / 100.0;
        }
    }
}
