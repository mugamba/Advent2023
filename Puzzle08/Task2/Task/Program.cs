using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("input.txt");
var dict = new Dictionary<string, Tuple<string, string>>();
var input = lines.First();

var zocc = new Dictionary<int, List<int>>();






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

while (true)
{

    if (allnodes.All(o => o.EndsWith("Z")))
        break;

    foreach (var c in input.ToCharArray())
    {
        var temp = new List<string>();
        if (allnodes.All(o => o.EndsWith("Z")))
            break;

        if (c == 'L')
        {
            for (var i = 0; i < allnodes.Length; i++)
            {
                allnodes[i] = dict[allnodes[i]].Item1;
                if (allnodes[i].EndsWith("Z"))
                {
                    if (zocc.ContainsKey(i))
                        zocc[i].Add(counter);
                    else
                        zocc.Add(i, new List<int>(i));
                }
            }

        }
        else
            for (var i = 0; i < allnodes.Length; i++)
            {
                allnodes[i] = dict[allnodes[i]].Item2;
                if (allnodes[i].EndsWith("Z"))
                {
                    if (zocc.ContainsKey(i))
                        zocc[i].Add(counter);
                    else
                        zocc.Add(i, new List<int>(i));
                }
            }







        counter = counter + 1;
    }

}

Console.WriteLine(counter);
Console.ReadKey();


