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
        public bool[,] IsWall;

        public GameManager()
        {
            SetMapArray();
            SetDotsArray();

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
            IsWall = new bool[columnCount, rowCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    if (Convert.ToString(rows[row][column]) != " ")
                        IsWall[column, row] = true;
                }
            }
        }
        public void RunGame()
        {
            Ghost a = new Ghost(20, 8, ConsoleColor.Red, IsWall);
            Ghost b = new Ghost(17, 10, ConsoleColor.Magenta, IsWall);
            Ghost c = new Ghost(20, 10, ConsoleColor.DarkYellow, IsWall);
            Ghost d = new Ghost(23, 10, ConsoleColor.Cyan, IsWall);
            display.RenderMap();
            
            while (true)
            {
                display.RenderPlayer(pos_X, pos_Y, speed_x, speed_y);
                display.RenderGhost(a.x, a.y, a.tempX, a.tempY, a.color);
                display.RenderGhost(b.x, b.y, b.tempX, b.tempY, b.color);
                display.RenderGhost(c.x, c.y, c.tempX, c.tempY, c.color);
                display.RenderGhost(d.x, d.y, d.tempX, d.tempY, d.color);
                if (a.UpdateGhostState(pos_X, pos_Y, energize_Trigger) == false)
                {
                    break;
                }
                if (b.UpdateGhostState(pos_X, pos_Y, energize_Trigger) == false)
                {
                    break;
                }
                if (c.UpdateGhostState(pos_X, pos_Y, energize_Trigger) == false)
                {
                    break;
                }
                if (d.UpdateGhostState(pos_X, pos_Y, energize_Trigger) == false)
                {
                    break;
                }
                Input();
                UpdatePlayerPos();
                if (UpdateDots() == true)
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
                    Console.WriteLine(@$"
██╗   ██╗ ██████╗ ██╗   ██╗    ██╗    ██╗██╗███╗   ██╗██╗██╗██╗
╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║    ██║██║████╗  ██║██║██║██║
 ╚████╔╝ ██║   ██║██║   ██║    ██║ █╗ ██║██║██╔██╗ ██║██║██║██║
  ╚██╔╝  ██║   ██║██║   ██║    ██║███╗██║██║██║╚██╗██║╚═╝╚═╝╚═╝
   ██║   ╚██████╔╝╚██████╔╝    ╚███╔███╔╝██║██║ ╚████║██╗██╗██╗
   ╚═╝    ╚═════╝  ╚═════╝      ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝╚═╝╚═╝╚═╝
Your score is {score}.
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

                a.GhostNextPos(pos_X, pos_Y);
                b.GhostNextPos(pos_X, pos_Y);
                c.GhostNextPos(pos_X, pos_Y);
                d.GhostNextPos(pos_X, pos_Y);



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

        public void Input()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.A or ConsoleKey.LeftArrow:
                        if (IsWall[pos_X - 1, pos_Y] != true)
                        {
                            speed_x = -1;
                            speed_y = 0;
                        }
                        break;
                    case ConsoleKey.D or ConsoleKey.RightArrow:
                        if (IsWall[pos_X + 1, pos_Y] != true)
                        {
                            speed_x = 1;
                            speed_y = 0;
                        }
                        break;
                    case ConsoleKey.W or ConsoleKey.UpArrow:
                        if (IsWall[pos_X, pos_Y - 1] != true)
                        {
                            speed_x = 0;
                            speed_y = -1;
                        }
                        break;
                    case ConsoleKey.S or ConsoleKey.DownArrow:
                        if (IsWall[pos_X, pos_Y + 1] != true)
                        {
                            speed_x = 0;
                            speed_y = 1;
                        }
                        break;
                    default: break;
                }
            }
        }
        public void UpdatePlayerPos()
        {
            if (speed_x == 1)
            {
                if (IsWall[pos_X + 1, pos_Y] != true)
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
                if (IsWall[pos_X - 1, pos_Y] != true)
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
                if (IsWall[pos_X, pos_Y + 1] != true)
                {
                    pos_Y++;
                }
            }
            else if (speed_y == -1)
            {
                if (IsWall[pos_X, pos_Y - 1] != true)
                {
                    pos_Y--;
                }
            }
        }
        public bool UpdateDots()
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
