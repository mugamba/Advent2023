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

            var temp = GetAllPosibleValues(new List<char>(), splits[0], 0, new List<string>());
            var firstMatchString = splits[1];
            for (int i = 0; i < 4; i++)
            {
                var t1 = GetAllPosibleValues(new List<char>(), "?" + splits[0], 0, new List<string>());
             
                temp = CombineStringLists(temp, t1);
                firstMatchString = firstMatchString + "," + splits[1];
                temp = CombineStringLists(temp, t1);
                firstMatchString = firstMatchString + "," + splits[1];
              
                temp = temp.Where(o => IsMatch(o, firstMatchString)).ToList();
            }
            
            
           






           

        }
        Console.WriteLine(sum);
        Console.ReadLine();

    }

    public static List<String> CombineStringLists(IList<string> list1, IList<string> list2)
    { 
        var list = new List<String>();
            foreach (var item in list1)
                foreach (var item2 in list2)
                    list.Add(item+item2);
    
    
            return list;
    }

    public static IList<String> GetAllPosibleValues(List<char> currentString, string inputLine, int index, IList<string> strings)
    {


        if (inputLine.Length == index)
        {
            strings.Add(String.Join(string.Empty, currentString));             
            return strings;
        }

        if (inputLine[index] == '?')
        {
            var firstLine = currentString.ToList();
            firstLine.Add('#');

            var secondLine = currentString.ToList();
            secondLine.Add('.');

            GetAllPosibleValues(firstLine, inputLine, index + 1, strings);
            GetAllPosibleValues(secondLine, inputLine, index + 1, strings);
        }
        else
        {
            var firstLines = currentString.ToList();
            firstLines.Add(inputLine[index]);
            GetAllPosibleValues(firstLines, inputLine, index + 1, strings);
        }

        return strings.ToList();



    }

    public static Boolean IsMatch(string input, string pattern)
    {
        var list = new List<int>(pattern.Split(',').Select(o => int.Parse(o.Trim())));
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

