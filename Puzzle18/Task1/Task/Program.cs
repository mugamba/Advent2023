using System.Drawing;
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

        var offsetX = 0 + minX;
        var offsetY = 0 + minY;


        _cubeMap = new char[x, y];

        for (int i = 0; i < x; i++)
            for (int j = 0; j < y; j++)
            {
                if (_border.ContainsKey(new Point(i + offsetX, j + offsetY)))
                        _cubeMap[i, j] = '#';
                else
                    _cubeMap[i, j] = '.';

            }

        for (int i = 0; i < x; i++)
            for (int j = 0; j < y; j++)
            {
                if (_cubeMap[i, j] == '.')
                {
                   var minnY = _border.Where(o => o.Key.X == i).Min(o=>o.Key.Y);
                   var maxxY =  _border.Where(o => o.Key.X == i).Max(o => o.Key.Y);
                    var minnX = _border.Where(o => o.Key.Y == j).Min(o => o.Key.X);
                    var maxxX = _border.Where(o => o.Key.Y == j).Max(o => o.Key.X);

                    if (minX <= i && maxX >= i && minnY <= j && maxxY >= j)
                        _inside.Add(new Point(i, j));




                }


            }
                


        //_borderEnds.Add(new Point(0, 0));
        //_borderEnds.Add(new Point(0, -3));
        //_borderEnds.Add(new Point(3, -3));
        //_borderEnds.Add(new Point(3, 0));
        



        //var sum = 0;
        //for (int i = 0; i < _borderEnds.Count; i++)
        //{
        //    Point last = new Point(0, 0);
        //    var current = _borderEnds[i]; 
        //    if (i == 0)
        //        last = _borderEnds[_borderEnds.Count - 1];
        //    else
        //        last = _borderEnds[i - 1];

        //   sum = sum + (last.X * current.Y - last.Y * current.X);
        //}

        var sum = 0;
        for (int i = 1; i < _borderEnds.Count+1; i++)
        {
            Point last = _borderEnds[i-1];
            Point current;
            if (i == _borderEnds.Count)
                current = new Point(0,0);
            else
                current = _borderEnds[i];

            var test = (current.X * last.Y - last.X * current.Y);
            sum = sum + test;
            Console.WriteLine("{0}  {1}", test, sum);
        }


        Math.Abs(sum);
        printmap(_cubeMap, x, y);

        Console.WriteLine(sum/2);
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

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Console.Write(array[j, i]);
                

            }
            Console.WriteLine();
        }

    }


   
    

}

