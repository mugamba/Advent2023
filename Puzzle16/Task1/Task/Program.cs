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
    public static Dictionary<Point, List<char>> _mapEnergy = new Dictionary<Point, List<char>>();


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
                _mapEnergy.Add(new Point(i, j), new List<char>());
            }

        DoNextPoint('>', new Point(0, 0));

        var temp = _mapEnergy.Where(o => o.Value.Count() > 0).Count();

        Console.WriteLine(temp);
        Console.ReadLine();


        var most = 0;
        for (int i = 0; i < _x; i++)
        {
            foreach (var v in _mapEnergy)
                v.Value.Clear();

            DoNextPoint('>', new Point(0, 0));

            var temp = _mapEnergy.Where(o => o.Value.Count() > 0).Count();
            if (temp > most) {
            
                most = temp;
            }
           
        }


        Console.WriteLine(most);
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

    public static void DoNextPoint(char direction, Point nextPoint)
    {
        var left  = nextPoint.X < 0;
        var right = nextPoint.X >= _x;
        var up = nextPoint.Y < 0;
        var down = nextPoint.Y >= _y;


        if (left || right  || up || down)
            return;


        var nextSign = _map[nextPoint];

        if (_mapEnergy[nextPoint].Contains(direction))
            return;


        _mapEnergy[nextPoint].Add(direction);



        /*energize*/
        if (nextSign == '.')
        {
          
            if (direction == '>')
                DoNextPoint(direction, new Point(nextPoint.X + 1, nextPoint.Y));
            if (direction == '<')
                DoNextPoint(direction, new Point(nextPoint.X - 1, nextPoint.Y));
            if (direction == 'v')
                DoNextPoint(direction, new Point(nextPoint.X, nextPoint.Y+1));
            if (direction == '^')
                DoNextPoint(direction, new Point(nextPoint.X, nextPoint.Y-1));

        }
       



        if (nextSign == '|' && (direction == '>' || direction == '<'))
        {
            DoNextPoint('^', new Point(nextPoint.X, nextPoint.Y - 1));
            DoNextPoint('v', new Point(nextPoint.X, nextPoint.Y + 1));
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

    }


}

