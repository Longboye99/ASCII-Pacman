using System;

namespace final_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            TitleScreen titlescreen = new TitleScreen();
            PathFinding pathFinding = new PathFinding();

            Display display = new Display();

            int selectOption = titlescreen.RunMenu();
            switch (selectOption)
            {
                case 0:
                    titlescreen.Startgame();
                    break;
                case 1:
                    titlescreen.Controls();
                    break;
                case 2:
                    titlescreen.ExitGame();
                    break;
                case 3: 
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Random rnd = new Random();
                    int x = 0, y = 0;
                    bool temp = true;
                    for (int i = 0; i < 25; i++)
                    {
                        temp = true;
                        while (temp == true)
                        {
                            x = rnd.Next(19, 41);
                            y = rnd.Next(1, 6);
                            if (gameManager.mapArray[x,y] != null)
                            {
                                temp = gameManager.mapArray[x, y];
                            }

                        }
                        Console.WriteLine(x + " , " + y);
                    }
                    break;


            }

        }



    }
}