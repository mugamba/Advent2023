using System.Linq;

var lines = File.ReadAllLines("input.txt");
var listOfAll = new List<int>();

foreach (var line in lines)
{
    line.Split(":");
    
    var splits = line.Split(":");
    var setSplits = splits[1].Split("|");

    var drawn = setSplits[0].Split(" ").Select(o=>o.Trim()).Where(o=>!String.IsNullOrWhiteSpace(o));
    var wining = setSplits[1].Split(" ").Select(o => o.Trim()).Where(o => !String.IsNullOrWhiteSpace(o));
    var hits=  drawn.Intersect(wining).Count();

    if (hits > 0)
     listOfAll.Add((int)Math.Pow(2, hits - 1));
}

//var filtered = listOfAll.Where(o => o.Sets.All(c => c.Blue <= 14 && c.Red <= 12 && c.Green <= 13)).ToList();
Console.WriteLine(listOfAll.Sum());
Console.ReadKey();


