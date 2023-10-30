using static System.Console;

namespace final_project
{
    internal class TitleScreen
    {
        private int selectOption = 0;
        private string[] option = { "Start", "Controls", "Exit", "Test" };

        public void displayTitle()
        {
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine(@"
██████╗  █████╗  ██████╗███╗   ███╗ █████╗ ███╗   ██╗
██╔══██╗██╔══██╗██╔════╝████╗ ████║██╔══██╗████╗  ██║
██████╔╝███████║██║     ██╔████╔██║███████║██╔██╗ ██║
██╔═══╝ ██╔══██║██║     ██║╚██╔╝██║██╔══██║██║╚██╗██║
██║     ██║  ██║╚██████╗██║ ╚═╝ ██║██║  ██║██║ ╚████║
╚═╝     ╚═╝  ╚═╝ ╚═════╝╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝
Welcome to PACMAN. What would you like to do?
(Use WASD or Arrow Keys to cycle through options and press ENTER to select an option.)
");
            for (int i = 0; i < option.Length; i++)
            {
                string currentOption = option[i];
                string pointer;

                if (i == selectOption)
                {
                    pointer = ">>";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    pointer = "  ";
                    ForegroundColor = ConsoleColor.DarkYellow;
                    BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"{pointer} [{currentOption}]");
            }
            ResetColor();
        }
        public int RunMenu()
        {
            ConsoleKey key;
            do
            {
                Clear();
                displayTitle();


                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.W or ConsoleKey.UpArrow:
                        selectOption--;
                        if (selectOption == -1)
                        {
                            selectOption = option.Length - 1;
                        }
                        break;
                    case ConsoleKey.S or ConsoleKey.DownArrow:
                        selectOption++;
                        if (selectOption == option.Length)
                        {
                            selectOption = 0;
                        }
                        break;
                    default:
                        break;
                }

            } while (key != ConsoleKey.Enter);
            return selectOption;
        }

        public void Startgame()
        {
            Clear();
            GameManager gameManager = new GameManager();
            gameManager.RunGame();
        }

        public void Controls()
        {
            Clear();
            Console.WriteLine("[Controls]");
            Console.WriteLine("Use WASD or Arrow Keys to move.");
            Console.WriteLine("Press any key to return to the menu.");
            ReadKey(true);
            RunMenu();
        }

        public void ExitGame()
        {
            Clear();
            Console.WriteLine("Press any key to exit...");
            ReadKey(true);
            Environment.Exit(0);
        }
    }
}
