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
            state = Wander;
            color = ghostcolor;
            tempcolor = ghostcolor;
        }
        private (int, int) Wander(int player_X, int player_Y)
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
                state = Chase;
            }
            return (targetX, targetY);
        }
        private (int, int) Chase(int player_X, int player_Y)
        {
            frameCounter++;
            if (frameCounter == 50)
            {
                frameCounter = 0;
                state = Wander;
            }
            targetX = player_X;
            targetY = player_Y;
            return (targetX, targetY);
        }
        private (int, int) Scared(int player_X, int player_Y)
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
                state = Wander;
            }
            return (targetX, targetY);
        }
        public (int, int) Dead(int player_X, int player_Y)
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
                    state = Wander;
                }

            }
            targetX = homeX; targetY = homeY;
            return (targetX, targetY);
        }
        public void GhostNextPos(int player_X, int player_Y)
        {

            tempX = x;
            tempY = y;
            state(player_X, player_Y);
            (x, y) = pathFinding.RunPathFinding(x, y, targetX, targetY);
        }
        public bool UpdateGhostState(int player_X, int player_Y, bool energize)
        {
            if (state != Dead)
            {
                if (player_X == x && player_Y == y && energize == true)
                {
                    frameCounter = 0;
                    state = Dead;
                    return true;
                }
                else if (player_X == x && player_Y == y && energize == false)
                {
                    return false;
                }
                else if (energize == true)
                {
                    if (state != Scared)
                    {
                        state = Scared;
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
