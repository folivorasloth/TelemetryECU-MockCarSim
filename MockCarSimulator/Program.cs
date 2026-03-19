using MockCarSimulator;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MockCarSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TasksCarro carro = new TasksCarro(0, 30, 50, 0);
            TelemetryRender tela = new TelemetryRender();

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

            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            Stopwatch timer = Stopwatch.StartNew();
            bool isRed = false;
            Console.CursorVisible = false;

            while (!Console.KeyAvailable)
            {
                Console.SetCursorPosition(x, y);

                if (timer.ElapsedMilliseconds > 5000)
                {
                    Console.ForegroundColor = isRed ? ConsoleColor.Red : ConsoleColor.DarkGray;
                    isRed = !isRed;
                    Thread.Sleep(500);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Thread.Sleep(100);
                }

                Console.WriteLine(" ╔════════════════════════════════════════════════╗");
                Console.WriteLine(" ║                                                ║");
                Console.WriteLine(" ║           [ PRESS ANY KEY TO ENTER ]           ║");
                Console.WriteLine(" ║                                                ║");
                Console.WriteLine(" ╚════════════════════════════════════════════════╝");
            }

            Console.ReadKey(true);
            Console.ResetColor();
            Console.Clear();

            TextoTopo();
            tela.Initialize();

            bool running = true;
            int coolDown = 0;
            bool freando = false;

            while (running)
            {
                // 1. INPUT
                while (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.L:
                            _ = carro.LigarCarro();   // Fire-and-forget OK aqui
                            break;
                        case ConsoleKey.S:
                            carro.SubirMarcha();
                            break;
                        case ConsoleKey.D:
                            carro.DescerMarcha();
                            break;
                        case ConsoleKey.A:
                            freando = false;
                            carro.AcelerarCarro();
                            coolDown = 6;
                            break;
                        case ConsoleKey.F:
                            freando = true;            // Ativa modo frenagem
                            break;
                        case ConsoleKey.Q:
                            running = false;
                            break;
                    }
                }

                // 2. FÍSICA
                if (freando)
                {
                    carro.FrearCarro(); // Um tick de frenagem por frame

                    if (carro.Speed == 0) freando = false; // Para quando parar
                }
                else if (coolDown > 0)
                {
                    coolDown--;
                }
                else if (carro.carState == 1)
                {
                    carro.DesacelerarMotor();
                    carro.AtualizarTemperatura();
                }

                // 3. RENDER
                string gearDisplay = carro.Marcha == 0 ? "N" : carro.Marcha.ToString();

                string stateDisplay = "OFFLINE";
                if (carro.carState == 1) stateDisplay = "ONLINE";
                else if (carro.carState == 2) stateDisplay = "STARTING";

                tela.UpdateDashboard(
                    gearDisplay,
                    (int)carro.Speed,
                    carro.Fuel,
                    (int)carro.Rpm,
                    carro.maxRpm,
                    (int)carro.Temp,
                    stateDisplay,
                    carro.StatusMessage      // <- Mensagem sem Console.WriteLine
                );

                await Task.Delay(50); // ~20 FPS
            }
        }

        static void TextoTopo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ╔══════════════════════════════════════════════════════════╗ ");
            Console.WriteLine(" ║  TELEMETRY ECU | MOCK CAR SIMULATOR | v0.0.1 - STABLE    ║ ");
            Console.WriteLine(" ╚══════════════════════════════════════════════════════════╝ ");
            Console.ResetColor();

            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  COMANDOS DE OPERAÇÃO:                                     \n");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(" [L]"); Console.ResetColor(); Console.Write(" Ligar Carro      ");
            Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(" [S]"); Console.ResetColor(); Console.WriteLine(" Subir Marcha");

            Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(" [A]"); Console.ResetColor(); Console.Write(" Acelerar         ");
            Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(" [D]"); Console.ResetColor(); Console.WriteLine(" Descer Marcha");

            Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(" [F]"); Console.ResetColor(); Console.Write(" Frear (segure)   ");
            Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(" [Q]"); Console.ResetColor(); Console.WriteLine(" Sair");

            Console.WriteLine(" ──────────────────────────────────────────────────────────");
        }
    }
}