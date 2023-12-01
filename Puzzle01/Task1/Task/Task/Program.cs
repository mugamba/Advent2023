using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt").Select(o => String.Join("", o.ToCharArray().Where(c => !(c >= 'a' && c <= 'z')).ToArray())).ToList();
            var listOfAll = new List<int>(); 

            foreach (var line in lines)
            {
                var num1 = line.First();
                var num2 = line.Last();
                listOfAll.Add(Int32.Parse(num1.ToString() + num2.ToString()));
            }


        //var maxElf = list.Max();



        Console.WriteLine("Result is {0}", listOfAll.Sum());
        Console.ReadKey();
        }
    }
}
