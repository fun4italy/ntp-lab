using System;

using Model;

namespace ConsoleLoader
{
    class Program
    {
        static void Main()
        {
            Transport[] transport = new Transport[3];
            string input;

            Console.Write("Расход машины на 100 км: ");
            input = Console.ReadLine();
            transport[0] = new Vehicle(double.Parse(input));

            Console.Write("Расход машины-гибрида на 100 км: ");
            input = Console.ReadLine();
            transport[1] = new Hybrid(double.Parse(input));

            Console.Write("Расход вертолета за час полета: ");
            input = Console.ReadLine();
            Console.Write("Скорость вертолета: ");
            string speedInput = Console.ReadLine();
            transport[2] = new Helicopter(double.Parse(input), double.Parse(speedInput));

            foreach (var item in transport)
            {
                Console.WriteLine($"{item.TransportType()}: расход топлива на 50 км пути - {item.FuelByDistance(50)} л");
            }
        }
    }
}
