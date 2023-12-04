using System;
using System.Linq;
using System.Text;

var lines = File.ReadAllLines("input.txt");
var dictionary = new Dictionary<Tuple<int, int>, char>();
var signHits = new Dictionary<Tuple<int, int>, SignHit>();

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

foreach(var number in listOfNumber) 
{
    number.IsMarked(dictionary, signHits);

}

Console.WriteLine(signHits.Select(o=>o.Value).Where(o=>o.Numbers.Count == 2).Select(o => o.Numbers[0] * o.Numbers[1]).Sum());
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

    public bool IsMarked(Dictionary<Tuple<int, int>, char> allsigns, Dictionary<Tuple<int, int>, SignHit> signHit)
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

            ismarked = ismarked || MarkedPoint(point1, allsigns, signHit)
                || MarkedPoint(point2, allsigns, signHit) || MarkedPoint(point3, allsigns, signHit)
                 || MarkedPoint(point4, allsigns, signHit) || MarkedPoint(point5, allsigns, signHit)
                  || MarkedPoint(point6, allsigns, signHit) || MarkedPoint(point7, allsigns, signHit)
                   || MarkedPoint(point8, allsigns, signHit);
        }


        return ismarked;
    }

    public Boolean MarkedPoint(Tuple<int, int> point, Dictionary<Tuple<int, int>, char> allsigns, Dictionary<Tuple<int, int>, SignHit> signHit)
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
        {
            if (allsigns[point] == '*')
            {
                if (!signHit.ContainsKey(point))
                {
                    signHit[point] = new SignHit();
                    signHit[point].NumberCoordinates.AddRange(NumberCoordinates);
                    signHit[point].Numbers.Add(this.GetNumber(allsigns));
                }
                else
                {
                    if (!signHit[point].NumberCoordinates.Contains(point))
                    {
                        signHit[point].NumberCoordinates.AddRange(NumberCoordinates);
                        signHit[point].Numbers.Add(this.GetNumber(allsigns));
                    }

                }

            }

            return true;
        }
        return false;
  }
}


public class SignHit
{

    public SignHit()
    {
        NumberCoordinates = new List<Tuple<int, int>>();
        Numbers = new List<int>();

    }

    public List<Tuple<int, int>> NumberCoordinates { get; set; }

    public List<int> Numbers { get; set; }

}



