using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("input.txt");

var firstLine = lines.First().Replace("Time:", "").Trim();
var lastLine = lines.Last().Replace("Distance:", "").Trim();


var firstlineSplits = firstLine.Split(" ").Where(o=>!String.IsNullOrWhiteSpace(o)).ToArray();
var lastLineSplits = lastLine.Split(" ").Where(o => !String.IsNullOrWhiteSpace(o)).ToArray();


var listOfallRaces = new List<BoatRace>();


    var boat = new BoatRace(long.Parse(String.Join("", firstlineSplits.Select(o=>o.Trim()))), 
        long.Parse(String.Join("", lastLineSplits.Select(o => o.Trim()))));

    
    long result = 0;

    boat.GetFailedOutcomes();
    result = boat._time - boat.failedOutcomes.Count();

Console.WriteLine(result);
Console.ReadKey();


public class BoatRace
{

    public long _time;
    public long _distanceToBeat;

    public BoatRace(long time, long distanceToBeat)
    {

        failedOutcomes = new List<PossibleOutcomes>();
        _time = time;
        _distanceToBeat = distanceToBeat;
    }

    public List<PossibleOutcomes> failedOutcomes;

    public BoatRace GetClone()
    {
        return (BoatRace)this.MemberwiseClone();
    }

    public void GetPossibleOutcomes()
    { 
        for(long i = 1;i<= _time;i++) {
            failedOutcomes.Add(new PossibleOutcomes(i, i * (_time - i)));
        }
    }

    public void GetFailedOutcomes()
    {
        for (long i = 1; i <= _time; i++)
        {
            if (i * (_time - i) > _distanceToBeat)
                break;


            failedOutcomes.Add(new PossibleOutcomes(i, i * (_time - i)));
        }

        for (long i = _time; i >= 0; i--)
        {
            if (i * (_time - i) > _distanceToBeat)
                break;

            failedOutcomes.Add(new PossibleOutcomes(i, i * (_time - i)));
        }

    }

}

public class PossibleOutcomes
{
    public long _buttonHold;
    public long _distance;

    public PossibleOutcomes(long buttonHold, long distance)
    { 
        _buttonHold = buttonHold;
        _distance = distance;
    }
}

