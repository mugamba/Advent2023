﻿using System.Collections.Generic;
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

            //printmap(tile._map,  x, y);

            _tiles.Add(tile);

         
            //Console.WriteLine();
            startindex = delimiter; 

        }
        var to = _tiles.Where(o => o.CalculateWeight() == 0).FirstOrDefault();


        var sum = 0;
        foreach (var t in _tiles)
        {
            var tt = t.CalculateWeight();
            Console.WriteLine(tt);
            Console.WriteLine("");
            sum += tt;

        }

        Console.WriteLine(sum);
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


        public Int32 CalculateWeight()
        {  
            return CheckRowReflection() + CheckColumnReflection();

        }



        public Int32 CheckRowReflection()
        {
            var tuples = MatchingNeighboursRows();

            var result = 0;
            foreach (var tuple in tuples)
            {
                if (tuple.Item1 != -1)
                {
                    var distance = tuple.Item1 > _y - tuple.Item2 - 1 ? _y - tuple.Item2 - 1 : tuple.Item1;
                    var toContinue = false;
                    for (int i = 1; i < distance + 1; i++)
                    {
                        if (!IsMatchingRow(tuple.Item1 - i, tuple.Item2 + i))
                        {
                            toContinue = true;
                            break;
                        }
                    }
                    
                    if (toContinue)
                        continue;

                    result = (tuple.Item1 + 1) * 100;
                    
                }
                else
                    continue;
            }

            return result;




        }

        public Int32 CheckColumnReflection()
        {
            var tuples = MatchingNeighboursColumns();
            var result = 0;
            foreach (var tuple in tuples)
            {
                if (tuple.Item1 != -1)
                {
                    var distance = tuple.Item1 > _x - tuple.Item2 - 1 ? _x - tuple.Item2 - 1 : tuple.Item1;
                    var toContinue = false;
                    for (int i = 1; i < distance + 1; i++)
                    {
                        if (!IsMatchingColumn(tuple.Item1 - i, tuple.Item2 + i))
                        {
                            toContinue = true;
                            break;
                        }
                    }
                    if (toContinue)
                        continue;

                    result = tuple.Item1 + 1;


                }
                else
                    continue;
            }

            return result;



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

        public List<Tuple<int, int>> MatchingNeighboursColumns()
        {
            var list = new List<Tuple<int, int>>();
            for (int i = 0; i < _x; i++)
            {
                if (i > 0)
                {
                    if (IsMatchingColumn(i - 1, i))
                        list.Add(new Tuple<int, int>(i-1, i));
                }

            }

            return list;
        
        }

        public List<Tuple<int, int>> MatchingNeighboursRows()
        {
            var list = new List<Tuple<int, int>>();
            for (int i = 0; i < _y; i++)
            {
                if (i > 0)
                {
                    if (IsMatchingRow(i - 1, i))
                        list.Add(new Tuple<int, int>(i - 1, i));
                }

            }

            return list;

        }




    }




   
}

