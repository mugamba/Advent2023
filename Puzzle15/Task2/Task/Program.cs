using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    
    public static Dictionary<int, List<Lens>> _memo = new Dictionary<int, List<Lens>>();
  

    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        
        var line = lines.First().Trim();

        var values = line.Split(",");

        

        for (int i = 0; i < 256; i++)
        { 
            _memo.Add(i, new List<Lens>());
        }

        foreach (var value in values)
        {
            if (value.Contains("-"))
            {
                var tohash = value.Trim('-');
                var box = DoHash(tohash, 0, 0);
                var toRemove = _memo[box].Where(o => o._key == tohash).FirstOrDefault();
                if (toRemove != null)
                    _memo[box].Remove(toRemove);
            }
            else
            { 
                var splited = value.Split("=");
                var tohash = splited[0];
                var lensValue = Int32.Parse(splited[1]);
                var box = DoHash(tohash, 0, 0);
                var toUpdate = _memo[box].Where(o => o._key == tohash).FirstOrDefault();
                if (toUpdate != null)
                    toUpdate._value = lensValue;
                else
                    _memo[box].Add(new Lens(tohash, lensValue));

            }
        }

        var totalsum = 0;
        foreach (var m in _memo)
        {
            var mult = (m.Key + 1);

            var index = 0;
            foreach (var v in m.Value)
            {
                totalsum +=  mult * (index + 1) * v._value;
                index++;
 
            }
        }
        Console.WriteLine(totalsum);
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

    public class Lens
    {
        public String _key;
        public int _value;

        public Lens(string key, int value)
        {
            _key = key;
            _value = value;
        }
    }


}

