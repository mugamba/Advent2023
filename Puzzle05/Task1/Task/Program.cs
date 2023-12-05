using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("input.txt");

var listOfAllMapers = new List<Mapper>();
var listofSeeds = new List<long>();
var splits = lines[0].Replace("seeds:", "").Trim().Split(" ");
listofSeeds.AddRange(splits.Select(o => long.Parse(o)));
int counter = 0;

Mapper mapper = null;
foreach (var line in lines)
{
    if (mapper != null && line == string.Empty)
    {
        var clone = mapper.GetClone();
        listOfAllMapers.Add(clone);
    }

    if (line.Contains("map:"))
    {
        mapper = new Mapper();
        continue;
    }

    if (mapper != null && line != string.Empty)
    {
        var ls = line.Split(" ");
        mapper.mappings.Add(new Tuple<long, long, long>(long.Parse(ls[0]), long.Parse(ls[1]), long.Parse(ls[2])));
    }

}

if (mapper != null)
{
    var clone = mapper.GetClone();
    listOfAllMapers.Add(clone);
}


var locations = new List<long>();
foreach (var seed in listofSeeds)
{
    var temp = seed;
    for (int i = 0; i < listOfAllMapers.Count; i++)
    {
        temp = listOfAllMapers[i].GetDestination(temp);
    }

    locations.Add(temp);
}
Console.WriteLine(locations.Min());
Console.ReadKey();


public class Mapper
{

    public Mapper()
    {

        mappings = new List<Tuple<long, long, long>>();
    }

    public List<Tuple<long, long, long>> mappings;

    public Mapper GetClone()
    {
        return (Mapper)this.MemberwiseClone();
    }


    public long GetDestination(long source)
    {
        var mapping = mappings.Where(o => o.Item2 <= source && (o.Item2 + o.Item3) > source).FirstOrDefault();
        if (mapping != null)
            return source + mapping.Item1 - mapping.Item2;

       return source;
    }

}

