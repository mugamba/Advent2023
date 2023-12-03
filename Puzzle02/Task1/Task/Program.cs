using System.Linq;

var lines = File.ReadAllLines("input.txt");
var listOfAll = new List<Game>();

foreach (var line in lines)
{
    var game = new Game();
    
    var splits = line.Split(":");
    game.Id = Int32.Parse(splits[0].Replace("Game ", ""));
    var setSplits = splits[1].Split(";");
    foreach (var setSpilt in setSplits)
    {
        var set = new Set();
        set.ParseColors(setSpilt);
        game.Sets.Add(set);
    }
    
    listOfAll.Add(game);

}

var filtered = listOfAll.Where(o => o.Sets.All(c => c.Blue <= 14 && c.Red <= 12 && c.Green <= 13)).ToList();
Console.WriteLine(filtered.Select(o => o.Id).Sum());
Console.ReadKey();


public class Game
{
    public Game() 
    {
        Sets = new List<Set>();
    }
   public int Id { get; set; }
    public List<Set> Sets { get; set;}
}

public class Set
{
    public int Blue { get; set; }
    public int Red { get; set; }
    public int Green { get; set; }

    internal void ParseColors(string colorString)
    {

        var colorSplits = colorString.Split(",");
        foreach (var color in colorSplits)
        {
            if (color.Contains("blue"))
                Blue = Convert.ToInt32(color.Replace("blue", "").Trim());
            if (color.Contains("red"))
                Red = Convert.ToInt32(color.Replace("red", "").Trim());
            if (color.Contains("green"))
                Green = Convert.ToInt32(color.Replace("green", "").Trim());
        }
    }
}

