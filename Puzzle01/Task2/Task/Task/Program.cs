
var testArrayLetters = new Dictionary<string, string> {
    { "one", "1" },
    { "two", "2" },
    { "three", "3" },
    { "four", "4" },
    { "five",  "5" },
    { "six", "6" },
    { "seven",  "7" },
    { "eight", "8" },
    { "nine",  "9" }
};

var testArrayNumbers = new List<string> {  "1", "2", "3", "4", "5", "6", "7", "8", "9" };
var lines = File.ReadAllLines("input.txt");
var listOfAll = new List<int>();

foreach (var line in lines)
{
    var mostLeftIndex = line.Length;
    var mostRightIndex = 0;
    var mostLeftValue = "y";
    var mostRightValue = "y";

    foreach (var item in testArrayNumbers)
    {

        if (line.IndexOf(item) <= mostLeftIndex && line.IndexOf(item) >= 0)
        {
            mostLeftIndex = line.IndexOf(item);
            mostLeftValue = item;
        }

        if (line.LastIndexOf(item) >= mostRightIndex && line.LastIndexOf(item) >= 0)
        {
            mostRightIndex = line.LastIndexOf(item);
            mostRightValue = item;
        }
    }

    foreach (var item in testArrayLetters.Keys)
    {
        if (line.IndexOf(item) <= mostLeftIndex && line.IndexOf(item)>=0)
        {
            mostLeftIndex = line.IndexOf(item);
            mostLeftValue = testArrayLetters[item].ToString();
        }

        if (line.LastIndexOf(item) >= mostRightIndex && line.LastIndexOf(item) >= 0)
        {
            mostRightIndex = line.LastIndexOf(item);
            mostRightValue = testArrayLetters[item].ToString();
        }

    }

    

    listOfAll.Add(Int32.Parse(mostLeftValue + mostRightValue));

    
}


Console.WriteLine(listOfAll.Sum());
Console.ReadKey();