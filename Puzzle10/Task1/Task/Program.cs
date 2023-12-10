using System.Drawing;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    public static char[,] _treeMap;
    public static int _x;
    public static int _y;
    
    public static Tuple<int, int> _start;
    public static Point _startPipe;
    public static Point _endPipe;

    public static Dictionary<Point, int> _visited = new Dictionary<Point, int>();
    public static Dictionary<Point, int> _unvisited = new Dictionary<Point, int>();
    public static Dictionary<Point, int> _distanceFromStart = new Dictionary<Point, int>();
    public static Dictionary<Point, int> _notInPipe = new Dictionary<Point, int>();


    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        _x = lines.First().Length;
        _y = lines.Count();
        _treeMap = new char[_x, _y];
     

        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {

                _treeMap[i, j] = (lines[j].ToCharArray()[i]);
            }


        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                if (lines[j].ToCharArray()[i] == 'S')
                {
                    _treeMap[i, j] = 'S';
                    _start = new Tuple<int, int> (i, j);
                }
            }

       


        var startNeighbours = new List<Point>();

        var x = _start.Item1;
        var y = _start.Item2;


        if (x - 1 >= 0)
        {
            var sign = _treeMap[x - 1, y];
            /*going one west*/
            var canmove = sign == '-' ||
                sign == 'F' ||
                sign == 'L';

            if (canmove)
                startNeighbours.Add(new Point(x-1, y));

        }
        if (x + 1 >= 0)
        {

            var sign = _treeMap[x + 1, y];
            /*going one west*/
            var canmove = sign == '-'
                 || sign == 'J'
                 || sign == '7';


            if (canmove)
                startNeighbours.Add(new Point(x + 1, y));

        }
        if (y - 1 >= 0)
        {

            var sign = _treeMap[x, y - 1];
            /*going one west*/
            var canmove = _treeMap[x, y - 1] == '|'
               || _treeMap[x, y - 1] == '7' || _treeMap[x, y - 1] == 'F';

            if (canmove)
                startNeighbours.Add(new Point(x, y-1));

        }
        if (y + 1 >= 0)
        {

            var sign = _treeMap[x, y + 1];
            /*going one west*/
            var canmove = _treeMap[x, y + 1] == '|' || _treeMap[x, y + 1] == 'L'
                || _treeMap[x, y + 1] == 'J';

            if (canmove)
                startNeighbours.Add(new Point(x, y + 1));

        }

        _startPipe =  startNeighbours.First();
        _endPipe = startNeighbours.Last();


        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                if (_startPipe.X == i && _startPipe.Y == j)
                    _distanceFromStart.Add(new Point(i, j), 0);
               

                _unvisited.Add(new Point(i, j), 0);
            }

        while (_unvisited.Count > 0)
        {

            var toVisit = _distanceFromStart.Where(o => !_visited.ContainsKey(o.Key) && _unvisited.ContainsKey(o.Key)).OrderBy(o => o.Value).Select(o=>o.Key).FirstOrDefault();
            if (_visited.ContainsKey(_endPipe))
                    break;

                VisitNode(toVisit.X, toVisit.Y);
                _visited.Add(toVisit, 0);
                _unvisited.Remove(toVisit);
        }

       var test = _distanceFromStart.OrderByDescending(o => o.Value).ToList();

    }

    //public List<Point> GetShortestPathDijkstra()
    //{
    //    var shortestPath = new List<Point>();
    //    shortestPath.Add(_endPipe);
    //    BuildShortestPath(shortestPath, _endPipe);
    //    shortestPath.Reverse();
    //    return shortestPath;
    //}

    public static void VisitNode(int x, int y)
    {
        if (x - 1 >= 0)
        {
            /*going one west*/
            var canmove = _treeMap[x - 1, y] == '-' ||
                _treeMap[x - 1, y] == 'F' ||
                _treeMap[x - 1, y] == 'L';


            var next = new Point(x - 1, y);
            if (canmove)
            {
                var newdistance = 1 + _distanceFromStart[new Point(x, y)];
                var neighbour = next;

                if (!_distanceFromStart.ContainsKey(neighbour))
                    _distanceFromStart.Add(neighbour, int.MaxValue);

                if (_distanceFromStart[neighbour] > newdistance)
                {
                    _distanceFromStart[neighbour] = newdistance;
                }
            }
        }

        if (x + 1 < _x)
        {
            /*going one east*/
            var canmove = _treeMap[x + 1, y] == '-'
                || _treeMap[x + 1, y] == 'J'
                || _treeMap[x + 1, y] == '7';

            var next = new Point(x + 1, y);

            if (canmove)
            {
                var newdistance = 1 + _distanceFromStart[new Point(x, y)];
                var neighbour = next;
                if (!_distanceFromStart.ContainsKey(neighbour))
                    _distanceFromStart.Add(neighbour, int.MaxValue);

                if (_distanceFromStart[neighbour] > newdistance)
                {
                    _distanceFromStart[neighbour] = newdistance;
                }

            }

        }

        if (y - 1 >= 0)
        {
            /*going north*/
            var canMove = _treeMap[x, y - 1] == '|'
                || _treeMap[x, y - 1] == '7' || _treeMap[x, y - 1] == 'F';

            var next = new Point(x, y-1);

            if (canMove )
            {
                var newdistance = 1 + _distanceFromStart[new Point(x, y)];
                var neighbour = next;
                if (!_distanceFromStart.ContainsKey(neighbour))
                    _distanceFromStart.Add(neighbour, int.MaxValue);

                if (_distanceFromStart[neighbour] > newdistance)
                {
                    _distanceFromStart[neighbour] = newdistance;
                }


            }
        }


        if (y + 1 < _y)
        {
            /*going south*/
            var canmove = _treeMap[x, y + 1] == '|' || _treeMap[x, y + 1] == 'L'
                || _treeMap[x, y + 1] == 'J';

            var next = new Point(x, y + 1);

            if (canmove)
            {
                var newdistance = 1 + _distanceFromStart[new Point(x, y)];
                var neighbour = next;
                if (!_distanceFromStart.ContainsKey(neighbour))
                    _distanceFromStart.Add(neighbour, int.MaxValue);

                if (_distanceFromStart[neighbour] > newdistance)
                {
                    _distanceFromStart[neighbour] = newdistance;
                }
            }
        }
    }


    //public record struct PointInArray : IComparable
    //{
    //    public PointInArray(int X, int Y)
    //    {
    //        this.X = X;
    //        this.Y = Y;
    //    }

    //    public int X { get; init; }
    //    public int Y { get; init; }

    //    public int CompareTo(object? obj)
    //    {
    //        var t? = obj as PointInArray;
    //        if (t == null)
    //            return 1;

    //        if (X.com)
    //    }
    //}
}

