using System;
using System.Linq;
using System.Text;

var lines = File.ReadAllLines("input.txt");
var dictionary = new Dictionary<Tuple<int, int>, char>();

char[] numbers = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
var listOfNumber = new List<Number>();

Number temp = null;

for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i].ToCharArray();
    var booleanNumberStarts = false;
    if (temp != null)
    {
        var newNumber = temp.NewClonedNumber();
        booleanNumberStarts = false;
        listOfNumber.Add(newNumber);
        temp = null;

    }


    for (int j = 0; j < line.Length; j++)
    {
        var sign = line[j];

        if (!numbers.Contains(sign) && booleanNumberStarts == true)
        { 
                var newNumber =  temp.NewClonedNumber();
                booleanNumberStarts = false;
                listOfNumber.Add(newNumber);
                temp = null;
        }
        if (numbers.Contains(sign) && booleanNumberStarts == true)
        {
            temp.NumberCoordinates.Add(new Tuple<int, int>(i, j));
        }

        if (numbers.Contains(sign) && booleanNumberStarts == false)
        {
            if (temp == null)
            {
                temp = new Number();
                temp.X = lines.Length;
                temp.Y = line.Length;
            }
            temp.NumberCoordinates.Add(new Tuple<int, int>(i, j));
            booleanNumberStarts = true;
        }

        

        dictionary.Add(new Tuple<int, int>(i, j), line[j]);
    }

}

Console.WriteLine( listOfNumber.Where(o => o.IsMarked(dictionary)).Select(o => o.GetNumber(dictionary)).Sum());
Console.ReadKey();


public class Number
{
    public int X;
    public int Y;

    public char[] signs = new char[] { '#', '*', '$', '-', '%', '+', '@', '&', '=', '/' }; 

    public Number() {
        NumberCoordinates = new List<Tuple<int, int>>();
    }
    public   List<Tuple<int, int>> NumberCoordinates { get; set; }


    public Int32 GetNumber(Dictionary<Tuple<int, int>, char> allsigns)
    {
        if (NumberCoordinates.Count == 0)
            return 0;

        var builder = new StringBuilder();
        foreach (var coordinate in NumberCoordinates)
        {
            builder.Append(allsigns[coordinate]);
        }

        return Int32.Parse(builder.ToString());    
    }

    public Number NewClonedNumber()
    {
        return (Number)this.MemberwiseClone();
    }

    public bool IsMarked(Dictionary<Tuple<int, int>, char> allsigns)
    {
        Boolean ismarked = false;

        foreach (var point in NumberCoordinates)
        {
            var point1 = new Tuple<int, int>(point.Item1 - 1, point.Item2);
            var point2 = new Tuple<int, int>(point.Item1 - 1, point.Item2 + 1);
            var point3 = new Tuple<int, int>(point.Item1 - 1, point.Item2 - 1);
            var point4 = new Tuple<int, int>(point.Item1 + 1, point.Item2);
            var point5 = new Tuple<int, int>(point.Item1 + 1, point.Item2 + 1);
            var point6 = new Tuple<int, int>(point.Item1 + 1, point.Item2 - 1);
            var point7 = new Tuple<int, int>(point.Item1, point.Item2 - 1);
            var point8 = new Tuple<int, int>(point.Item1, point.Item2 + 1);

            ismarked = ismarked || MarkedPoint(point1, allsigns)
                || MarkedPoint(point2, allsigns) || MarkedPoint(point3, allsigns)
                 || MarkedPoint(point4, allsigns) || MarkedPoint(point5, allsigns)
                  || MarkedPoint(point6, allsigns) || MarkedPoint(point7, allsigns)
                   || MarkedPoint(point8, allsigns);
        }


        return ismarked;
    }

    public Boolean MarkedPoint(Tuple<int, int> point, Dictionary<Tuple<int, int>, char> allsigns)
    {
        if (point.Item1 < 0 ||
            point.Item1 > (X - 1) ||
            point.Item2 < 0 ||
            point.Item2 > (Y - 1) ||
            point.Item1 > (Y - 1) ||
            point.Item2 > (X - 1)
           )
          return false;
 

        if (signs.Contains(allsigns[point]))
            return true;
        
        return false;
  }

}


