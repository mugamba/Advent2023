
using System.ComponentModel.Design;

var lines = File.ReadAllLines("input.txt");

var listOfAllMapers = new List<Mapper>();
var listOfRanges = new List<Tuple<long, long>>();
var splits = lines[0].Replace("seeds:", "").Trim().Split(" ");

for (int i = 0; i < splits.Length; i = i + 2)
{
    listOfRanges.Add(new Tuple<long, long>(long.Parse(splits[i]), 
        long.Parse(splits[i]) + long.Parse(splits[i+1])-1));
}

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
        mapper.mappings.Add(new RangeOffset(long.Parse(ls[1]), 
            long.Parse(ls[1]) + long.Parse(ls[2])-1, 
            long.Parse(ls[0]) - long.Parse(ls[1])));
    }
}

if (mapper != null)
{
    var clone = mapper.GetClone();
    listOfAllMapers.Add(clone);
}

/*Add dummy ranges from 0 to first mapper and from last mapper to Infinity, with offset 0*/
foreach (var maper in listOfAllMapers)
{
    var firstSource = maper.mappings.Min(o => o.From);
    var lastTo = maper.mappings.Max(o => o.To);
    if (firstSource > 0)
        maper.mappings.Add(new RangeOffset(0, firstSource - 1, 0));
    
    maper.mappings.Add(new RangeOffset(lastTo + 1, long.MaxValue, 0));
}

var locations = new List<Tuple<long, long>>();

foreach (var r in listOfRanges)
{
    var temp = new List<Tuple<long, long>>();
    temp.Add(r);
    foreach (var map in listOfAllMapers)
    {
      temp = map.GetDestinationRange(temp);
    }
    locations.AddRange(temp);
}


Console.WriteLine(locations.Min(o=>o.Item1));
Console.ReadKey();


public class Mapper
{
    public Mapper()
    {

        mappings = new List<RangeOffset>();
    }

    public List<RangeOffset> mappings;

    public Mapper GetClone()
    {
        return (Mapper)this.MemberwiseClone();
    }


    public List<Tuple<long, long>> GetDestinationRange(List<Tuple<long, long>> listRange)
    {

        var returnList = new List<Tuple<long, long>>();

        foreach (var tupple in listRange)
        {

            foreach (var mapping in mappings)
            {
                if ((mapping.From <= tupple.Item1 && mapping.To >= tupple.Item1)
                || (mapping.From <= tupple.Item2 && mapping.To >= tupple.Item2))
                {
                    /*bothinside*/
                    if ((mapping.From <= tupple.Item1 && mapping.To >= tupple.Item1)
                 && (mapping.From <= tupple.Item2 && mapping.To >= tupple.Item2))
                        returnList.Add(new Tuple<long, long>(tupple.Item1 + mapping.Offset, tupple.Item2 + mapping.Offset));

                    else
                    {
                        if (mapping.From <= tupple.Item1 && mapping.To >= tupple.Item1)
                            returnList.Add(new Tuple<long, long>(tupple.Item1 + mapping.Offset, mapping.To + mapping.Offset));
                        else
                            returnList.Add(new Tuple<long, long>(mapping.From + mapping.Offset, tupple.Item2 + mapping.Offset));

                    }
                }

                    if (tupple.Item1 <= mapping.From && tupple.Item2 >= mapping.To)
                        returnList.Add(new Tuple<long, long>(mapping.From + mapping.Offset, mapping.To + mapping.Offset));

                //if (mapping.From >= tupple.Item1 && mapping.From <= tupple.Item2 && mapping.To <= tupple.Item2)
                //    returnList.Add(new Tuple<long, long>(mapping.From + mapping.Offset, mapping.To + mapping.Offset));
            }
        }
        return returnList;
    }
}

public class RangeOffset : IComparer<RangeOffset>
{
    public long From;
    public long To;
    public long Offset;

    public RangeOffset(long from, long to, long offset)
    {
        From = from;
        To = to;
        Offset = offset;
    }

    public int Compare(RangeOffset x, RangeOffset y)
    {
        return x.From.CompareTo(y.From);
    }
}

