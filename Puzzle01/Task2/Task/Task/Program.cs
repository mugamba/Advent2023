
using System.Collections.Generic;

var testArrayLetters = new Dictionary<string, string> {
    { "one", "1" },
    { "two", "2" },
    { "three", "3" },
    { "four", "4" },
    { "five",  "5" },
    { "six", "6" },
    { "seven",  "7" },
    { "eight", "8" },
    { "nine",  "9" }
};

var testArrayNumbers = new List<string> {  "1", "2", "3", "4", "5", "6", "7", "8", "9" };
var lines = File.ReadAllLines("input.txt");
var listOfAll = new List<int>();

foreach (var line in lines)
{
    List<Tuple<string, int, int>> mostleft = new List<Tuple<string, int, int>>();
    List<Tuple<string, int, int>>mostRight = new List<Tuple<string, int, int>>();

    foreach (var item in testArrayNumbers)
    {
        var index = line.IndexOf(item);
        if (index >= 0)
            mostleft.Add(new Tuple<string, int, int>(item, index, index));

        var lastIndex = line.LastIndexOf(item);
        if (lastIndex >= 0)
            mostRight.Add(new Tuple<string, int, int>(item, lastIndex, lastIndex));

    }

    foreach (var item in testArrayLetters.Keys)
    {
        var index = line.IndexOf(item);
        if (index >= 0)
            mostleft.Add(new Tuple<string, int, int>(testArrayLetters[item], index, index + item.Length-1));

        var lastIndex = line.LastIndexOf(item);
        if (lastIndex >= 0)
            mostRight.Add(new Tuple<string, int, int>(testArrayLetters[item], lastIndex, lastIndex + item.Length-1));
    }

    var test =  mostRight.Where(o => mostRight.All(g => !(g.Item2 <= o.Item2 && g.Item3 >= o.Item2) || (g.Item2 == o.Item2 && g.Item3 == o.Item3)));



    var mostLeftValue = mostleft.OrderBy(x => x.Item2).Select(o=>o.Item1).First();
    var mostRightValue = test.OrderByDescending(x => x.Item2).Select(o => o.Item1).First();

   Console.WriteLine("{0} -->> {1}   {2}", line, mostLeftValue, mostRightValue);
        
   


   listOfAll.Add(Int32.Parse(mostLeftValue + mostRightValue));
}


Console.WriteLine(listOfAll.Sum());
Console.ReadKey();