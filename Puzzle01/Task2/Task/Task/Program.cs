
using System.Collections.Generic;
using System.Text.RegularExpressions;

var lines = File.ReadAllLines("input.txt");
var pattern = @"\d|one|two|three|four|five|six|seven|eight|nine";

var result = 0;
foreach (var line in lines)
{
    var first = Regex.Match(line, pattern).Value;
    var last = Regex.Match(line, pattern, RegexOptions.RightToLeft).Value;
    result += ParseMatch(first) * 10 + ParseMatch(last);
}

Console.WriteLine(result);
Console.ReadKey();

    int ParseMatch(string st) => st switch
    {
        "one" => 1,
        "two" => 2,
        "three" => 3,
        "four" => 4,
        "five" => 5,
        "six" => 6,
        "seven" => 7,
        "eight" => 8,
        "nine" => 9,
        var d => int.Parse(d)
    };
