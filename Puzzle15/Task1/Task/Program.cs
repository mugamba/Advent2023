using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    
    public static Dictionary<string, int> _memo = new Dictionary<string, int>();
  

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        
        var line = lines.First().Trim();

        var values = line.Split(",");

     
        IList<int> list = new List<int>();

        foreach (var value in values)
        {
            list.Add(DoHash(value, 0, 0));
        }


        Console.WriteLine(list.Sum());
        Console.ReadLine();
    }


    public static Int32 DoHash(string input, int depth, int result)
    {
        if (depth == input.Length)
        {
            return result;
        }
        result = result + (int)input[depth]; 
        result = result * 17;
        result = result % 256; 

        return DoHash(input, depth + 1, result);
    }
    
   
}

