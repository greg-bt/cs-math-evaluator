using System;

// Required
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace MathEval {
    class Program {
        static void Main(string[] args) {
            string x = Console.ReadLine();
            Console.WriteLine(EvalMath(x));
        }
        
        
        // Function to evaluate maths
        public static float EvalMath(string strExp) {
            while (strExp.Contains('(')) { // Evaluate the contence of any brackets and then replace them
                    // Remove the brackets from the string                                               And replace with the evaluation of said brackets
                strExp = strExp.Remove(strExp.IndexOf('('), strExp.LastIndexOf(')') - strExp.IndexOf('(') + 1).Insert(strExp.IndexOf('('), EvalMath(strExp.Substring(strExp.IndexOf('(') + 1, strExp.LastIndexOf(')') - strExp.IndexOf('(') - 1)).ToString());
            }
            
            // split the string into numbers and symbols for doing maths
            string[] x = Regex.Split(strExp, @"([x*\^\/]|(?<!E)[\+\-])");
            
            // convert that into a list so we can use some cool functions
            List<string> parts = x.ToList();

            int i = 0;
            // For every element untill there is only one element left that would have to contain the answer
            while (i < parts.Count) { 
                
                // if the element is any operator (could use !float.TryParse instead probs)
                if (parts[i] == "^" || parts[i] == "/" || parts[i] == "x" || parts[i] == "+" || parts[i] == "-" || parts[i] == "*") {
                    
                    // Replace that element (operater) with the result of the calculation using the numbers either side of it
                    parts[i] = MathDis(parts[i], float.Parse(parts[i - 1]), float.Parse(parts[i + 1])).ToString();
                    
                    // Remove the prior and latter elements that contained the numbers
                    parts.RemoveAt(i - 1);
                    parts.RemoveAt(i);
                    
                    // Restart from first element
                    i = 0;
                // Otherwise move on to the next element
                } else { i++; }
            }
            
            // Return the only remaining element of "parts" which must be the result
            return float.Parse(string.Join("", parts.ToArray()));
        }
        
        // Function to carry out operations
        public static float MathDis(string oprand, float x, float y) {
            switch (oprand) {
                    // Easy implementation of bidmas
                case "^": return (float)Math.Pow(x, y);
                case "/": return x / y;
                case "x": return x * y;
                case "*": return x * y;
                case "+": return x + y;
                case "-": return x - y;
                default: throw new Exception("invalid logic");
            }
        }
    }
}
