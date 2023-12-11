using System.Drawing;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    public static int _x;
    public static int _y;
    
   
    public static Dictionary<Tuple<long, long>, char> _map = new Dictionary<Tuple<long,long>, char>();
    
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        _x = lines.First().Length;
        _y = lines.Count();
   
        for (int i = 0; i < _x; i++)
            for (int j = 0; j < _y; j++)
            {
                   _map.Add(new Tuple<long, long>(i, j),  lines[j].ToCharArray()[i]);
            }

        List<int> rowsToInsert = new List<int>();
        List<int> colummsToInsert = new List<int>();


        for (int i = 0; i < _x; i++)
        {
            if (_map.Where(o=>o.Key.Item1 == i).Select(o=>o.Value).All(c=> c=='.'))
                colummsToInsert.Add(i);
        }

        for (int i = 0; i < _y; i++)
        {
            if (_map.Where(o => o.Key.Item2 == i).Select(o => o.Value).All(c => c == '.'))
                rowsToInsert.Add(i);
        }

        var offset = 1000000 - 1;



        var colinserted = 0;
        foreach (var col in colummsToInsert)
        {
            for (int i = 0; i < _y; i++)
            {
                var list = _map.Where(o => o.Key.Item1 >= col + colinserted && o.Key.Item2 == i).ToList();

                foreach (var node in list)
                {
                    _map.Remove(node.Key);
                    _map.Add(new Tuple<long, long>(node.Key.Item1 + offset, node.Key.Item2), node.Value);

                }
            }
            colinserted = colinserted + offset;
        }

        var rowInserted = 0;
        foreach (var row in rowsToInsert)
        {
            for (int k = 0; k < (colinserted / offset) + 1; k++)
            {
                for (int i = 0; i < _x; i++)
                {
                    var list = _map.Where(o => o.Key.Item2 >= row + rowInserted && o.Key.Item1 == k * offset + i).ToList();
                    foreach (var node in list)
                    {
                        _map.Remove(node.Key);
                        _map.Add(new Tuple<long, long>(node.Key.Item1, node.Key.Item2 + offset), node.Value);

                    }
                }
            }
            rowInserted = rowInserted + offset;
        }

      
        var x = _x + colummsToInsert.Count;
        var y = _y + rowsToInsert.Count;


        var listOfPoints = new List<Tuple<long, long>>();
        listOfPoints.AddRange(_map.Where(o => o.Value == '#').Select(o=>o.Key).ToArray());
        long sumOfAll = 0;

        foreach (var point in listOfPoints)
        {
           var toFinddistance =  listOfPoints.Where(item => listOfPoints.IndexOf(item) > listOfPoints.IndexOf(point)).ToList();
            foreach (var point1 in toFinddistance)
            { 
                var distance = Math.Abs(point.Item1 - point1.Item1) + Math.Abs(point.Item2 - point1.Item2) ;
                sumOfAll = sumOfAll + distance;
            }
        }

        Console.WriteLine(sumOfAll);
        Console.ReadKey();
    }


    //public static void printmap(int x, int y)
    //{
    //    for (int i = 0; i < y; i++)
    //    {
    //        for (int j = 0; j < x; j++)
    //        {
    //            Console.Write(_map[new Point(j, i)]);
    //        }
    //        Console.WriteLine();
    //    }



    //}

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

