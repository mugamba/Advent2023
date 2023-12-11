using System.Drawing;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    public static int _x;
    public static int _y;
    
   
    public static Dictionary<Point, char> _map = new Dictionary<Point, char>();
    public static Dictionary<Point, char> _mapExpanded = new Dictionary<Point, char>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        _x = lines.First().Length;
        _y = lines.Count();
   
        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                   _map.Add(new Point(i, j),  lines[j].ToCharArray()[i]);
            }

        List<int> rowsToInsert = new List<int>();
        List<int> colummsToInsert = new List<int>();


        for (int i = 0; i < _x; i++)
        {
            if (_map.Where(o=>o.Key.X == i).Select(o=>o.Value).All(c=> c=='.'))
                colummsToInsert.Add(i);
        }

        for (int i = 0; i < _y; i++)
        {
            if (_map.Where(o => o.Key.Y == i).Select(o => o.Value).All(c => c == '.'))
                rowsToInsert.Add(i);
        }

       
        var colinserted = 0;
        foreach (var col in colummsToInsert)
        {
            for (int i = 0; i < _y; i++)
            {
                var list = _map.Where(o => o.Key.X >= col + colinserted && o.Key.Y == i).ToList();
                foreach (var node in list)
                    _map.Remove(node.Key);

                _map.Add(new Point(col + colinserted, i), '.');

                foreach (var node in list)
                    _map.Add(new Point(node.Key.X + 1, node.Key.Y), node.Value);
            }
            colinserted = colinserted + 1;
        }

        var rowInserted = 0;
        foreach (var row in rowsToInsert)
        {
            for (int i = 0; i < _x + colinserted; i++)
            {
                var list = _map.Where(o => o.Key.Y >= row + rowInserted && o.Key.X == i).ToList();
                foreach (var node in list)
                    _map.Remove(node.Key);

                _map.Add(new Point(i, row + rowInserted), '.');

                foreach (var node in list)
                    _map.Add(new Point(node.Key.X, node.Key.Y + 1), node.Value);
            }

            rowInserted = rowInserted + 1;
        }

        var dict = _map.OrderBy(o => o.Key.X).ThenBy(o=>o.Key.Y).Select(o=>o);

        var x = _x + colummsToInsert.Count;
        var y = _y + rowsToInsert.Count;


        var listOfPoints = new List<Point>();
        listOfPoints.AddRange(_map.Where(o => o.Value == '#').Select(o=>o.Key).ToArray());
        var sumOfAll = 0;

        foreach (var point in listOfPoints)
        {
           var toFinddistance =  listOfPoints.Where(item => listOfPoints.IndexOf(item) > listOfPoints.IndexOf(point)).ToList();
            foreach (var point1 in toFinddistance)
            { 
                var distance = Math.Abs(point.X - point1.X) + Math.Abs(point.Y - point1.Y) ;
                sumOfAll = sumOfAll + distance;
            }
        }
    }


    public static void printmap(int x, int y)
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Console.Write(_map[new Point(j, i)]);
            }
            Console.WriteLine();
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

