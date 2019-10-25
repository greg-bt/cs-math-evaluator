using System;

// Required
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace MathEval {
    class Program {
        static void Main(string[] args) {
            System.Console.WriteLine("Sum:");
            string sum = Console.ReadLine();
            Console.WriteLine(EvalMath(sum));
        }
        
        // Function to evaluate maths in string form
        public static float EvalMath(string strExp) {

            // Local function that carries out most simplified operations
            float MathDis(string sign, float x, float y) {
                switch (sign) {
                    case "^": return (float)Math.Pow(x, y);
                    case "/": return x / y;
                    case "x": return x * y;
                    case "*": return x * y;
                    case "+": return x + y;
                    case "-": return x - y;
                    default: return 0;
                }
            }

            // Solve the contense of any brackets and then replace them with the result
            while (strExp.Contains('(')) { 
                // Remove the bracketed expressions from the string
                strExp = strExp.Remove(
                            strExp.IndexOf('('),                              // Start of the expression
                            strExp.LastIndexOf(')') - strExp.IndexOf('(') + 1 // End of the expression
                // And replace them with the solved bracketed expression
                            ).Insert( 
                                strExp.IndexOf('('), 
                // Solve said bracketed expressions by calling EvalMath
                                EvalMath(
                                    strExp.Substring( // Extract the expression from the brackets
                                        strExp.IndexOf('(') + 1,                           // Start of the expression
                                        strExp.LastIndexOf(')') - strExp.IndexOf('(') - 1  // End of the expression
                                    )).ToString());
            }
            
            // Split the string Expression into a Array of numbers and symbols
            // Then convert that into a list for ease of use
            List<string> mathExp = (Regex.Split(strExp, @"([x*\^\/]|(?<!E)[\+\-])")).ToList();

            string[] signs = {"^","/","x","*","+","-"}; // Bidmas
            int i = 0;

            foreach (string sign in signs) {
                while (mathExp.Contains(sign)) {
                    i = mathExp.IndexOf(sign);
                                        // Replace that sign with the result of the calculation using the numbers either side of it
                    mathExp[i-1] = MathDis(mathExp[i],          // The sign
                                         float.Parse(mathExp[i - 1]), // First number
                                         float.Parse(mathExp[i + 1])  // Second number
                                        ).ToString();
                        // Remove the prior and latter elements that contained the numbers
                        // so that only the result is returned to the list
                        mathExp.RemoveRange(i, i+1);
                }
            }
            // When only one element exists it must be the answer
            // Return the only remaining element of "mathExp" which must be the result
            return float.Parse(mathExp[0]);
        }
    }
}
