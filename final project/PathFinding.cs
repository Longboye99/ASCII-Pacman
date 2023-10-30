namespace final_project
{
    internal class PathFinding
    {
        public Spot[,] mapArray;
        public string[] rows;
        Spot start;
        Spot end;

        public int rowCount;
        public int columnCount;


        private static string ghostmap =
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
    "             █║           ║█             \n" +
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

        public PathFinding()
        {

        }

        public (int, int) Run(int startX, int startY, int endX, int endY)
        {


            SetMapArray();
            start = mapArray[startX, startY];
            end = mapArray[endX, endY];
            List<Spot> openList = new List<Spot>();
            List<Spot> closedList = new List<Spot>();

            openList.Add(start);

            while (openList.Count > 0)
            {
                int lowestIndex = 0;
                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].f < openList[lowestIndex].f)
                    {
                        lowestIndex = i;
                    }
                }

                Spot current = openList[lowestIndex];

                if (openList[lowestIndex] == end)
                {
                    List<Spot> path = new List<Spot>();
                    Spot temp = current;
                    path.Add(temp);
                    while (temp.previous != null)
                    {
                        path.Add(temp.previous);
                        temp = temp.previous;
                    }

                    return (path[path.Count - 2].x, path[path.Count - 2].y);
                    //Console.WriteLine("done");

                }

                openList.Remove(current);
                closedList.Add(current);

                List<Spot> neighbors = current.neighbors;
                for (int i = 0; i < neighbors.Count; i++)
                {
                    Spot neighbor = neighbors[i];

                    if (closedList.Contains(neighbor) == false)
                    {
                        float tempG = current.g + 1;
                        if (openList.Contains(neighbor))
                        {
                            if (tempG < neighbor.g)
                            {
                                neighbor.g = tempG;
                            }
                        }
                        else
                        {
                            neighbor.g = tempG;
                            openList.Add(neighbor);
                        }

                        neighbor.h = heuristic(neighbor, end);
                        neighbor.f = neighbor.g + neighbor.h;
                        neighbor.previous = current;
                    }
                }
            }
            return (0, 0);

        }


        private void SetMapArray()
        {
            rows = ghostmap.Split("\n");
            rowCount = rows.Length;
            columnCount = rows[0].Length;
            mapArray = new Spot[columnCount, rowCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    if (Convert.ToString(rows[row][column]) == " ")
                    {
                        mapArray[column, row] = new Spot(column, row, columnCount, rowCount);
                    }

                }
            }

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    if (Convert.ToString(rows[row][column]) == " ")
                    {
                        mapArray[column, row].AddNeighbors(mapArray);
                    }

                }
            }


        }

        public float heuristic(Spot a, Spot b)
        {
            float d = Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
            return d;
        }



    }

    class Spot
    {
        public int x;
        public int y;
        public float g = 0;
        public float f = 0;
        public float h = 0;
        int columnCount;
        int rowCount;
        public List<Spot> neighbors = new List<Spot>();
        public Spot previous;
        public Spot(int _x, int _y, int _columnCount, int _rowCount)
        {
            x = _x;
            y = _y;
            columnCount = _columnCount;
            rowCount = _rowCount;
        }

        public void AddNeighbors(Spot[,] mapArray)
        {
            if (x < columnCount - 1)
            {
                if (mapArray[x + 1, y] != null)
                {
                    neighbors.Add(mapArray[x + 1, y]);
                }
            }
            if (x > 0)
            {
                if (mapArray[x - 1, y] != null)
                {
                    neighbors.Add(mapArray[x - 1, y]);
                }
            }
            if (y < rowCount - 1)
            {
                if (mapArray[x, y + 1] != null)
                {
                    neighbors.Add(mapArray[x, y + 1]);
                }
            }
            if (y > 0)
            {
                if (mapArray[x, y - 1] != null)
                {
                    neighbors.Add(mapArray[x, y - 1]);
                }
            }
        }

    }

}
