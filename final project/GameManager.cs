﻿using static System.Console;

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
        int dotcounter = 168;

        Display display = new Display();
        string Dots = Display.dots;
        string Map = Display.map;
        char[,] dotsArray;
        public bool[,] mapArray;

        Ghost ghost = new Ghost(16, 10, ConsoleColor.Red);


        public GameManager()
        {
            SetMapArray();
            SetDotsArray();
        }
        public void RunGame()
        {
            display.RenderMap();
            display.RenderGhost(ghost.ghost_X, ghost.ghost_Y, ghost.tempX, ghost.tempY, ghost.ghostColor);
            display.RenderDot(dotsArray);
            //ReadKey(true);
            while (true)
            {
                display.RenderPlayer(pos_X, pos_Y, speed_x, speed_y);

                display.RenderGhost(ghost.ghost_X, ghost.ghost_Y, ghost.tempX, ghost.tempY, ghost.ghostColor);

                if (addScore() == true)
                {
                    display.RenderDot(dotsArray);
                }

                if (dotcounter == 0)
                {
                    Clear();
                    Console.WriteLine("You win");
                    break;
                }

                if (pos_X == ghost.ghost_X && pos_Y == ghost.ghost_Y)
                {
                    if (energize_Trigger == true)
                    {

                    }
                    else
                    {
                        Clear();
                        Console.WriteLine("You lost");
                        break;
                    }
                }

                input();

                updatePos();

                if (energize_Trigger == true)
                {
                    energize_timer--;
                    if (energize_timer == 0)
                    {
                        energize_Trigger = false;
                        energize_timer = 50;
                    }
                }
                ghost.GhostNextPos(pos_X, pos_Y);




                Console.SetCursorPosition(42, 1);
                Console.WriteLine(speed_x + " and " + speed_y);
                Task.Delay(500).Wait();
            }
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
                        if (mapArray[pos_X - 2, pos_Y] != true)
                        {
                            speed_x = -1;
                            speed_y = 0;
                        }
                        break;
                    case ConsoleKey.D or ConsoleKey.RightArrow:
                        if (mapArray[pos_X + 2, pos_Y] != true)
                        {
                            speed_x = 1;
                            speed_y = 0;
                        }
                        break;
                    case ConsoleKey.W or ConsoleKey.UpArrow:
                        if (mapArray[pos_X, pos_Y - 1] != true && mapArray[pos_X + 1, pos_Y - 1] != true && mapArray[pos_X - 1, pos_Y - 1] != true)
                        {
                            speed_x = 0;
                            speed_y = -1;
                        }
                        break;
                    case ConsoleKey.S or ConsoleKey.DownArrow:
                        if (mapArray[pos_X, pos_Y + 1] != true && mapArray[pos_X + 1, pos_Y + 1] != true && mapArray[pos_X - 1, pos_Y + 1] != true)
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
                if (mapArray[pos_X + 2, pos_Y] != true)
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
                if (mapArray[pos_X - 2, pos_Y] != true)
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
                if (mapArray[pos_X, pos_Y + 1] != true && mapArray[pos_X + 1, pos_Y + 1] != true && mapArray[pos_X - 1, pos_Y + 1] != true)
                {
                    pos_Y++;
                }
            }
            else if (speed_y == -1)
            {
                if (mapArray[pos_X, pos_Y - 1] != true && mapArray[pos_X + 1, pos_Y - 1] != true && mapArray[pos_X - 1, pos_Y - 1] != true)
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
