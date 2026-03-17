using System;
using System.Threading.Tasks;

namespace TelemetryECU
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("TelemetryECU | Mock Car Simulator");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Comandos:\nL - Ligar carro\nA - Acelerar\nF - Frear\nS - Sair do simulador.");

            bool running = true;

            while (running)
            {
                Console.WriteLine("Informações do carro:");
                Console.WriteLine("RPM: " + rpm);
                Console.WriteLine("Temperatura: " + temp);
                Console.WriteLine("Velocidade: " + speed);
                Console.WriteLine("Tanque combustível: " + fuel + "L");

                var key = await Task.Run(() => Console.ReadKey(true).Key);

                switch (key)
                {
                    case ConsoleKey.L:
                        await LigarCarro();
                        break;
                    case ConsoleKey.A:
                        await AcelerarCarro();
                        break;
                    case ConsoleKey.F:
                        await FrearCarro();
                        break;
                    case ConsoleKey.S:
                        running = false;
                        Console.WriteLine("Saindo do simulador...");
                        break;  
                }
            }
        }
    }
}