using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("input.txt");

var firstLine = lines.First().Replace("Time:", "").Trim();
var lastLine = lines.Last().Replace("Distance:", "").Trim();


var firstlineSplits = firstLine.Split(" ").Where(o=>!String.IsNullOrWhiteSpace(o)).ToArray();
var lastLineSplits = lastLine.Split(" ").Where(o => !String.IsNullOrWhiteSpace(o)).ToArray();


var listOfallRaces = new List<BoatRace>();

for (var i = 0; i < firstlineSplits.Length; i++)
{ 
    var boat = new BoatRace(int.Parse(firstlineSplits[i].Trim()), int.Parse(lastLineSplits[i].Trim()));

    listOfallRaces.Add(boat);
}

var result = 1;
foreach (var boat in listOfallRaces)
{
    boat.GetPossibleOutcomes();


    result = result * boat.possibleOutcomes.Where(o => o._distance > boat._distanceToBeat).Count();
}





Console.WriteLine(result);
Console.ReadKey();


public class BoatRace
{

    public int _time;
    public int _distanceToBeat;

    public BoatRace(int time, int distanceToBeat)
    {

        possibleOutcomes = new List<PossibleOutcomes>();
        _time = time;
        _distanceToBeat = distanceToBeat;
    }

    public List<PossibleOutcomes> possibleOutcomes;

    public BoatRace GetClone()
    {
        return (BoatRace)this.MemberwiseClone();
    }

    public void GetPossibleOutcomes()
    { 
        for(int i = 1;i<= _time;i++) {
            possibleOutcomes.Add(new PossibleOutcomes(i, i * (_time - i)));
        }
    }

}

public class PossibleOutcomes
{
    public int _buttonHold;
    public int _distance;

    public PossibleOutcomes(int buttonHold, int distance)
    { 
        _buttonHold = buttonHold;
        _distance = distance;
    }

}

