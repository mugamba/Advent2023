using System.Drawing;
using System.Text;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    public static List<string> _list = new List<string>();
   
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        var sum = 0;
        foreach (var line in lines)
        {

            var splits = line.Split(' ');

            _list.Clear();
            GetAllPosibleValues(new List<char>(), splits[0], 0);

            sum +=  _list.Where(o => IsMatch(o, splits[1])).Count();

        }


        Console.WriteLine(sum);
        Console.ReadLine();

    }



    public static void GetAllPosibleValues(List<char> currentString, string inputLine, int index)
    {

        if (inputLine.Length == index)
        {
            _list.Add(String.Join(string.Empty, currentString));             
            return;
        }

        if (inputLine[index] == '?')
        {
            var firstLine = currentString.ToList();
            firstLine.Add('#');

            var secondLine = currentString.ToList();
            secondLine.Add('.');

            GetAllPosibleValues(firstLine, inputLine, index + 1);
            GetAllPosibleValues(secondLine, inputLine, index + 1);
        }
        else
        {
            var firstLine = currentString.ToList();
            firstLine.Add(inputLine[index]);
            GetAllPosibleValues(firstLine, inputLine, index + 1);
        } 
    }

    public static Boolean IsMatch(string input, string pattern)
    { 
       var list = new List<int>(pattern.Split(',').Select(o=>int.Parse(o.Trim())));
        var array1 = input.Split('.').Select(o => o.Trim()).Where(o => o.Contains("#"));
        var listOfString = new List<string>(array1);
    

        if (listOfString.Count != list.Count)
            return false;

        for (int i = 0; i < listOfString.Count; i++)
        {

            if (list[i] != listOfString[i].Length)
                return false;
        
        }

        return true;

    
    }

}

