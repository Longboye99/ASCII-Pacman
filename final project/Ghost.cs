namespace final_project
{
    internal class Ghost
    {
        public int ghost_X;
        public int ghost_Y;
        public int tempX;
        public int tempY;
        public ConsoleColor ghostColor;
        PathFinding pathFinding = new PathFinding();
        public Ghost(int x, int y, ConsoleColor color)
        {
            ghost_X = x;
            ghost_Y = y;
            tempX = 100;
            tempY = 100;


            ghostColor = color;
        }

        public void GhostNextPos(int player_X, int player_Y)
        {

            tempX = ghost_X;
            tempY = ghost_Y;

            (ghost_X, ghost_Y) = pathFinding.Run(ghost_X, ghost_Y, player_X, player_Y);
        }
    }
}
