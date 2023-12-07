using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("input.txt");

var listofallHandbids = new List<HandBid>();
foreach (var line in lines)
{
    var splits =line.Split(" ");
    listofallHandbids.Add(new HandBid(splits[0].Trim(), Int32.Parse(splits[1].Trim())));
}


var order = listofallHandbids.OrderBy(x => x).ToList();
int rank = 1;
var sum = 0;
foreach (var hand in order)
{
    sum = sum + hand._bid * rank;
    rank++;
}

Console.WriteLine(sum);
Console.ReadKey();

public class HandBid : IComparable
{
    public String _hand;
    public int _bid;

   public static Dictionary<char, int> possibleCards = new Dictionary<char, int>
{
  { 'A', 13 },
  { 'K', 12 },
  { 'Q', 11 },
  { 'T', 9 },
  { '9', 8 },
  { '8', 7 },
  { '7', 6 },
  { '6', 5 },
  { '5', 4 },
  { '4', 3 },
  { '3', 2 },
  { '2', 1 },
  { 'J', 0 },
};

    public HandBid(String hand, int bid)
    {
        _hand = hand;
        _bid = bid;
    }

    public int CompareTo(object? obj)
    {
        var hand = obj as HandBid;
        if (hand == null)
            return 1;

        var toh = hand.GetTypeOfHand();
        var ttoh = this.GetTypeOfHand();

        if (ttoh > toh) return 1;
        if (ttoh < toh) return -1;

        return BiggerCardCompare(hand._hand);

    }

    public int GetTypeOfHand()
    {
        var groups = _hand.ToCharArray().GroupBy(o => o)
            .Select(g => new Tuple<char, int>(g.Key, g.Count()))
            .OrderByDescending(o => o.Item2).ToList();

        var groupsOfJJ = groups.Where(o => o.Item1 == 'J').FirstOrDefault();
        var checkList = groups.Where(o => o.Item1 != 'J').ToList().OrderByDescending(o => o.Item2).ToList();

        if (checkList.Any() && groupsOfJJ != null)
            checkList[0] = new Tuple<char, int>(checkList[0].Item1, checkList[0].Item2 + groupsOfJJ.Item2);

        if (checkList.Count == 0)
            checkList = new List<Tuple<char, int>>() { groupsOfJJ };

        if (checkList.Count == 1)
            return 7;

        if (checkList.Count == 2 && checkList[0].Item2 == 4)
            return 6;

        if (checkList.Count == 2 && checkList[0].Item2 == 3)
            return 5;

        if (checkList.Count == 3 && checkList[0].Item2 == 3)
            return 4;

        if (checkList.Count == 3 && checkList[0].Item2 == 2)
            return 3;
        if (checkList.Count == 4 && checkList[0].Item2 == 2)
            return 2;
        if (checkList.Count == 5)
            return 1;
        return 0;
    }


    public int BiggerCardCompare(String hand)
    {
        for (int i = 0; i < 5; i++) 
        {
            if (possibleCards[_hand[i]] > possibleCards[hand[i]])
                return 1;
            if (possibleCards[_hand[i]] < possibleCards[hand[i]])
                return -1;

        }
        return 0;
    }

}