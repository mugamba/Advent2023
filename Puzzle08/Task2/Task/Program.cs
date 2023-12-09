using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("input.txt");
var dict = new Dictionary<string, Tuple<string, string>>();
var input = lines.First();
var offsetts = new Dictionary<int, long>();
var repeats = new Dictionary<int, long>();
var repeats1 = new Dictionary<int, long>();


foreach (var line in lines.Skip(2))
{
    var splits =line.Split("=");

    var dictKey = splits[0].Trim();

    var ss = splits[1].Replace("(", "").Replace(")", "").Split(",");
    var left = ss[0].Trim();
    var right = ss[1].Trim();

    dict.Add(dictKey, new Tuple<string, string> ( left, right ));

}

var counter = 0;
var allnodes = dict.Where(o=>o.Key.EndsWith("A")).Select(o=>o.Key).ToArray();

var breakall = false;
while (true)
{

    if (breakall)
        break;

    if (allnodes.Length == offsetts.Count && allnodes.Length == repeats.Count)
        break;


    foreach (var c in input.ToCharArray())
    {
        var temp = new List<string>();
        if (allnodes.All(o => o.EndsWith("Z")))
        {
            breakall = true;
            break;
        }

        if (c == 'L')
        {
            for (var i = 0; i < allnodes.Length; i++)
            {
                allnodes[i] = dict[allnodes[i]].Item1;
                if (allnodes[i].EndsWith("Z"))
                {
                    if (!offsetts.ContainsKey(i))
                        offsetts.Add(i, counter);
                    else
                        if (!repeats.ContainsKey(i))
                            repeats.Add(i, counter - offsetts[i]);
                       
                        
                }
            }

        }
        else
            for (var i = 0; i < allnodes.Length; i++)
            {
                allnodes[i] = dict[allnodes[i]].Item2;
                if (allnodes[i].EndsWith("Z"))
                {
                    if (!offsetts.ContainsKey(i))
                        offsetts.Add(i, counter);
                    else
                        if (!repeats.ContainsKey(i))
                            repeats.Add(i, counter - offsetts[i]);
                       
                }
            }
        
        counter = counter + 1;
    }
}



ulong gcd = 1;
ulong commonMultiplyer = 1;

foreach (var repeatingOffsets in repeats)
{ 
    gcd = GreatestCommonDivisor(commonMultiplyer, (ulong)repeatingOffsets.Value);
    commonMultiplyer = (commonMultiplyer * (ulong)repeatingOffsets.Value) / gcd;
}

Console.WriteLine(commonMultiplyer);
//Console.WriteLine(resulT);
Console.ReadKey();


static ulong GreatestCommonDivisor(ulong a, ulong b)
{
    while (a != 0 && b != 0)
    {
        if (a > b)
            a %= b;
        else
            b %= a;
    }

    return a | b;
}