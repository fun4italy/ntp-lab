using System;

namespace Model
{
    public class Helicopter : Transport
    {
        private double _speed;

        public new double FuelConsumption => base.FuelConsumption;

        public double Speed
        {
            get => _speed;
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException(
                        "Значение скорости должно быть положительным");
                }

                _speed = value;
            }
        }

        public Helicopter(double consumption, double speed) : base(consumption)
        {
            Speed = speed;
        }

        public override string TransportType()
        {
            return "Вертолет";
        }

        public override double FuelByDistance(double distance)
        {
            if (distance <= 0.0)
            {
                throw new ArgumentException(
                    "Значение расстояния должно быть положительным");
            }

            return FuelConsumption * distance / Speed;
        }
    }
}
