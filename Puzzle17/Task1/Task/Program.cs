using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Xml.Linq;


class Program
{

    public static int[,] _treeMap;
    public static int _x;
    public static int _y;

    public static Point _startPipe;
    public static Point _endPipe;

    public static Dictionary<Node, int> _visited = new Dictionary<Node, int>();
    public static Dictionary<Node, int> _unvisited = new Dictionary<Node, int>();
    public static Dictionary<Node, int> _distanceFromStart = new Dictionary<Node, int>();

  

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        _x = lines.First().Length;
        _y = lines.Count();
        _treeMap = new int[_x, _y];


        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {

                _treeMap[i, j] = int.Parse((lines[j].ToCharArray()[i]).ToString());
            }



        var startNeighbours = new List<Point>();

        _startPipe = new Point(0, 0);
        _endPipe = new Point(_x-1, _y-1);


        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                if (_startPipe.X == i && _startPipe.Y == j)
                    _distanceFromStart.Add(new Node(new Point(0, 0), new Point(0, 0)), 0);

                if (i>0)
                    _unvisited.Add(new Node(new Point(i, j), new Point(i-1, j)), 0);

                if (i < _x-1)
                    _unvisited.Add(new Node(new Point(i, j), new Point(i + 1, j)), 0);

                if (j > 0)
                    _unvisited.Add(new Node(new Point(i, j), new Point(i, j-1)), 0);

                if (j < _y - 1)
                    _unvisited.Add(new Node(new Point(i, j), new Point(i, j+1)), 0);

            }

        while (_unvisited.Count > 0)
        {

            var toVisit = _distanceFromStart.Where(o => !_visited.ContainsKey(o.Key) && _unvisited.ContainsKey(o.Key)).OrderBy(o=>o.Value._distance).Select(o => o.Key).FirstOrDefault();
         
            VisitNode(toVisit.X, toVisit.Y);
            _visited.Add(toVisit, 0);
            _unvisited.Remove(toVisit);
        }

        //var test = _distanceFromStart.OrderByDescending(o => o.Value).ToList();



        Console.WriteLine(_distanceFromStart[_endPipe]._distance);
        Console.ReadKey();
    }

    public static void printmap(char[,] array, int x, int y)
    {

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Console.Write(array[j, i]);
                

            }
            Console.WriteLine();
        }

    }


    public static void VisitNode(Node node)
    {
   
        var cantMoveLeft = false; 
        var cantMoveRight = false;
        var cantMoveUp = false;
        var cantMoveDown = false;


        if (_distanceFromStart.ContainsKey(node))
        {
            Point previousOne = node._neighbour;

            if (previousOne != null && _distanceFromStart.ContainsKey(previousOne._point))
            {
                PreviousNode previousTwo = _distanceFromStart[previousOne._point];

                if (previousTwo != null)
                {

                    if (y == previousOne._point.Y && y == previousTwo._point.Y && x == previousOne._point.X - 1 && x == previousTwo._point.X - 2)
                        cantMoveLeft = true;

                    if (y == previousOne._point.Y && y == previousTwo._point.Y && x == previousOne._point.X + 1 && x == previousTwo._point.X + 2)
                        cantMoveRight = true;

                    if (y == previousOne._point.Y-1 && y == previousTwo._point.Y-2 && x == previousOne._point.X && x == previousTwo._point.X)
                        cantMoveUp = true;

                    if (y == previousOne._point.Y+1 && y == previousTwo._point.Y+2 && x == previousOne._point.X && x == previousTwo._point.X)
                        cantMoveDown = true;

                }

            }
        }

        if (x - 1 >= 0 && !cantMoveLeft)
        {
            /*going one west*/
            var next = new Point(x - 1, y);
            var curr = new Point(x, y);
            var newdistance = _treeMap[x-1, y] + _distanceFromStart[curr]._distance;
            var neighbour = next;

            if (!_distanceFromStart.ContainsKey(neighbour))
                _distanceFromStart.Add(neighbour, new PreviousNode(curr, newdistance));

            if (_distanceFromStart[neighbour]._distance > newdistance)
            {
                _distanceFromStart[neighbour] = new PreviousNode(curr, newdistance);
            }

        }

        if (x + 1 < _x && !cantMoveRight)
        {
            /*going one east*/
            var next = new Point(x + 1, y);
            var curr = new Point(x, y);

            var newdistance = _treeMap[x + 1, y] + _distanceFromStart[curr]._distance;
            var neighbour = next;

            if (!_distanceFromStart.ContainsKey(neighbour))
                _distanceFromStart.Add(neighbour, new PreviousNode(curr, newdistance));

            if (_distanceFromStart[neighbour]._distance > newdistance)
            {
                _distanceFromStart[neighbour] = new PreviousNode(curr, newdistance);
            }

        }

        if (y - 1 >= 0 && !cantMoveUp)
        {
            /*going north*/
           
            var next = new Point(x, y - 1);
            var curr = new Point(x, y);
            var newdistance = _treeMap[x, y-1] + _distanceFromStart[curr]._distance;
            var neighbour = next;

            if (!_distanceFromStart.ContainsKey(neighbour))
                _distanceFromStart.Add(neighbour, new PreviousNode(curr, newdistance));

            if (_distanceFromStart[neighbour]._distance > newdistance)
            {
                _distanceFromStart[neighbour] = new PreviousNode(curr, newdistance);
            }

        }


        if (y + 1 < _y && !cantMoveDown)
        {
            /*going south*/
         
            var next = new Point(x, y + 1);
            var curr = new Point(x, y);

            var newdistance = _treeMap[x, y+1] + _distanceFromStart[curr]._distance;
            var neighbour = next;

            if (!_distanceFromStart.ContainsKey(neighbour))
                _distanceFromStart.Add(neighbour, new PreviousNode(curr, newdistance));

            if (_distanceFromStart[neighbour]._distance > newdistance)
            {
                _distanceFromStart[neighbour] = new PreviousNode(curr, newdistance);
            }
        }
    }

    public struct Node
    {
        public Point _current;
        public Point _neighbour;
      
        public Node(Point current, Point neighbour)
        { 
        
            _current = current;
            _neighbour = neighbour;
            
        }


        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var curr = obj is Node;
            if (!curr)
                return false;

            return ((Node)obj)._current.Equals(this._current) && ((Node)obj)._neighbour.Equals(this._neighbour); 
        }

    }


}

