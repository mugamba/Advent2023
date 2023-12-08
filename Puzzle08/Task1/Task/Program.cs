using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("input.txt");
var dict = new Dictionary<string, Tuple<string, string>>();
var input = lines.First();



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
var nodeKey = "AAA";

while (true)
{

    if (nodeKey == "ZZZ")
        break;

    foreach (var c in input.ToCharArray())
    {

        if (nodeKey == "ZZZ")
            break;

        if (c == 'L')
            nodeKey = dict[nodeKey].Item1;
        else
            nodeKey = dict[nodeKey].Item2;

        counter = counter + 1;
    }

}

Console.WriteLine(counter);
Console.ReadKey();


