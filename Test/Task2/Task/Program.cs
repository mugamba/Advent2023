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
        var t = (1000000000 - 480) % 26;



      //  Console.WriteLine(tile.CalculateWeight());
        Console.ReadLine();

        //for (int i = 0; i < 1000000000; i++)
        //{
        //    tile.DoNorth();
        //}

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

        public Dictionary<int, List<int>> _northSouthRanges = new Dictionary<int, List<int>>();
        public Dictionary<int, List<int>> _westEastRanges = new Dictionary<int, List<int>>();


        public Tile(Dictionary<Point, char> dict, int x, int y)
        {
            _map = dict;
            _x = x;
            _y = y;

            _northSouthRanges = GetNorthSouthRanges();
            _westEastRanges = GetWestEastRanges();


        }

        private Dictionary<int, List<int>> GetNorthSouthRanges()
        {
            var dict = new Dictionary<int, List<int>>();
            for (int i = 0; i < _x; i++)
            {
                var list = new List<int>();
                list.Add(-1);
                list.AddRange(_map.Where(o => o.Key.X == i && o.Value == '#').Select(o => o.Key.Y));
                list.Add(_x);
                dict.Add(i, list);
            }
            return dict;
        }
        private Dictionary<int, List<int>> GetWestEastRanges()
        {
            var dict = new Dictionary<int, List<int>>();
            for (int i = 0; i < _y; i++)
            {
                var list = new List<int>();
                list.Add(-1);
                list.AddRange(_map.Where(o => o.Key.Y == i && o.Value == '#').Select(o => o.Key.X));
                list.Add(_y);
                dict.Add(i, list);
            }
            return dict;
        }



        public void DoNorth()
        {
            for (int i = 0; i < _x; i++)
                MoveColumnNorth(i);
        }
        public void DoSouth()
        {
            for (int i = 0; i < _x; i++)
                MoveColumnSouth(i);
        }
        public void DoWest()
        {
            for (int i = 0; i < _y; i++)
                MoveColumnWest(i);
        }
        public void DoEast()
        {
            for (int i = 0; i < _y; i++)
                MoveColumnEast(i);
        }


        private void MoveColumnNorth(int i)
        {
            for (int j = 1; j < _northSouthRanges[i].Count; j++)
            {
                var min = _northSouthRanges[i][j - 1];
                var max = _northSouthRanges[i][j];
                var toRemove = _map.Where(o => o.Key.Y >= min && o.Key.Y <= max && o.Value == 'O' && o.Key.X == i).ToList();
                foreach (var item in toRemove)
                {
                    _map.Remove(item.Key);
                }
                var counter = 0;
                foreach (var item in toRemove)
                {
                    _map.Add(new Point(item.Key.X, min + 1 + counter), 'O');
                    counter++;
                }
            }
        }
        private void MoveColumnSouth(int i)
        {
            for (int j = _northSouthRanges[i].Count-1; j > 0; j--)
            {
                var min = _northSouthRanges[i][j - 1];
                var max = _northSouthRanges[i][j];
                var toRemove = _map.Where(o => o.Key.Y >= min && o.Key.Y <= max && o.Value == 'O' && o.Key.X == i).ToList();
               
                foreach (var item in toRemove)
                {
                    _map.Remove(item.Key);
                }
                var counter = 0;
                foreach (var item in toRemove)
                {
                    _map.Add(new Point(item.Key.X, max - 1 - counter), 'O');
                    counter++;
                }
            }
        }





        private void MoveColumnWest(int i)
        { 
            for (int j = 1; j < _westEastRanges[i].Count; j++)
            {
                var min = _westEastRanges[i][j - 1];
                var max = _westEastRanges[i][j];
                var toRemove = _map.Where(o => o.Key.X >= min && o.Key.X <= max && o.Value == 'O' && o.Key.Y == i).ToList();
                foreach (var item in toRemove)
                {
                    _map.Remove(item.Key);
                }
                var counter = 0;
                foreach (var item in toRemove)
                {
                    _map.Add(new Point(min + 1 + counter, item.Key.Y), 'O');
                    counter++;
                }
            }
        }

        private void MoveColumnEast(int i)
        {
            for (int j = _westEastRanges[i].Count - 1; j > 0; j--)
            {
                var min = _westEastRanges[i][j - 1];
                var max = _westEastRanges[i][j];
                var toRemove = _map.Where(o => o.Key.X >= min && o.Key.X <= max && o.Value == 'O' && o.Key.Y == i).ToList();

                foreach (var item in toRemove)
                {
                    _map.Remove(item.Key);
                }
                var counter = 0;
                foreach (var item in toRemove)
                {
                    _map.Add(new Point(max - 1 - counter, item.Key.Y), 'O');
                    counter++;
                }
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

