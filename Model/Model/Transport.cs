using System;

namespace Model
{
    public abstract class Transport
    {
        private double _consumption;

        protected double FuelConsumption
        {
            get => _consumption;
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException(
                        "Значение расхода должно быть положительным");
                }

                _consumption = value;
            }
        }

        protected Transport(double consumption)
        {
            FuelConsumption = consumption;
        }

        public abstract string TransportType();
        public abstract double FuelByDistance(double distance);
    }
}
