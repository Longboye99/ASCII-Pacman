using static System.Console;

namespace final_project
{
    public class GameManager
    {
        public int pos_X = 20;
        public int pos_Y = 17;
        public int speed_x = 0;
        public int speed_y = 0;
        public int score = 0;
        public int energize_timer = 50;
        public bool energize_Trigger = false;
        int dotcounter = 172;

        Display display = new Display();
        string Dots = Display.dots;
        string Map =
    "╔═══════════════════╦═══════════════════╗\n" +
    "║█                 █║█                 █║\n" +
    "║█ █╔═╗█ █╔═════╗█ █║█ █╔═════╗█ █╔═╗█ █║\n" +
    "║█ █╚═╝█ █╚═════╝█ █╨█ █╚═════╝█ █╚═╝█ █║\n" +
    "║█                                     █║\n" +
    "║█ █═══█ █╥█ █══════╦══════█ █╥█ █═══█ █║\n" +
    "║█       █║█       █║█       █║█       █║\n" +
    "╚═════╗█ █╠══════█ █╨█ █══════╣█ █╔═════╝\n" +
    "██████║█ █║█                 █║█ █║██████\n" +
    "══════╝█ █╨█ █╔════█ █════╗█ █╨█ █╚══════\n" +
    "|            █║           ║█            |\n" +
    "══════╗█  ╥█ █║███████████║█ █╥█ █╔══════\n" +
    "██████║█  ║█ █╚═══════════╝█ █║█ █║██████\n" +
    "██████║█  ║█                 █║█ █║██████\n" +
    "╔═════╝█  ╨█ █══════╦══════█ █╨█ █╚═════╗\n" +
    "║█                 █║█                 █║\n" +
    "║█ █══╗█ █═══════█ █╨█ █═══════█ █╔══█ █║\n" +
    "║█   █║█                         █║█   █║\n" +
    "╠══█ █╨█ █╥█ █══════╦══════█ █╥█ █╨█ █══╣\n" +
    "║█       █║█       █║█       █║█       █║\n" +
    "║█ █══════╩══════█ █╨█ █══════╩══════█ █║\n" +
    "║█                                     █║\n" +
    "╚═══════════════════════════════════════╝";
        char[,] dotsArray;
        public bool[,] mapArray;

        public GameManager()
        {
            SetMapArray();
            SetDotsArray();

        }
        public void RunGame()
        {
            Ghost a = new Ghost(20, 8, ConsoleColor.Red, mapArray);
            Ghost b = new Ghost(17, 10, ConsoleColor.Magenta, mapArray);
            Ghost c = new Ghost(20, 10, ConsoleColor.DarkYellow, mapArray);
            Ghost d = new Ghost(23, 10, ConsoleColor.Cyan, mapArray);
            display.RenderMap();
            display.RenderPlayer(pos_X, pos_Y, speed_x, speed_y);
            display.RenderGhost(a.x, a.y, a.tempX, a.tempY, a.color);
            display.RenderGhost(b.x, b.y, b.tempX, b.tempY, b.color);
            display.RenderGhost(c.x, c.y, c.tempX, c.tempY, c.color);
            display.RenderGhost(d.x, d.y, d.tempX, d.tempY, d.color);
            display.RenderDot(dotsArray);
            ReadKey(true);
            while (true)
            {
                display.RenderPlayer(pos_X, pos_Y, speed_x, speed_y);
                display.RenderGhost(a.x, a.y, a.tempX, a.tempY, a.color);
                display.RenderGhost(b.x, b.y, b.tempX, b.tempY, b.color);
                display.RenderGhost(c.x, c.y, c.tempX, c.tempY, c.color);
                display.RenderGhost(d.x, d.y, d.tempX, d.tempY, d.color);
                if (a.Update(pos_X, pos_Y, energize_Trigger) == false)
                {
                    break;
                }
                if (b.Update(pos_X, pos_Y, energize_Trigger) == false)
                {
                    break;
                }
                if (c.Update(pos_X, pos_Y, energize_Trigger) == false)
                {
                    break;
                }
                if (d.Update(pos_X, pos_Y, energize_Trigger) == false)
                {
                    break;
                }
                input();
                updatePos();
                if (addScore() == true)
                {
                    display.RenderDot(dotsArray);
                }
                if (dotcounter == 0)
                {
                    Clear();
                    display.RenderMap();
                    display.RenderPlayer(pos_X, pos_Y, speed_x, speed_y);
                    Task.Delay(2000).Wait();
                    Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(@"
██╗   ██╗ ██████╗ ██╗   ██╗    ██╗    ██╗██╗███╗   ██╗██╗██╗██╗
╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║    ██║██║████╗  ██║██║██║██║
 ╚████╔╝ ██║   ██║██║   ██║    ██║ █╗ ██║██║██╔██╗ ██║██║██║██║
  ╚██╔╝  ██║   ██║██║   ██║    ██║███╗██║██║██║╚██╗██║╚═╝╚═╝╚═╝
   ██║   ╚██████╔╝╚██████╔╝    ╚███╔███╔╝██║██║ ╚████║██╗██╗██╗
   ╚═╝    ╚═════╝  ╚═════╝      ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝╚═╝╚═╝╚═╝
Thanks for playing! Press any key to exit.
");
                    ReadKey(true);
                    Environment.Exit(0);
                    break;
                }
                if (energize_Trigger == true)
                {
                    energize_timer--;
                    if (energize_timer == 0)
                    {
                        energize_Trigger = false;
                        energize_timer = 50;
                    }
                }

                a.NextMove(pos_X, pos_Y);
                b.NextMove(pos_X, pos_Y);
                c.NextMove(pos_X, pos_Y);
                d.NextMove(pos_X, pos_Y);



                Console.SetCursorPosition(42, 1);
                Console.WriteLine("Score : " + score);
                Task.Delay(200).Wait();
            }
            Clear();
            display.RenderMap();
            display.RenderDot(dotsArray);
            display.RenderGhost(a.x, a.y, a.tempX, a.tempY, a.color);
            display.RenderGhost(b.x, b.y, b.tempX, b.tempY, b.color);
            display.RenderGhost(c.x, c.y, c.tempX, c.tempY, c.color);
            display.RenderGhost(d.x, d.y, d.tempX, d.tempY, d.color);

            Task.Delay(2000).Wait();
            Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@"
 ██████╗  █████╗ ███╗   ███╗███████╗     ██████╗ ██╗   ██╗███████╗██████╗ 
██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔═══██╗██║   ██║██╔════╝██╔══██╗
██║  ███╗███████║██╔████╔██║█████╗      ██║   ██║██║   ██║█████╗  ██████╔╝
██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗
╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║
 ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝     ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝
                                                                          
Press any key to exit.
");
            ReadKey(true);
        }

        private void SetDotsArray()
        {
            string[] rows = Dots.Split("\n");
            int rowCount = rows.Length;
            int columnCount = rows[0].Length;
            dotsArray = new char[columnCount, rowCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    dotsArray[column, row] = rows[row][column];
                }
            }
        }
        private void SetMapArray()
        {
            string[] rows = Map.Split("\n");
            int rowCount = rows.Length;
            int columnCount = rows[0].Length;
            mapArray = new bool[columnCount, rowCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    if (Convert.ToString(rows[row][column]) != " ")
                        mapArray[column, row] = true;
                }
            }
        }
        public void input()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.A or ConsoleKey.LeftArrow:
                        if (mapArray[pos_X - 1, pos_Y] != true)
                        {
                            speed_x = -1;
                            speed_y = 0;
                        }
                        break;
                    case ConsoleKey.D or ConsoleKey.RightArrow:
                        if (mapArray[pos_X + 1, pos_Y] != true)
                        {
                            speed_x = 1;
                            speed_y = 0;
                        }
                        break;
                    case ConsoleKey.W or ConsoleKey.UpArrow:
                        if (mapArray[pos_X, pos_Y - 1] != true)
                        {
                            speed_x = 0;
                            speed_y = -1;
                        }
                        break;
                    case ConsoleKey.S or ConsoleKey.DownArrow:
                        if (mapArray[pos_X, pos_Y + 1] != true)
                        {
                            speed_x = 0;
                            speed_y = 1;
                        }
                        break;
                    default: break;
                }
            }
        }
        public void updatePos()
        {
            if (speed_x == 1)
            {
                if (mapArray[pos_X + 1, pos_Y] != true)
                {
                    pos_X++;
                    if (pos_X > 38)
                    {
                        pos_X = 1;
                    }
                }
            }
            else if (speed_x == -1)
            {
                if (mapArray[pos_X - 1, pos_Y] != true)
                {
                    pos_X--;
                    if (pos_X < 2)
                    {
                        pos_X = 38;
                    }
                }
            }
            if (speed_y == 1)
            {
                if (mapArray[pos_X, pos_Y + 1] != true)
                {
                    pos_Y++;
                }
            }
            else if (speed_y == -1)
            {
                if (mapArray[pos_X, pos_Y - 1] != true)
                {
                    pos_Y--;
                }
            }
        }
        public bool addScore()
        {
            if (Convert.ToString(dotsArray[pos_X, pos_Y]) == "·")
            {
                score += 10;
                dotsArray[pos_X, pos_Y] = Convert.ToChar(" ");
                dotcounter--;
                return true;
            }
            else if (Convert.ToString(dotsArray[pos_X, pos_Y]) == "O")
            {
                score += 50;
                dotcounter--;
                if (energize_Trigger == true)
                {
                    energize_timer = 50;
                }
                energize_Trigger = true;
                dotsArray[pos_X, pos_Y] = Convert.ToChar(" ");
                return true;
            }
            else { return false; }
        }
    }
}
