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
                    /*case 3: 
                        Console.Clear();

                        pathFinding.Run(16,10, 20,17);                                    
                        foreach (var item in pathFinding.path)
                        {
                            Console.SetCursorPosition(item.x,item.y);
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(" ");
                        }
                        display.RenderMap();
                        Console.ReadLine();
                        Console.Clear();

                        pathFinding.Run(16, 10, 21, 17);
                        foreach (var item in pathFinding.path)
                        {
                            Console.SetCursorPosition(item.x, item.y);
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(" ");
                        }
                        display.RenderMap();
                        Console.ReadLine();

                        Console.Clear();

                        pathFinding.Run(16, 10, 22, 17);
                        foreach (var item in pathFinding.path)
                        {
                            Console.SetCursorPosition(item.x, item.y);
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(" ");
                        }
                        display.RenderMap();
                        break;*/
            }

        }



    }
}