using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;

class Program
{

    
    public static Dictionary<string, String> _memo = new Dictionary<string, String>();
    public static List<Node> _checkingLines = new List<Node>();
    public static List<Node> _correctline = new List<Node>();


    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var index = lines.Select((item, index) => new { Item = item, Pos = index }).Where(o => o.Item == "").Select(o => o.Pos).First();


        for (int i = 0; i < index; i++) 
        {
           var splits = lines[i].Split("{");
            _memo.Add(splits[0].Trim(), splits[1].Replace("}", "").Trim());
        }

        for (int i = index + 1; i < lines.Length; i++)
        {

            var splits = lines[i].Replace("{", "").Replace("}", "").Split(",");

            var x= int.Parse(splits[0].Replace("x=", ""));
            var m = int.Parse(splits[1].Replace("m=", ""));
            var a = int.Parse(splits[2].Replace("a=", ""));
            var s = int.Parse(splits[3].Replace("s=", ""));


            _checkingLines.Add(new Node(x, m, a, s));

        }

        foreach (var node in _checkingLines)
        {
            var temp = _memo["in"];
            while (temp != null) 
            {

              var keyy =  node.ToCheck(temp);

                if (keyy == "A")
                {
                    _correctline.Add(node);
                    break;
                }
                if (keyy == "R")
                {
                    break;
                }
                temp = _memo[keyy];

            }
        }

        //foreach (var value in values)
        //{
        //    list.Add(DoHash(value, 0, 0));
        //}


        Console.WriteLine(_correctline.Select(o=> o.a + o.x+o.s+o.m).Sum());
        Console.ReadLine();
    }


    public class Node
    {
        public int a;
        public int m;
        public int s;
        public int x;

        public Node(int x, int m, int a,  int s) {
            this.a = a;
            this.m = m;
            this.s = s;
            this.x = x;
        }


        public String ToCheck(string test)
        {
            
            var splits = test.Split(",");

            var fallBack = splits.Last();

            for (int i = 0; i < splits.Length - 1; i++)
            {
                var cond = splits[i].Split(":");
                if (isTrue(cond[0].Trim()))
                    return cond[1].Trim();
            }

            return fallBack;
        }

        public Boolean isTrue(string expression)
        {
            if (expression.Contains("<"))
            {
               var splits =  expression.Split("<");

                var varia = splits[0];
                var number = int.Parse(splits[1]);

                if (varia == "a")
                    return a < number;

                if (varia == "m")
                    return m < number;

                if (varia == "s")
                    return s < number;

                if (varia == "x")
                    return x < number;

            }

            if (expression.Contains(">"))
            {
                var splits = expression.Split(">");

                var varia = splits[0];
                var number = int.Parse(splits[1]);

                if (varia == "a")
                    return a > number;

                if (varia == "m")
                    return m > number;

                if (varia == "s")
                    return s > number;

                if (varia == "x")
                    return x > number;

            }

            return false;
        }

    }




}

