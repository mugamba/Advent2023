using System.Linq;

var lines = File.ReadAllLines("input.txt");
var listOfAll = new List<int>();
var dict = new Dictionary<int, int>();

for (int i = 1; i < lines.Length+1; i++)
    dict.Add(i, 1);


int counter = 1;
foreach (var line in lines)
{
    var repNumber = dict[counter];
    line.Split(":");
    var splits = line.Split(":");
    var setSplits = splits[1].Split("|");

    var drawn = setSplits[0].Split(" ").Select(o => o.Trim()).Where(o => !String.IsNullOrWhiteSpace(o));
    var wining = setSplits[1].Split(" ").Select(o => o.Trim()).Where(o => !String.IsNullOrWhiteSpace(o));
    var hits = drawn.Intersect(wining).Count();

    for (int i = 0; i < hits; i++)
    {
        dict[counter + i + 1] = dict[counter + i + 1] + repNumber;
    }
    
    counter++;
}

//var filtered = listOfAll.Where(o => o.Sets.All(c => c.Blue <= 14 && c.Red <= 12 && c.Green <= 13)).ToList();
Console.WriteLine(dict.Select(o=>o.Value).Sum());
Console.ReadKey();


