using System.Drawing;
using System.Text;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    public static char[,] _cubeMap;
    public static int _x;
    public static int _y;


    public static Dictionary<Point, int> _border = new Dictionary<Point, int>();
    public static List<Point> _inside = new List<Point>();
    public static List<Point> _borderEnds = new List<Point>();
    public static Dictionary<Point, int> _borderOfsset= new Dictionary<Point, int>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        String previous = null;
        Point lastaddedPoint = new Point(0, 0);
        foreach (var line in lines)
        {

            var splits = line.Split(' ');
            var current = splits[0].Trim();
            lastaddedPoint = AddPoints(current, previous, Int32.Parse(splits[1].Trim()), lastaddedPoint); 
            previous = current.ToString();
        }


        var minX = _border.Min(o => o.Key.X);
        var minY = _border.Min(o => o.Key.Y);

        var maxX = _border.Max(o => o.Key.X);
        var maxY = _border.Max(o => o.Key.Y);

        var x = maxX - minX  + 1;
        var y = maxY - minY + 1;

        var offsetX = 0 - minX;
        var offsetY = 0 - minY;


        _cubeMap = new char[x, y];

        for (int i = 0; i < x; i++)
            for (int j = 0; j < y; j++)
            {
                if (_border.ContainsKey(new Point(i - offsetX, j - offsetY)))
                        _cubeMap[i, j] = '#';
                else
                    _cubeMap[i, j] = '.';

            }


        for (int i = 0; i < x; i++)
        {

            var left = '.';
            var right = '.';
            var lastchar = '.';
            var inside = false;
            var lastbreakingCharacter = 'F';


            for (int j = 0; j < y; j++)
            {
                if (i > 0)
                    left = _cubeMap[i - 1, j];
                if (i < x-1)
                    right = _cubeMap[i + 1, j];

                var current = _cubeMap[i, j];

                if (lastchar == '.' && current == '#')
                {
                    inside = !inside;
                    if (left == '.' && right == '.')
                        lastbreakingCharacter = '-';

                    if (left == '#' && right == '.')
                        lastbreakingCharacter = '7';

                    if (left == '.' && right == '#')
                        lastbreakingCharacter = 'F';

                    if (left == '#' && right == '#')
                        lastbreakingCharacter = '-';

                }

                if (lastchar == '#' && current == '#' && lastbreakingCharacter == 'F')
                {
                    if (left == '.' && right == '#')
                    { 
                        inside = !inside;

                    }
                }

                if (lastchar == '#' && current == '#' && lastbreakingCharacter == '7')
                {
                    if (left == '#' && right == '.')
                    {
                        inside = !inside;

                    }
                }

                if (inside && _cubeMap[i, j] == '.')
                { 
                    _inside.Add(new Point(i, j));

                }
                lastchar = current;

            }

        }


        foreach (var i in _inside)
            _cubeMap[i.X, i.Y] = '#';

                printmap(_cubeMap, x, y);

        Console.WriteLine(_inside.Count + _border.Count);
        Console.ReadKey();
    }


    public static Point AddPoints(String current, String previous, int points, Point lastaddedPoint)
    {
        Point firstToAdd = new Point(0,0);
        Point lastToAdd = new Point(0, 0);

        if (previous == null && lastaddedPoint == new Point(0, 0))
            firstToAdd = new Point(0, 0);
        else
        {
            if (previous == "R")
                firstToAdd = new Point(lastaddedPoint.X + 1, lastaddedPoint.Y);
            if (previous == "L")
                firstToAdd = new Point(lastaddedPoint.X - 1, lastaddedPoint.Y);
            if (previous == "U")
                firstToAdd = new Point(lastaddedPoint.X, lastaddedPoint.Y - 1);
            if (previous == "D")
                firstToAdd = new Point(lastaddedPoint.X, lastaddedPoint.Y + 1);
        }

        _borderEnds.Add(firstToAdd);

            
            if (current == "R")
            {
                for (int i = 0; i < points; i++)
                {

                    if (i == points - 1)
                        lastToAdd = new Point(firstToAdd.X + i, firstToAdd.Y);

                    _border.Add(new Point(firstToAdd.X + i, firstToAdd.Y), 1);
                }
            }
        if (current == "L")
        {
            for (int i = 0; i < points; i++)
            {

                if (i == points - 1)
                    lastToAdd = new Point(firstToAdd.X - i, firstToAdd.Y);

                _border.Add(new Point(firstToAdd.X - i, firstToAdd.Y), 1);
            }
        }
        if (current == "U")
        {
            for (int i = 0; i < points; i++)
            {

                if (i == points - 1)
                    lastToAdd = new Point(firstToAdd.X, firstToAdd.Y-i);

                _border.Add(new Point(firstToAdd.X, firstToAdd.Y-i), 1);
            }
        }
        if (current == "D")
        {
            for (int i = 0; i < points; i++)
            {

                if (i == points - 1)
                    lastToAdd = new Point(firstToAdd.X, firstToAdd.Y+i);

                _border.Add(new Point(firstToAdd.X, firstToAdd.Y+i), 1);
            }
        }

        return lastToAdd;
    }

    public static void printmap(char[,] array, int x, int y)
    {

        var builder = new StringBuilder();
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
               builder.Append(array[j,i]);
                

            }
            builder.AppendLine();
        }
        File.WriteAllText(@"C:\Temp\test.txt", builder.ToString());

    }


   
    

}

