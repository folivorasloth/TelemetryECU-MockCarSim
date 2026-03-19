using MockCarSimulator;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TelemetryECU
{
    class Program
    {
        static async Task Main(string[] args)
        {

            TasksCarro carro = new TasksCarro(0, 30, 50, 0);

            string asciiArt = @"
  __  __            _        _____              _____ _                 _       _             
 |  \/  |          | |      / ____|            / ____(_)               | |     | |            
 | \  / | ___   ___| | __  | |     __ _ _ __  | (___  _ _ __ ___  _   _| | __ _| |_ ___  _ __ 
 | |\/| |/ _ \ / __| |/ /  | |    / _` | '__|  \___ \| | '_ ` _ \| | | | |/ _` | __/ _ \| '__|
 | |  | | (_) | (__|   <   | |___| (_| | |     ____) | | | | | | | |_| | | (_| | || (_) | |   
 |_|  |_|\___/ \___|_|\_\   \_____\__,_|_|    |_____/|_|_| |_| |_|\__,_|_|\__,_|\__\___/|_|                                                                                                                                                                                              
        ";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(asciiArt);
            Console.ResetColor();

            int boxStartLine = Console.CursorTop;
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            Stopwatch timer = Stopwatch.StartNew();
            bool isRed = false;
            Console.CursorVisible = false;

            while (!Console.KeyAvailable)
            {
                // Posiciona o cursor sempre no início do bloco da mensagem
                Console.SetCursorPosition(x, y);

                if (timer.ElapsedMilliseconds > 5000)
                {
                    // MODO ALERTA (PISCANTE)
                    Console.ForegroundColor = isRed ? ConsoleColor.Red : ConsoleColor.DarkGray;

                    Console.WriteLine(" ╔════════════════════════════════════════════════╗");
                    Console.WriteLine(" ║                                                ║");
                    Console.WriteLine(" ║           [ PRESS ANY KEY TO ENTER ]           ║");
                    Console.WriteLine(" ║                                                ║");
                    Console.WriteLine(" ╚════════════════════════════════════════════════╝");

                    isRed = !isRed;
                    Thread.Sleep(500); // Velocidade do pisca
                }
                else
                {
                    // MODO PADRÃO (ESTÁTICO)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(" ╔════════════════════════════════════════════════╗");
                    Console.WriteLine(" ║                                                ║");
                    Console.WriteLine(" ║           [ PRESS ANY KEY TO ENTER ]           ║");
                    Console.WriteLine(" ║                                                ║");
                    Console.WriteLine(" ╚════════════════════════════════════════════════╝");

                    // Um delay menor aqui para não consumir 100% da CPU
                    Thread.Sleep(100);
                }
            }

            Console.ReadKey(true);
            Console.ResetColor();
            Console.SetCursorPosition(0, boxStartLine + 5); // Pula o box para continuar
            TextoTopo();

            // AQUI INICIA O PROGRAMA
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