using System;
using System.Text;

namespace MockCarSimulator
{
    public class TelemetryRender
    {
        private const int LeftM = 2;
        private const int TopM = 12;

        public void Initialize()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            DrawStaticUI();
        }

        private void DrawStaticUI()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(LeftM, TopM + 0); Console.Write("╔════════════════════════ TELEMETRY ECU v1.0 ════════════════════════╗");
            Console.SetCursorPosition(LeftM, TopM + 1); Console.Write("║                                                                    ║");
            Console.SetCursorPosition(LeftM, TopM + 2); Console.Write("║  GEAR: [   ]          SPEED:     km/h       FUEL: [        ]     % ║");
            Console.SetCursorPosition(LeftM, TopM + 3); Console.Write("║                                                                    ║");
            Console.SetCursorPosition(LeftM, TopM + 4); Console.Write("║  RPM:                                                              ║");
            Console.SetCursorPosition(LeftM, TopM + 5); Console.Write("║  [0] ═══[1]═══[2]═══[3]═══[4]═══[5]═══[6]═══[7]═══[8]              ║");
            Console.SetCursorPosition(LeftM, TopM + 6); Console.Write("║                                                                    ║");
            Console.SetCursorPosition(LeftM, TopM + 7); Console.Write("║                                                                    ║");
            Console.SetCursorPosition(LeftM, TopM + 8); Console.Write("║  TEMP:   °C [        ]                      STATUS:                ║");
            Console.SetCursorPosition(LeftM, TopM + 9); Console.Write("╚════════════════════════════════════════════════════════════════════╝");
            // Linha de mensagem logo abaixo do painel
            Console.SetCursorPosition(LeftM, TopM + 10); Console.Write("                                                                      ");
            Console.ResetColor();
        }

        // Assinatura agora inclui statusMessage
        public void UpdateDashboard(
            string gear, int speed, double fuelPct,
            int rpm, int maxRpm, int temp,
            string status, string statusMessage = "")
        {
            // ── MARCHA ────────────────────────────────────────────────────────────
            Console.SetCursorPosition(LeftM + 11, TopM + 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(gear.PadRight(1));

            // ── VELOCIDADE ────────────────────────────────────────────────────────
            Console.SetCursorPosition(LeftM + 31, TopM + 2);
            Console.Write(speed.ToString("000"));

            // ── COMBUSTÍVEL ───────────────────────────────────────────────────────
            int fuelWidth = 8;
            int fuelBlocks = Math.Clamp((int)((fuelPct / 100.0) * fuelWidth), 0, fuelWidth);
            Console.SetCursorPosition(LeftM + 52, TopM + 2);
            Console.ForegroundColor = fuelPct < 20 ? ConsoleColor.Red : ConsoleColor.Cyan;
            Console.Write(new string('█', fuelBlocks));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(new string('░', fuelWidth - fuelBlocks));
            Console.SetCursorPosition(LeftM + 63, TopM + 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(fuelPct.ToString("00").PadLeft(2));

            // ── RPM (número) ──────────────────────────────────────────────────────
            Console.SetCursorPosition(LeftM + 8, TopM + 4);
            Console.Write(rpm.ToString("0000"));

            // ── RPM (barra) ───────────────────────────────────────────────────────
            int rpmWidth = 49;
            double rpmRatio = (double)rpm / maxRpm;
            int rpmBlocks = Math.Clamp((int)(rpmRatio * rpmWidth), 0, rpmWidth);

            Console.SetCursorPosition(LeftM + 3, TopM + 6);
            Console.ForegroundColor = rpmRatio > 0.9
                ? ConsoleColor.Red
                : rpmRatio > 0.7
                    ? ConsoleColor.Yellow
                    : ConsoleColor.Green;
            Console.Write(new string('█', rpmBlocks));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(new string('░', rpmWidth - rpmBlocks));

            // ── TEMPERATURA ───────────────────────────────────────────────────────
            Console.SetCursorPosition(LeftM + 9, TopM + 8);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(temp.ToString("00"));

            Console.SetCursorPosition(LeftM + 16, TopM + 8);
            if (temp < 70) { Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("COLD  "); }
            else if (temp <= 100) { Console.ForegroundColor = ConsoleColor.Green; Console.Write("NORMAL"); }
            else { Console.ForegroundColor = ConsoleColor.Red; Console.Write("HOT!  "); }

            // ── STATUS ────────────────────────────────────────────────────────────
            Console.SetCursorPosition(LeftM + 54, TopM + 8);
            Console.ForegroundColor = status == "ONLINE"
                ? ConsoleColor.Green
                : status == "STARTING"
                    ? ConsoleColor.Yellow
                    : ConsoleColor.DarkGray;
            Console.Write(status.PadRight(10));

            // ── MENSAGEM (linha abaixo do painel, sem overlap) ────────────────────
            Console.SetCursorPosition(LeftM, TopM + 10);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write((" » " + statusMessage).PadRight(70));

            Console.ResetColor();
        }
    }
}