using System.Drawing;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    
   
    public static List<int> nextStartIndex = new List<int>();



    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        var count = 0;
        
        for (int i = 0; i < lines.Length; i++)
            if (lines[i] == String.Empty)
                nextStartIndex.Add(i + 1);

        nextStartIndex.Add(lines.Length + 1);


        var startindex = 0;
        foreach (var delimiter in nextStartIndex)
        {

            var x = lines[startindex].Length;
            var y = delimiter - 1 - startindex;
            var map = new Dictionary<Point, char>();

            for (int i = 0; i < x; i++)
            {
                for (int j = startindex; j < delimiter - 1; j++)
                {
                    map.Add(new Point(i, j - startindex), lines[j].ToCharArray()[i]);
                }
            }

            var tile = new Tile(map, x, y);

            //printmap(_x, _y);

            Console.WriteLine("Matching columns = {0}   {1}", tile.MatchingNeighboursColumns().Item1, 
                tile.MatchingNeighboursColumns().Item2) ;
            Console.WriteLine("Matching rows = {0}   {1}", tile.MatchingNeighboursRows().Item1,
              tile.MatchingNeighboursRows().Item2);
            //Console.WriteLine();
            startindex = delimiter; 

        }


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



    public class Tile
    {
        public int _x;
        public int _y;


        public Dictionary<Point, char> _map = new Dictionary<Point, char>();

        public Tile(Dictionary<Point, char> dict, int x, int y)
        {
            _map = dict;
            _x = x;
            _y = y;
        }


        public Boolean CheckRowReflection()
        { 
        
            
        
        }

        public Boolean CheckColumnReflection()
        {



        }


        public Boolean IsMatchingColumn(int x1, int x2)
        {
            var target1 = _map.Where(o => o.Key.X == x1).Select(o => o.Value);
            var target2 = _map.Where(o => o.Key.X == x2).Select(o => o.Value);

            return Enumerable.SequenceEqual(target1, target2);
        }

        public Boolean IsMatchingRow(int y1, int y2)
        {
            var target1 = _map.Where(o => o.Key.Y == y1).Select(o => o.Value);
            var target2 = _map.Where(o => o.Key.Y == y2).Select(o => o.Value);

            return Enumerable.SequenceEqual(target1, target2);
        }

        public Tuple<int, int> MatchingNeighboursColumns()
        {

            for (int i = 0; i < _x; i++)
            {
                if (i > 0)
                {
                    if (IsMatchingColumn(i - 1, i))
                        return new Tuple<int, int>(i-1, i);
                }

            }

            return new Tuple<int, int>(-1, -1);
        
        }

        public Tuple<int, int> MatchingNeighboursRows()
        {

            for (int i = 0; i < _y; i++)
            {
                if (i > 0)
                {
                    if (IsMatchingRow(i - 1, i))
                        return new Tuple<int, int>(i - 1, i);
                }

            }

            return new Tuple<int, int>(-1, -1);

        }




    }




   
}

