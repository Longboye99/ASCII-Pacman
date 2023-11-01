namespace final_project
{
    internal class PathFinding
    {
        public Tile[,] mapArray;
        public string[] rows;
        Tile start;
        Tile end;

        public int rowCount;
        public int columnCount;


        public static string ghostmap =
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


        public (int, int) RunPathFinding(int startX, int startY, int endX, int endY)
        {


            SetMapArray();
            SetNeighbors();
            start = mapArray[startX, startY];
            end = mapArray[endX, endY];
            List<Tile> openList = new List<Tile>();
            List<Tile> closedList = new List<Tile>();

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

                Tile current = openList[lowestIndex];

                if (openList[lowestIndex] == end)
                {
                    List<Tile> path = new List<Tile>();
                    Tile temp = current;
                    path.Add(temp);
                    while (temp.previous != null)
                    {
                        path.Add(temp.previous);
                        temp = temp.previous;
                    }

                    if (path.Count < 2)
                    {
                        return (path[path.Count - 1].x, path[path.Count - 1].y);
                    }
                    else
                    {
                        return (path[path.Count - 2].x, path[path.Count - 2].y);
                    }
                    //Console.WriteLine("done");

                }

                openList.Remove(current);
                closedList.Add(current);

                List<Tile> neighbors = current.neighbors;
                for (int i = 0; i < neighbors.Count; i++)
                {
                    Tile neighbor = neighbors[i];

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

                        neighbor.h = Heuristic(neighbor, end);
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
            mapArray = new Tile[columnCount, rowCount];

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    if (Convert.ToString(rows[row][column]) == " ")
                    {
                        mapArray[column, row] = new Tile(column, row, columnCount, rowCount);

                    }

                }
            }




        }
        private void SetNeighbors()
        {
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
        public float Heuristic(Tile a, Tile b)
        {
            float d = Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
            return d;
        }
    }

    class Tile
    {
        public int x;
        public int y;
        public float g = 0;
        public float f = 0;
        public float h = 0;
        int columnCount;
        int rowCount;
        public List<Tile> neighbors = new List<Tile>();
        public Tile previous;
        public Tile(int _x, int _y, int _columnCount, int _rowCount)
        {
            x = _x;
            y = _y;
            columnCount = _columnCount;
            rowCount = _rowCount;
        }

        public void AddNeighbors(Tile[,] mapArray)
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
