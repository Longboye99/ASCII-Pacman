namespace final_project
{
    public class Display
    {
        public static string dots =
    "                                         \n" +
    "  · · · · · · · · ·   · · · · · · · · ·  \n" +
    "  ·     ·         ·   ·         ·     ·  \n" +
    "  O     ·         ·   ·         ·     O  \n" +
    "  · · · · · · · · · · · · · · · · · · ·  \n" +
    "  ·     ·   ·               ·   ·     ·  \n" +
    "  · · · ·   · · · ·   · · · ·   · · · ·  \n" +
    "        ·                       ·        \n" +
    "        ·                       ·        \n" +
    "        ·                       ·        \n" +
    "        ·                       ·        \n" +
    "        ·                       ·        \n" +
    "        ·                       ·        \n" +
    "        ·                       ·        \n" +
    "        ·                       ·        \n" +
    "  · · · · · · · · ·   · · · · · · · · ·  \n" +
    "  ·     ·         ·   ·         ·     ·  \n" +
    "  O ·   · · · · · ·   · · · · · ·   · O  \n" +
    "    ·   ·   ·               ·   ·   ·    \n" +
    "  · · · ·   · · · ·   · · · ·   · · · ·  \n" +
    "  ·               ·   ·               ·  \n" +
    "  · · · · · · · · · · · · · · · · · · ·  \n" +
    "                                         ";
        public static string map =
    "╔═══════════════════╦═══════════════════╗\n" +
    "║                   ║                   ║\n" +
    "║   ╔═╗   ╔═════╗   ║   ╔═════╗   ╔═╗   ║\n" +
    "║   ╚═╝   ╚═════╝   ╨   ╚═════╝   ╚═╝   ║\n" +
    "║                                       ║\n" +
    "║   ═══   ╥   ══════╦══════   ╥   ═══   ║\n" +
    "║         ║         ║         ║         ║\n" +
    "╚═════╗   ╠══════   ╨   ══════╣   ╔═════╝\n" +
    "      ║   ║                   ║   ║      \n" +
    "══════╝   ╨   ╔════   ════╗   ╨   ╚══════\n" +
    "              ║           ║              \n" +
    "══════╗   ╥   ║           ║   ╥   ╔══════\n" +
    "      ║   ║   ╚═══════════╝   ║   ║      \n" +
    "      ║   ║                   ║   ║      \n" +
    "╔═════╝   ╨   ══════╦══════   ╨   ╚═════╗\n" +
    "║                   ║                   ║\n" +
    "║   ══╗   ═══════   ╨   ═══════   ╔══   ║\n" +
    "║     ║                           ║     ║\n" +
    "╠══   ╨   ╥   ══════╦══════   ╥   ╨   ══╣\n" +
    "║         ║         ║         ║         ║\n" +
    "║   ══════╩══════   ╨   ══════╩══════   ║\n" +
    "║                                       ║\n" +
    "╚═══════════════════════════════════════╝";

        public void RenderMap()
        {
            Render(map, ConsoleColor.DarkBlue, ConsoleColor.Black);
        }

        public void RenderDot(char[,] dots)
        {
            //Render(x, ConsoleColor.White, ConsoleColor.Black);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 41; j++)
                {
                    Console.SetCursorPosition(j, i);
                    if (Convert.ToString(dots[j, i]) != " ")
                        Console.Write(dots[j, i]);
                }
            }
        }

        private void Render(string @string, ConsoleColor foreground, ConsoleColor background)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;

            Console.SetCursorPosition(0, 0);
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            foreach (char c in @string)
            {
                if (c is '\n')
                {
                    Console.SetCursorPosition(x, ++y);
                }
                else if (c != ' ')
                {
                    Console.Write(c);
                }
                else
                {
                    Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                }
            }
        }

        public void RenderPlayer(int x, int y, int speed_X, int speed_Y)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(' ');

            if (speed_X == 1)
            {
                if ((x - 1) >= 0)
                {
                    Console.SetCursorPosition(x - 1, y);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(' ');
                }
                if ((x == 1) && (y == 10))
                {
                    Console.SetCursorPosition(38, 10);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(' ');
                }
            }
            if (speed_X == -1)
            {
                Console.SetCursorPosition(x + 1, y);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(' ');
                if ((x == 38) && (y == 10))
                {
                    Console.SetCursorPosition(2, 10);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(' ');
                }
            }
            if (speed_Y == 1)
            {
                if ((y - 1) >= 0)
                {
                    Console.SetCursorPosition(x, y - 1);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(' ');
                }

            }
            if (speed_Y == -1)
            {
                Console.SetCursorPosition(x, y + 1);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(' ');
            }
        }

        public void RenderGhost(int x, int y, int tempX, int tempY, ConsoleColor ghostcolor)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = ghostcolor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('"');

            Console.SetCursorPosition(tempX, tempY);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(' ');
        }
    }
}
