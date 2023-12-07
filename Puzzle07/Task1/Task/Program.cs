using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("input.txt");

Dictionary<char, int> possibleCards = new Dictionary<char, int>
{
  { 'A', 13 },
  { 'K', 12 },
  { 'Q', 11 },
  { 'J', 10 },
  { 'T', 9 },
  { '9', 8 },
  { '8', 7 },
  { '7', 6 },
  { '6', 5 },
  { '5', 4 },
  { '4', 3 },
  { '3', 2 },
  { '2', 1 }
};
var firstLine = lines.First().Replace("Time:", "").Trim();
var lastLine = lines.Last().Replace("Distance:", "").Trim();







Console.WriteLine(result);
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
  { 'J', 10 },
  { 'T', 9 },
  { '9', 8 },
  { '8', 7 },
  { '7', 6 },
  { '6', 5 },
  { '5', 4 },
  { '4', 3 },
  { '3', 2 },
  { '2', 1 }
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
            .OrderBy(o => o.Item2).ToList();

        if (groups.Count == 1)
            return 7;

        if (groups.Count == 2 && groups[0].Item2 == 4)
            return 6;

        if (groups.Count == 2 && groups[0].Item2 == 3)
            return 5;

        if (groups.Count == 3 && groups[0].Item2 == 3)
            return 4;

        if (groups.Count == 3 && groups[0].Item2 == 2)
            return 3;
        if (groups.Count == 4 && groups[0].Item2 == 2)
            return 2;
        if (groups.Count == 5)
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