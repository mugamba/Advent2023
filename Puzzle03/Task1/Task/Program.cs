using System;
using System.Linq;

var lines = File.ReadAllLines("input.txt");
var dictionary = new Dictionary<Tuple<int, int>, char>();

char[] numbers = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i].ToCharArray();
    var booleanNumberStarts = false;
    for (int j = 0; j < line.Length; j++)
    {
        var sign = line[j];

        if (sign == '.')
            booleanNumberStarts = false;
        if (numbers.Contains(sign) && booleanNumberStarts == true)




        dictionary.Add(new Tuple<int, int>(i, j), line[j]);
    }
}

Console.WriteLine("Test");
Console.ReadKey();


public class Number
{
    public Number() {
        NumberCoordinates = new List<Tuple<int, int>>();
    }
    public   List<Tuple<int, int>> NumberCoordinates { get; set; }

}


