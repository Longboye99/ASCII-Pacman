namespace final_project
{
    class Ghost
    {
        public int x;
        public int y;
        public int homeX;
        public int homeY;
        public int tempX;
        public int tempY;
        public int targetX;
        public int targetY;
        public int startwanderX, endwanderX, startwanderY, endwanderY;

        public ConsoleColor color;
        public ConsoleColor tempcolor;
        public int frameCounter = 0;
        public bool[,] IsWall;
        public delegate (int, int) State(int player_X, int player_Y);
        public State state;
        bool hasTarget = false;
        PathFinding pathFinding = new PathFinding();

        public Ghost(int _x, int _y, ConsoleColor ghostcolor, bool[,] mapArray)
        {
            x = _x; y = _y;
            homeX = x; homeY = y;
            tempX = x; tempY = y + 1;
            IsWall = mapArray;
            state = wander;
            color = ghostcolor;
            tempcolor = ghostcolor;
        }
        private (int, int) wander(int player_X, int player_Y)
        {

            Random rnd = new Random();
            bool temp = true;
            if (targetX == x && targetY == y) { hasTarget = false; }
            while (hasTarget == false)
            {
                targetX = rnd.Next(0, 40);
                targetY = rnd.Next(0, 22);
                temp = IsWall[targetX, targetY];
                if (temp == false)
                    hasTarget = true;
            }

            frameCounter++;
            if (frameCounter == 15)
            {
                frameCounter = 0;
                state = chase;
            }
            return (targetX, targetY);
        }
        private (int, int) chase(int player_X, int player_Y)
        {
            frameCounter++;
            if (frameCounter == 50)
            {
                frameCounter = 0;
                state = wander;
            }
            targetX = player_X;
            targetY = player_Y;
            return (targetX, targetY);
        }
        private (int, int) frighten(int player_X, int player_Y)
        {
            Random rnd = new Random();
            color = ConsoleColor.Blue;
            int xdif, ydif;
            xdif = x - player_X;
            ydif = y - player_Y;
            if (frameCounter % 2 == 1)
            {
                if (xdif < 0)
                {
                    if (IsWall[x - 1, y] == false)
                    {
                        targetX = x - 1;
                    }
                    else if (IsWall[x - 1, y] == true)
                    {
                        if (IsWall[x, y + 1] == false)
                        {
                            targetX = x;
                            targetY = y + 1;
                        }
                        else if (IsWall[x, y - 1] == true)
                        {
                            targetX = x;
                            targetY = y - 1;
                        }
                    }
                    else { targetX = x; targetY = y; }
                }
                else if (xdif > 0)
                {
                    if (IsWall[x + 1, y] == false)
                    {
                        targetX = x + 1;
                    }
                    else if (IsWall[x + 1, y] == true)
                    {
                        if (IsWall[x, y + 1] == false)
                        {
                            targetX = x;
                            targetY = y + 1;
                        }
                        else if (IsWall[x, y - 1] == true)
                        {
                            targetX = x;
                            targetY = y - 1;
                        }
                    }
                    else { targetX = x; targetY = y; }
                }
                else if (ydif > 0)
                {
                    if (IsWall[x, y + 1] == false)
                    {
                        targetY = y + 1;
                    }
                    else if (IsWall[x, y + 1] == true)
                    {
                        if (IsWall[x + 1, y] == false)
                        {
                            targetX = x + 1;
                            targetY = y;
                        }
                        else if (IsWall[x - 1, y] == true)
                        {
                            targetX = x - 1;
                            targetY = y;
                        }
                    }
                    else { targetX = x; targetY = y; }
                }
                else if (ydif < 0)
                {
                    if (IsWall[x, y - 1] == false)
                    {
                        targetY = y - 1;
                    }
                    else if (IsWall[x, y - 1] == true)
                    {
                        if (IsWall[x + 1, y] == false)
                        {
                            targetX = x + 1;
                            targetY = y;
                        }
                        else if (IsWall[x - 1, y] == true)
                        {
                            targetX = x - 1;
                            targetY = y;
                        }
                    }
                    else { targetX = x; targetY = y; }
                }
                else { targetX = x; targetY = y; }
            }
            else { targetX = x; targetY = y; }
            frameCounter++;
            if (frameCounter == 30)
            {
                frameCounter = 0;
                color = tempcolor;
                state = wander;
            }
            return (targetX, targetY);
        }
        public (int, int) dead(int player_X, int player_Y)
        {
            color = ConsoleColor.Black;

            if (x == homeX && y == homeY)
            {
                tempX = x; tempY = y + 1;
                frameCounter++;
                if (frameCounter == 15)
                {
                    color = tempcolor;
                    frameCounter = 0;
                    state = wander;
                }

            }
            targetX = homeX; targetY = homeY;
            return (targetX, targetY);
        }
        public void NextMove(int player_X, int player_Y)
        {

            tempX = x;
            tempY = y;
            state(player_X, player_Y);
            (x, y) = pathFinding.Run(x, y, targetX, targetY);
        }
        public bool Update(int player_X, int player_Y, bool energize)
        {
            if (state != dead)
            {
                if (player_X == x && player_Y == y && energize == true)
                {
                    frameCounter = 0;
                    state = dead;
                    return true;
                }
                else if (player_X == x && player_Y == y && energize == false)
                {
                    return false;
                }
                else if (energize == true)
                {
                    if (state != frighten)
                    {
                        state = frighten;
                        frameCounter = 0;
                    }
                    return true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }

}
