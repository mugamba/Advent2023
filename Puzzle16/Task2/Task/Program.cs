using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    public static int _x;
    public static int _y;
    public static Dictionary<Point, char> _map = new Dictionary<Point, char>();
    public static Dictionary<Point, Boolean> _isEnergized = new Dictionary<Point, Boolean>();
    public static Dictionary<Tuple<Point, char>, int> _memo = new Dictionary<Tuple<Point, char>, int>();
    public static List<int> allResults = new List<int>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        _x = lines.First().Length;
        _y = lines.Length;
        var count = 0;

        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                _map.Add(new Point(i, j), lines[j].ToCharArray()[i]);
                
            }



        var most = 0;
        for (int i = 0; i < _x; i++)
        {

            _isEnergized.Clear();
            _memo.Clear();

            var temp = new List<Tuple<char, Point>>();
            temp.Add(new Tuple<char, Point>('v', new Point(i, 0)));
            while (temp.Count > 0)
            {

                var list = temp.ToList();
                temp.Clear();

                foreach (var item in list)
                    temp.AddRange(DoNextPoint(item.Item1, item.Item2));



            }
            allResults.Add(_isEnergized.Count);

        }


        for (int i = 0; i < _x; i++)
        {

            _isEnergized.Clear();
            _memo.Clear();

            var temp = new List<Tuple<char, Point>>();
            temp.Add(new Tuple<char, Point>('^', new Point(i, _y-1)));
            while (temp.Count > 0)
            {

                var list = temp.ToList();
                temp.Clear();

                foreach (var item in list)
                    temp.AddRange(DoNextPoint(item.Item1, item.Item2));



            }

            allResults.Add(_isEnergized.Count);
                

        }
        for (int j = 0; j < _y; j++)
        {
            _isEnergized.Clear();
            _memo.Clear();


            var temp = new List<Tuple<char, Point>>();
            temp.Add(new Tuple<char, Point>('>', new Point(0, j)));
            while (temp.Count > 0)
            {

                var list = temp.ToList();
                temp.Clear();

                foreach (var item in list)
                    temp.AddRange(DoNextPoint(item.Item1, item.Item2));



            }

            allResults.Add(_isEnergized.Count);

        }
        for (int j = 0; j < _y; j++)
        {
            _isEnergized.Clear();
            _memo.Clear();

            var temp = new List<Tuple<char, Point>>();
            temp.Add(new Tuple<char, Point>('<', new Point(_x-1, j)));
            while (temp.Count > 0)
            {

                var list = temp.ToList();
                temp.Clear();

                foreach (var item in list)
                    temp.AddRange(DoNextPoint(item.Item1, item.Item2));



            }

            allResults.Add(_isEnergized.Count);
        }



        Console.WriteLine(allResults.Max());
            Console.ReadLine();


    }


    public static void printmap(Dictionary<Point, char> dict,  int x, int y)
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Console.Write(dict[new Point(j, i)]);
            }
            Console.WriteLine();
        }

    }

    public static IList<Tuple<char, Point>> DoNextPoint(char direction, Point nextPoint)
    {
        var list = new List<Tuple<char, Point>>();

        
        var left  = nextPoint.X < 0;
        var right = nextPoint.X >= _x;
        var up = nextPoint.Y < 0;
        var down = nextPoint.Y >= _y;

        if (left || right  || up || down)
            return list;


        var nextSign = _map[nextPoint];
        var newMemo = new Tuple<Point, char>(nextPoint, direction);

        if (_memo.ContainsKey(newMemo))
            return list;

        _memo.Add(newMemo, 0);


        if (!_isEnergized.ContainsKey(nextPoint))
        {
            _isEnergized.Add(nextPoint, true);
        }
        /*energize*/
        if (nextSign == '.')
        {
           
            if (direction == '>')
                list.Add(new Tuple<char, Point>(direction, new Point(nextPoint.X + 1, nextPoint.Y)));
            if (direction == '<')
                list.Add(new Tuple<char, Point>(direction, new Point(nextPoint.X - 1, nextPoint.Y)));
            if (direction == 'v')
                list.Add(new Tuple<char, Point>(direction, new Point(nextPoint.X, nextPoint.Y+1)));
            if (direction == '^')
                list.Add(new Tuple<char, Point>(direction, new Point(nextPoint.X, nextPoint.Y-1)));

        }

        if (nextSign == '|' && (direction == '>' || direction == '<'))
        {
            list.Add(new Tuple<char, Point>('^', new Point(nextPoint.X, nextPoint.Y - 1)));
            list.Add(new Tuple<char, Point>('v', new Point(nextPoint.X, nextPoint.Y + 1)));
        }
        if (nextSign == '|' && direction == '^')
        {
            list.Add(new Tuple<char, Point>('^', new Point(nextPoint.X, nextPoint.Y - 1)));           
        }
        if (nextSign == '|' && direction == 'v')
        {
            list.Add(new Tuple<char, Point>('v', new Point(nextPoint.X, nextPoint.Y + 1)));
        }


        if (nextSign == '-' && (direction == '^' || direction == 'v'))
        {
            list.Add(new Tuple<char, Point>('<', new Point(nextPoint.X-1, nextPoint.Y)));
            list.Add(new Tuple<char, Point>('>', new Point(nextPoint.X+1, nextPoint.Y)));
        }
        if (nextSign == '-' && direction == '<')
        {
            list.Add(new Tuple<char, Point>('<', new Point(nextPoint.X - 1, nextPoint.Y)));
        }
        if (nextSign == '-' && direction == '>')
        {
            list.Add(new Tuple<char, Point>('>', new Point(nextPoint.X + 1, nextPoint.Y)));
        }


        if (nextSign == '\\' && direction == '>')
        {
            list.Add(new Tuple<char, Point>('v', new Point(nextPoint.X, nextPoint.Y+1)));
        }
        if (nextSign == '\\' && direction == '<')
        {
            list.Add(new Tuple<char, Point>('^', new Point(nextPoint.X, nextPoint.Y - 1)));
        }
        if (nextSign == '\\' && direction == '^')
        {
            list.Add(new Tuple<char, Point>('<', new Point(nextPoint.X-1, nextPoint.Y)));
        }
        if (nextSign == '\\' && direction == 'v')
        {
            list.Add(new Tuple<char, Point>('>', new Point(nextPoint.X+1, nextPoint.Y)));
        }

        if (nextSign == '/' && direction == '>')
        {
            list.Add(new Tuple<char, Point>('^', new Point(nextPoint.X, nextPoint.Y - 1)));
        }
        if (nextSign == '/' && direction == '<')
        {
            list.Add(new Tuple<char, Point>('v', new Point(nextPoint.X, nextPoint.Y + 1)));
        }
        if (nextSign == '/' && direction == '^')
        {
            list.Add(new Tuple<char, Point>('>', new Point(nextPoint.X + 1, nextPoint.Y)));
        }
        if (nextSign == '/' && direction == 'v')
        {
            list.Add(new Tuple<char, Point>('<', new Point(nextPoint.X - 1, nextPoint.Y)));
        }

      

        return list;

    }


}

