using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace MockCarSimulator
{
    public class TasksCarro
    {
        public int Rpm { get; set; }
        public double Temp { get; set; }
        public double Fuel { get; set; }
        public int Speed { get; set; }

        public TasksCarro(int rpm, double temp, double fuel, int speed)
        {
            Rpm = rpm;
            Temp = temp;
            Fuel = fuel;
            Speed = speed;
        }

        public async Task LigarCarro()
        {
            Console.WriteLine("\nLigando carro");
            await Task.Delay(5000);
            Rpm = 900;
            Temp = 70;

        }
    }
}
