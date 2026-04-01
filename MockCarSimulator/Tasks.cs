using System;
using System.Threading.Tasks;

namespace MockCarSimulator
{
    public class TasksCarro
    {
        public double Rpm { get; set; }
        public double Temp { get; set; }
        public double Fuel { get; set; }
        public double Speed { get; set; }
        public int Marcha { get; set; }
        public int maxRpm = 6500;

        // Velocidade máxima por marcha (índice 0 = neutro)
        private static readonly int[] maxSpeedPerGear = { 0, 40, 80, 120, 160, 200 };

        // Estado: 0 = OFFLINE | 1 = ONLINE | 2 = STARTING
        public int carState;

        // Mensagem de status que o Program.cs exibe no lugar de Console.WriteLine
        public string StatusMessage { get; private set; } = "";

        public TasksCarro(int rpm, double temp, double fuel, int speed)
        {
            Rpm = rpm;
            Temp = temp;
            Fuel = fuel;
            Speed = speed;
        }

        // ── LIGAR ────────────────────────────────────────────────────────────────
        public async Task LigarCarro()
        {
            if (carState != 0) return; // Ignora se já ligado/ligando

            carState = 2; // STARTING
            StatusMessage = "Ligando carro...";
            await Task.Delay(2000);

            Rpm = 900;
            Temp = 70;
            Fuel = Math.Max(0, Fuel - 1.0);
            carState = 1; // ONLINE
            StatusMessage = "Carro ligado!";
        }

        // ── SUBIR MARCHA ─────────────────────────────────────────────────────────
        public void SubirMarcha()
        {
            if (carState != 1) return;

            if (Marcha < 5)
            {
                Marcha++;
                if (Marcha > 1)
                    Rpm = Math.Max(900, Rpm - 2500); // Queda de RPM na troca
                StatusMessage = $"Marcha {Marcha}";
            }
            else
            {
                StatusMessage = "Marcha máxima!";
            }
        }

        // ── DESCER MARCHA ─────────────────────────────────────────────────────────
        public void DescerMarcha()
        {
            if (carState != 1) return;
            if (Marcha == 0) return; 

                Marcha--;
                if (Marcha > 1)
                    Rpm = Math.Max(900, Rpm + 2500); // Queda de RPM na troca
                StatusMessage = $"Marcha {Marcha}";

        }

        // ── ACELERAR ─────────────────────────────────────────────────────────────
        public void AcelerarCarro()
        {
            if (carState != 1) return;

            // Sobe RPM
            Rpm = Math.Min(maxRpm, Rpm + 150);

            // Velocidade limitada pelo teto da marcha atual
            int topSpeed = maxSpeedPerGear[Marcha];
            if (Speed < topSpeed)
            {
                Speed += 1;
                if (Speed > topSpeed) Speed = topSpeed;
            }

            Fuel = Math.Max(0, Fuel - 0.05);
            StatusMessage = "";
        }

        // ── DESACELERAR (chamado pelo loop quando nenhuma tecla é pressionada) ───
        public void DesacelerarMotor()
        {
            if (carState != 1) return;

            // RPM cai naturalmente até o marcha-lenta
            double rpmMinimo = 900;
            if (Rpm > rpmMinimo)
                Rpm = Math.Max(rpmMinimo, Rpm - 150);

            // Velocidade também cai com a resistência (sem embreagem)
            if (Speed > 0)
            {
                Speed = Math.Max(0, Speed - 0.8);
                // Se for devagar demais para a marcha, não empurra o RPM alto
            }
        }

        // ── FREAR ────────────────────────────────────────────────────────────────
        // Faz um tick de frenagem a cada chamada (sem loop interno, sem await perdido)
        public void FrearCarro()
        {
            if (carState != 1) return;

            if (Speed > 0)
            {
                Speed = Math.Max(0, Speed - 3.0);
                Rpm = Math.Max(900, Rpm - 200);
            }
            else
            {
                StatusMessage = "Carro parado.";
            }
        }
        public void AtualizarTemperatura()
        {
            if (carState != 1) return;

            double rpmRatio = Rpm / maxRpm;

            // ── AQUECIMENTO ──────────────────────────────────────────────
            double calor = 0;

            // RPM alto sempre gera calor
            if (rpmRatio > 0.7)
                calor += (rpmRatio - 0.7) * 0.8; // Escala suave

            // Neutro esticado — sem carga dissipa mal o calor
            if (Marcha == 0 && Rpm > 2000)
                calor += 0.3;

            // Marcha baixa com velocidade alta — motor forçado
            if (Marcha > 0 && Marcha <= 2 && Speed > 80)
                calor += 0.5;

            // ── RESFRIAMENTO ─────────────────────────────────────────────
            double frio = 0;

            // Vento do radiador — quanto mais rápido, mais resfria
            if (Speed > 30)
                frio += (Speed / 200.0) * 0.4;

            // Resfriamento passivo lento sempre acontece se acima do ideal
            if (Temp > 90)
                frio += 0.05;

            // ── APLICA ───────────────────────────────────────────────────
            Temp += calor - frio;
            Temp = Math.Clamp(Temp, 30, 130); // Limita entre frio ambiente e superaquecimento
        }
    }
}