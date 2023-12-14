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
                var c = lines[j].ToCharArray()[i];
                    if (c != '.')
                        dict.Add(new Point(i, j), lines[j].ToCharArray()[i]);
                }
            }

            var tile = new Tile(dict, x, y);

        //printmap(tile._map, x, y);
        //Console.WriteLine("-------");
        //Console.WriteLine("-------");
        //printmap(tile._map, x, y);
    
        //Console.WriteLine(tile.CalculateWeight());
        //Console.ReadLine();

        for (int i = 0; i < 1000000000; i++)
        {
            tile.DoNorth();
        }
       
    }


    public static void printmap(Dictionary<Point, char> dict,  int x, int y)
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (dict.ContainsKey(new Point(j, i)))
                    Console.Write(dict[new Point(j, i)]);
                else
                    Console.Write('.');
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



        public void DoNorth()
        {


            //var temp = 0; 
            for (int i = 0; i < _x; i++)
            {
                var t = _map.Where(o => o.Key.X == i).ToList();
            }

                    


        
        
        }



    }





}

