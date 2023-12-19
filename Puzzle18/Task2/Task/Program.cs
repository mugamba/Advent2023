using System.Drawing;
using System.Text;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    
    public static Dictionary<Tuple<long, long>, long> _points= new Dictionary<Tuple<long, long>, long>();

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        Tuple<long, long> lastaddedPoint = new Tuple<long, long>(0, 0);

        IList<Tuple<string, long>> input = new List<Tuple<string, long>>();
        foreach (var line in lines)
        {

            var splits = line.Split(' ');
            var split3 = splits[2].Trim().Replace("(", "").Replace(")", "");

           var heksa = split3.Substring(0, 6).Replace("#", "").ToUpper(); 

           var direction  = split3.Substring(6, 1);
           var num = long.Parse(heksa, System.Globalization.NumberStyles.HexNumber);
            lastaddedPoint = AddPoints(lastaddedPoint, GetDirection(direction), num); 
        }


        //Sholace  
        long area = 0;
        for (var i = 0; i < _points.Count; i++)
        {
            Tuple<long, long> current = _points.ElementAt(i).Key;
            Tuple<long, long> previous = new Tuple<long, long>(0, 0);

            if (i == 0)
                previous = _points.ElementAt(_points.Count - 1).Key;
            else
                previous = _points.ElementAt(i - 1).Key;


            area = area + (current.Item1*previous.Item2-previous.Item1*current.Item2);

        }

        var borrderDotsCount = _points.Select(o => o.Value).Sum();
            

        area = Math.Abs(area) / 2;

        var insideDotsCount = area - borrderDotsCount / 2 + 1;


        Console.WriteLine(insideDotsCount + borrderDotsCount);
        Console.ReadKey();
    }

    public static String GetDirection(String input)
    {
        if (input == "0")
            return "R";
        if (input == "1")
            return "D";
        if (input == "2")
            return "L";
        if (input == "3")
            return "U";

        return null;

    }

    public static Tuple<long, long> AddPoints(Tuple<long, long> lastAdedd, String direction, long distance)
    {
   
        var newPoint = new Tuple<long, long>(0, 0);

            if (direction == "R")
            newPoint = new Tuple<long, long>(lastAdedd.Item1 + distance, lastAdedd.Item2);
            if (direction == "L")
            newPoint = new Tuple<long, long>(lastAdedd.Item1 - distance, lastAdedd.Item2);
            if (direction == "U")
            newPoint = new Tuple<long, long>(lastAdedd.Item1, lastAdedd.Item2 - distance);
            if (direction == "D")
            newPoint = new Tuple<long, long>(lastAdedd.Item1, lastAdedd.Item2 + distance);
        

        _points.Add(newPoint, distance);

   
        return newPoint;
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

