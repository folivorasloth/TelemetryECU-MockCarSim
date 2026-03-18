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

            Console.WriteLine("|-----------------------------------------------|");
            Console.WriteLine("|     PRESSIONE QUALQUER TECLA PARA INICIAR     |");
            Console.WriteLine("|_______________________________________________|");

            bool running = true;

            while (running)
            {
                var key = await Task.Run(() => Console.ReadKey(true).Key);

                TextoTopo();
                

                switch (key)
                {
                    case ConsoleKey.L:
                        await carro.LigarCarro();
                        Console.WriteLine(carro);
                        TextoTopo();
                        Console.WriteLine(carro);
                        break;
                    case ConsoleKey.S:
                        TextoTopo();
                        await carro.SubirMarcha();
                        break;
                    case ConsoleKey.D:
                         /*await carro.DescerMarcha();
                        break; */
                    case ConsoleKey.A:
                        TextoTopo();
                        await carro.AcelerarCarro();
                        Console.WriteLine(carro);
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
        static void TextoTopo()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("TelemetryECU | Mock Car Simulator");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("\nL - Ligar carro\nA - Acelerar\nS - Subir Marcha\nD - Descer Marcha\nF - Frear\nQ - sair");
        }
    }
}