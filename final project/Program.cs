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
                    titlescreen.StartGame();
                    break;
                case 1:
                    titlescreen.Controls();
                    break;
                case 2:
                    titlescreen.ExitGame();
                    break;

            }

        }



    }
}