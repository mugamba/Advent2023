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
    public static Dictionary<Point, Tuple<Point, int>> _distanceFromStart = new Dictionary<Point, Tuple<Point, int>>();

    public static List<Point> _pipe = new List<Point>();
    public static List<Point> _insidePipe = new List<Point>();


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
                    _start = new Tuple<int, int>(i, j);
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
                startNeighbours.Add(new Point(x - 1, y));

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
                startNeighbours.Add(new Point(x, y - 1));

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

        _startPipe = startNeighbours.First();
        _endPipe = startNeighbours.Last();


        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                if (_startPipe.X == i && _startPipe.Y == j)
                    _distanceFromStart.Add(new Point(i, j), new Tuple<Point, int>(new Point(_start.Item1, _start.Item2), 0));

                _unvisited.Add(new Point(i, j), 0);
            }

        while (_unvisited.Count > 0)
        {

            var toVisit = _distanceFromStart.Where(o => !_visited.ContainsKey(o.Key) && _unvisited.ContainsKey(o.Key)).OrderBy(o=>o.Value.Item2).Select(o => o.Key).FirstOrDefault();
            if (_visited.ContainsKey(_endPipe))
                break;

            VisitNode(toVisit.X, toVisit.Y);
            _visited.Add(toVisit, 0);
            _unvisited.Remove(toVisit);
        }

        //var test = _distanceFromStart.OrderByDescending(o => o.Value).ToList();

        var temp = _endPipe;
        _pipe.Add(temp);
        while (temp != new Point(_start.Item1, _start.Item2))
        {
            temp = _distanceFromStart[temp].Item1;
            _pipe.Add(temp);

        }


      

         var pictreeMap = new char[_x, _y];

        for(int i=0; i<_x; i++) 
        { 
            for(int j=0; j<_y; j++)
            {
                pictreeMap[i, j] = '.';

            }
        }

        foreach(var p in _pipe) 
        {
            pictreeMap[p.X, p.Y] = _treeMap[p.X, p.Y];
        }

        for (int i = 0; i < _x; i++)
        {
            var lastenteringcharacter = 'c';
            var inside = false;
            for (int j = 0; j < _y; j++)
            {
                var current = pictreeMap[i, j];

                if (inside == false && (current == '7' || current == 'F' || current == '-'))
                {
                    inside = !inside;
                    lastenteringcharacter = current;
                }
                if (inside == true) 
                {
                    if (current == '-')
                    {
                        inside = !inside;
                    }
                    else
                    { 
                        if (current == 'J' && lastenteringcharacter == '7')
                            inside = !inside;

                        if (current == 'L' && lastenteringcharacter == 'F')
                            inside = !inside;

                    }
                }
                if (inside && current == '.')
                {
                    _insidePipe.Add(new Point(i, j));
                    pictreeMap[i, j] = '@';

                }

            }

        }
                //    }
                //}

                printmap(pictreeMap, _x, _y);
        Console.WriteLine(_insidePipe.Count);
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

    public static void VisitNode(int x, int y)
    {
        if (x - 1 >= 0)
        {
            /*going one west*/
            var canmove = _treeMap[x - 1, y] == '-' ||
                _treeMap[x - 1, y] == 'F' ||
                _treeMap[x - 1, y] == 'L';


            var next = new Point(x - 1, y);
            var curr = new Point(x, y);
            if (canmove)
            {
                var newdistance = 1 + _distanceFromStart[curr].Item2;
                var neighbour = next;

                if (!_distanceFromStart.ContainsKey(neighbour))
                    _distanceFromStart.Add(neighbour, new Tuple<Point, int>(curr, newdistance));

                if (_distanceFromStart[neighbour].Item2 > newdistance)
                {
                    _distanceFromStart[neighbour] = new Tuple<Point, int>(curr, newdistance);
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
            var curr = new Point(x, y);

            if (canmove)
            {
                var newdistance = 1 + _distanceFromStart[curr].Item2;
                var neighbour = next;


                if (!_distanceFromStart.ContainsKey(neighbour))
                    _distanceFromStart.Add(neighbour, new Tuple<Point, int>(curr, newdistance));

                if (_distanceFromStart[neighbour].Item2 > newdistance)
                {
                    _distanceFromStart[neighbour] = new Tuple<Point, int>(curr, newdistance);
                }

            }

        }

        if (y - 1 >= 0)
        {
            /*going north*/
            var canMove = _treeMap[x, y - 1] == '|'
                || _treeMap[x, y - 1] == '7' || _treeMap[x, y - 1] == 'F';

            var next = new Point(x, y - 1);
            var curr = new Point(x, y);

            if (canMove)
            {
                var newdistance = 1 + _distanceFromStart[curr].Item2;
                var neighbour = next;

                if (!_distanceFromStart.ContainsKey(neighbour))
                    _distanceFromStart.Add(neighbour, new Tuple<Point, int>(curr, newdistance));

                if (_distanceFromStart[neighbour].Item2 > newdistance)
                {
                    _distanceFromStart[neighbour] = new Tuple<Point, int>(curr, newdistance);
                }


            }
        }


        if (y + 1 < _y)
        {
            /*going south*/
            var canmove = _treeMap[x, y + 1] == '|' || _treeMap[x, y + 1] == 'L'
                || _treeMap[x, y + 1] == 'J';

            var next = new Point(x, y + 1);
            var curr = new Point(x, y);

            if (canmove)
            {
                var newdistance = 1 + _distanceFromStart[curr].Item2;
                var neighbour = next;

                if (!_distanceFromStart.ContainsKey(neighbour))
                    _distanceFromStart.Add(neighbour, new Tuple<Point, int>(curr, newdistance));

                if (_distanceFromStart[neighbour].Item2 > newdistance)
                {
                    _distanceFromStart[neighbour] = new Tuple<Point, int>(curr, newdistance);
                }
            }
        }
    }

}

