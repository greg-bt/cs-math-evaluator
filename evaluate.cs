using System;

// Required
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;


namespace Tmp {
    class Program {
        static void Main(string[] args) {
        
            string x = Console.ReadLine();
            Console.WriteLine(EvalMath(x));
        }
        
        // Function to evaluate maths
        public static float EvalMath(string s) {
        
            while (s.Contains('(')) { // Evaluate the contence of any brackets and then replace them
                s = s.Remove(s.IndexOf('('), s.IndexOf(')') - s.IndexOf('(') + 1).Insert(s.IndexOf('('), EvalMath(s.Substring(s.IndexOf('(') + 1, s.IndexOf(')') - s.IndexOf('(') - 1)).ToString());
            }
            
            string[] x = Regex.Split(s, @"([x()\^\/]|(?<!E)[\+\-])");
            List<string> parts = x.ToList(); // split input into numbers and operators

            int i = 0;
            while (i < parts.Count) { // For every element
                if (parts[i] == "^" || parts[i] == "/" || parts[i] == "x" || parts[i] == "+" || parts[i] == "-") { // If there is an operator
                    parts[i] = MathDis(parts[i], float.Parse(parts[i - 1]), float.Parse(parts[i + 1])).ToString(); // Pass to mathDis for evaluation
                    parts.RemoveAt(i - 1);
                    parts.RemoveAt(i);      // remove the calculated elements
                    i = 0;
                } else { i++; }
            }
            return float.Parse(string.Join("", parts.ToArray()));
        }
        
        // Function to carry out operations
        public static float MathDis(string oprand, float x, float y) {
            switch (oprand) {
                case "^": return (float)Math.Pow(x, y);
                case "/": return x / y;
                case "x": return x * y;
                case "+": return x + y;
                case "-": return x - y;
                default: throw new Exception("invalid logic");
            }
        }
    }
}
