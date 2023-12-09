using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("input.txt");

var recursionDict = new Dictionary<int, List<int>>();

var sum = 0;
foreach (var line in lines)
{
    recursionDict.Clear();
    var list = new List<int>();
    var splits = line.Split(" ");

    foreach (var split in splits)
        list.Add(int.Parse(split));

    recursionDict.Add(1, list);

    makeTree(recursionDict, 1);
    traverseBack(recursionDict, recursionDict.Count);

    sum = sum + recursionDict[1].First();
}


Console.WriteLine(sum);
Console.ReadKey();


void makeTree(Dictionary<int, List<int>> input, int depth)
{
    if (input[depth].All(o => o == 0))
        return;

    var lstEntry = input[depth];

    var newEntry = new List<int>();
    for (int i = 1; i < lstEntry.Count; i++)
        newEntry.Add(lstEntry[i] - lstEntry[i - 1]);


    depth = depth + 1;
    input.Add(depth, newEntry);

    makeTree(input, depth);

}

void traverseBack(Dictionary<int, List<int>> input, int depth)
{

    if (depth == 1)
    {
        input[depth].Insert(0, input[depth].First() - input[depth + 1].First());
        return;
    }


    if (depth == input.Count)
    {
        input[depth].Insert(0, 0);
        depth--;
        traverseBack(input, depth);
    }
    else
    {
        input[depth].Insert(0, input[depth].First() - input[depth + 1].First());
        depth--;
        traverseBack(input, depth);
    }
}
