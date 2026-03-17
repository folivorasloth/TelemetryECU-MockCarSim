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
        public int Marcha { get; set; }

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
            Fuel -= 0.5;
            Console.WriteLine("\nCarro ligado!");
        }

        public async Task SubirMarcha()
        {
            if (Marcha < 5)
            {
                Marcha++;
                Console.WriteLine($"Subindo marcha para: {Marcha}");

                if(Marcha > 1)
                {
                    Rpm -= 3000;
                }
            }
            else
            {
                Console.WriteLine("Marcha máxima atingida.");
            }
        }

        public async Task AcelerarCarro()
        {

            //Verifica se o usuário tentou sair com o carro parado em uma marcha alta, o que faria o motor apagar.
            if (Speed < 10 && Marcha > 3)
            {
                Rpm = 0;
                Speed = 0;
                Console.WriteLine("Você tentou amdar com o carro em uma marcha alta, o motor apagou!");
                return;
            }

            //Aviso de Neutro
            if (Marcha == 0)
            {
                Rpm = Math.Min(Rpm + 800, 7000);
                Console.WriteLine("NEUTRO!!");
                return;
            }

            Console.WriteLine("Acelerando!");

            // Funcionamento

            Rpm += (600 / Marcha);
            Speed += (Marcha * 5);
            Fuel -= 0.1;

            if(Speed >= 180 && Speed < 181)
            {
                Console.WriteLine("VELOCIDADE MÁXIMA!!!");
                Speed = 180;
            }

            if(Rpm >= 6000)
            {
                if(Marcha < 5)
                {
                    Console.WriteLine("RPM Alto. TROQUE DE MARCHA!");
                }
                else
                {
                    Rpm = 6500;
                    Console.WriteLine("RPM no limite! Não é possível subir mais a marcha.");
                }
            }
            Console.WriteLine($"Status: {Marcha}ª Marcha | Velocidade: {Speed} km/h | RPM: {Rpm}");
        }

        public async Task FrearCarro()
        {
            bool parado = false;
            while(! parado)
            {
                Console.WriteLine("Freando!");
                if (Speed == 0)
                {
                    Console.WriteLine("\nCarro já está parado.");
                    parado = true;
                }
                else
                {
                    while (Speed >= 0)
                    {
                        Speed -= 2;
                        Rpm -= 100;
                        Console.WriteLine($"Velocidade: {Speed} km/h");
                        await Task.Delay(50);
                    }
                }
            }
        }

        public override string ToString()
        {
            // Lógica simples de alerta
            string statusTemp = Temp > 100 ? "!!! SUPERAQUECIMENTO !!!" : "Normal";
            string statusFuel = Fuel < 5 ? "!!! RESERVA !!!" : "OK";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n>>>> PAINEL DE CONTROLE <<<<");
            sb.AppendLine($"MARCHA: {Marcha}");
            sb.AppendLine($"VELOCIDADE: {Speed} km/h");
            sb.AppendLine($"RPM: {Rpm} rpm");
            sb.AppendLine($"TEMP: {Temp:F1}°C ({statusTemp})");
            sb.AppendLine($"COMBUSTÍVEL: {Fuel:F2}L ({statusFuel})");
            sb.AppendLine("----------------------------");

            return sb.ToString();
        }

    }
}
