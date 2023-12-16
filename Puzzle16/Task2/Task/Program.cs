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
                _isEnergized.Add(new Point(i, j), false);
                
            }


        DoNextPoint('>', new Point(0, 0));


        var most = 0;
        //for (int i = 0; i < _x; i++)
        //{
        //    foreach (var v in _mapEnergy)
        //        v.Value.Clear();

        //    DoNextPoint('v',  new Point(i, 0));

        //    var temp = _mapEnergy.Where(o => o.Value.Count() > 0).Count();
        //    if (temp > most) {
            
        //        most = temp;
        //    }
           
        //}
        //for (int i = 0; i < _x; i++)
        //{
        //    foreach (var v in _mapEnergy)
        //        v.Value.Clear();

        //    DoNextPoint('^', new Point(i, _y));

        //    var temp = _mapEnergy.Where(o => o.Value.Count() > 0).Count();
        //    if (temp > most)
        //    {

        //        most = temp;
        //    }

        //}
        //for (int j = 0; j < _y; j++)
        //{
        //    foreach (var v in _mapEnergy)
        //        v.Value.Clear();

        //    DoNextPoint('>', new Point(0, j));

        //    var temp = _mapEnergy.Where(o => o.Value.Count() > 0).Count();
        //    if (temp > most)
        //    {

        //        most = temp;
        //    }

        //}
        //for (int j = 0; j < _y; j++)
        //{
        //    foreach (var v in _mapEnergy)
        //        v.Value.Clear();

        //    DoNextPoint('<', new Point(_x, j));

        //    var temp = _mapEnergy.Where(o => o.Value.Count() > 0).Count();
        //    if (temp > most)
        //    {

        //        most = temp;
        //    }

        //}



        //Console.WriteLine(most);
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

    public static Int32 DoNextPoint(char direction, Point nextPoint, int value)
    {
        var left  = nextPoint.X < 0;
        var right = nextPoint.X >= _x;
        var up = nextPoint.Y < 0;
        var down = nextPoint.Y >= _y;

        if (left || right  || up || down)
            return value;


        var nextSign = _map[nextPoint];
        var newMemo = new Tuple<Point, char>(nextPoint, direction);

        if (_memo.ContainsKey(newMemo))
            return value + _memo[newMemo];


        if (_isEnergized[nextPoint])
        {
            _isEnergized[nextPoint] = true;
            value = value + 1;
        }
        /*energize*/
        if (nextSign == '.')
        {
           
            if (direction == '>')
              value = DoNextPoint(direction, new Point(nextPoint.X + 1, nextPoint.Y), value);
            if (direction == '<')
              value = DoNextPoint(direction, new Point(nextPoint.X - 1, nextPoint.Y), value);
            if (direction == 'v')
              value = DoNextPoint(direction, new Point(nextPoint.X, nextPoint.Y+1), value);
            if (direction == '^')
              value = DoNextPoint(direction, new Point(nextPoint.X, nextPoint.Y-1), value);

        }

        if (nextSign == '|' && (direction == '>' || direction == '<'))
        {
            value = DoNextPoint('^', new Point(nextPoint.X, nextPoint.Y - 1)) +  DoNextPoint('v', new Point(nextPoint.X, nextPoint.Y + 1));
        }
        if (nextSign == '|' && direction == '^')
        {
            DoNextPoint('^', new Point(nextPoint.X, nextPoint.Y - 1));           
        }
        if (nextSign == '|' && direction == 'v')
        {
            DoNextPoint('v', new Point(nextPoint.X, nextPoint.Y + 1));
        }


        if (nextSign == '-' && (direction == '^' || direction == 'v'))
        {
            DoNextPoint('<', new Point(nextPoint.X-1, nextPoint.Y));
            DoNextPoint('>', new Point(nextPoint.X+1, nextPoint.Y));
        }
        if (nextSign == '-' && direction == '<')
        {
            DoNextPoint('<', new Point(nextPoint.X - 1, nextPoint.Y));
        }
        if (nextSign == '-' && direction == '>')
        {
            DoNextPoint('>', new Point(nextPoint.X + 1, nextPoint.Y));
        }


        if (nextSign == '\\' && direction == '>')
        {
            DoNextPoint('v', new Point(nextPoint.X, nextPoint.Y+1));
        }
        if (nextSign == '\\' && direction == '<')
        {
            DoNextPoint('^', new Point(nextPoint.X, nextPoint.Y - 1));
        }
        if (nextSign == '\\' && direction == '^')
        {
            DoNextPoint('<', new Point(nextPoint.X-1, nextPoint.Y));
        }
        if (nextSign == '\\' && direction == 'v')
        {
            DoNextPoint('>', new Point(nextPoint.X+1, nextPoint.Y));
        }

        if (nextSign == '/' && direction == '>')
        {
            DoNextPoint('^', new Point(nextPoint.X, nextPoint.Y - 1));
        }
        if (nextSign == '/' && direction == '<')
        {
            DoNextPoint('v', new Point(nextPoint.X, nextPoint.Y + 1));
        }
        if (nextSign == '/' && direction == '^')
        {
            DoNextPoint('>', new Point(nextPoint.X + 1, nextPoint.Y));
        }
        if (nextSign == '/' && direction == 'v')
        {
            DoNextPoint('<', new Point(nextPoint.X - 1, nextPoint.Y));
        }

        if (_memo.ContainsKey(newMemo))
        _memo.Add(newMemo, value);

        return value;

    }


}

