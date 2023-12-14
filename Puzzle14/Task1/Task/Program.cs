using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    
   
    public static List<int> nextStartIndex = new List<int>();

    public static List<Tile> _tiles = new List<Tile>();


    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        var x = lines.First().Length;
        var y = lines.Count();
        
        

        var dict = new Dictionary<Point, char>();

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    dict.Add(new Point(i, j), lines[j].ToCharArray()[i]);
                }
            }

            var tile = new Tile(dict, x, y);

        printmap(tile._map, x, y);
        tile.MoveAllRowsUp();
        Console.WriteLine("-------");
        Console.WriteLine("-------");
        printmap(tile._map, x, y);
        Console.WriteLine(tile.CalculateWeight());
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


        public IList<Point> MoveRocksUpToTop(int rowIndex)
        {
            var list = new List<Point>();
            var rowAbove = _map.Where(o => o.Key.Y == rowIndex-1).ToArray();
            var row = _map.Where(o => o.Key.Y == rowIndex).ToArray();


            for (int i = 0; i < _x; i++)
            {
                if (row[i].Value == 'O' && rowAbove[i].Value == '.')
                {
                    _map[rowAbove[i].Key] = 'O';
                    _map[row[i].Key] = '.';

                    list.Add(rowAbove[i].Key);
                }
            }

            return list;

        }
        public Boolean CanMoveUp(int rowIndex)
        {
            var rowAbove = _map.Where(o => o.Key.Y == rowIndex - 1).ToArray();
            var row = _map.Where(o => o.Key.Y == rowIndex).ToArray();

            for (int i = 0; i < _x; i++)
            {
                if (row[i].Value == 'O' && rowAbove[i].Value == '.')
                    return true;
            }

            return false;
        }


        public void MoveAllRowsUp()
        {
            while (true)
            {
             
                var list = new List<Point>();
                for (int j = 1; j < _y; j++)
                {
                   list.AddRange(MoveRocksUpToTop(j));
                }

                if (list.Count == 0)
                    break;

            }

        }

        public Int32 CalculateWeight()
        {
            var sum = 0;
            for (int i = 0; i < _y; i++)
            {

                sum += _map.Where(o => o.Value == 'O' && o.Key.Y == i).Count() * (_y - i);

            }
            return sum;

        }




    }




   
}

