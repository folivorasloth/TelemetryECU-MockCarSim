using MockCarSimulator;
using System;
using System.Threading.Tasks;

namespace TelemetryECU
{
    class Program
    {
        static async Task Main(string[] args)
        {

            TasksCarro carro = new TasksCarro(0, 30, 50, 0);

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("TelemetryECU | Mock Car Simulator");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Comandos:\nL - Ligar carro\nS - Subir Marcha\nD - Descer Marcha\nA - Acelerar\nF - Frear\nQ - Sair do simulador.");

            bool running = true;

            while (running)
            {

                /*
                Console.WriteLine("Informações do carro:");
                Console.WriteLine("RPM: " + rpm);
                Console.WriteLine("Temperatura: " + temp);
                Console.WriteLine("Velocidade: " + speed);
                Console.WriteLine("Tanque combustível: " + fuel + "L");
                */

                var key = await Task.Run(() => Console.ReadKey(true).Key);

                switch (key)
                {
                    case ConsoleKey.L:
                        await carro.LigarCarro();
                        Console.WriteLine(carro);
                        break;
                    case ConsoleKey.S:
                        await carro.SubirMarcha();
                        break;
                    case ConsoleKey.D:
                         /*await carro.DescerMarcha();
                        break; */
                    case ConsoleKey.A:
                        await carro.AcelerarCarro();

                        // Limpa a tela para que a telemetria apareça sempre no topo
                        Console.Clear();

                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine("TelemetryECU | Mock Car Simulator");
                        Console.WriteLine("----------------------------------------------");

                        // Chama o seu override ToString()
                        Console.WriteLine(carro);

                        Console.WriteLine("\n[Segure A para acelerar | F para frear | S para sair]");
                        break;
                    case ConsoleKey.F:
                        await carro.FrearCarro();
                        break;
                    case ConsoleKey.Q:
                        running = false;
                        Console.WriteLine("Saindo do simulador...");
                        break;  
                }
            }
        }
    }
}