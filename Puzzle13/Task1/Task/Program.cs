using System.Drawing;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    public static int _x;
    public static int _y;
    
   
    public static Dictionary<Point, char> _map = new Dictionary<Point, char>();
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

            _x = lines[startindex].Length;
            _y = delimiter - 1 - startindex;
            _map.Clear();

            for (int i = 0; i < _x; i++)
            {
                for (int j = startindex; j < delimiter - 1; j++)
                {
                    _map.Add(new Point(i, j - startindex), lines[j].ToCharArray()[i]);
                }
            }


            printmap(_x, _y);

            Console.WriteLine();
            Console.WriteLine();
            startindex = delimiter; 

        }


        Console.ReadLine();
       

       
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


    public class

   
}

