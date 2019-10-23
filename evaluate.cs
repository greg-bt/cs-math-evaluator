using System;

// Required
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace MathEval {
    class Program {
        static void Main(string[] args) {
            string sum = Console.ReadLine();
            Console.WriteLine(EvalMath(sum));
        }
        
        
        // Function to evaluate maths
        public static float EvalMath(string strExp) {

            // Function to carry out operations
            float MathDis(string symbol, float x, float y) {
                switch (symbol) {// Easy implementation of bidmas
                    case "^": return (float)Math.Pow(x, y);
                    case "/": return x / y;
                    case "x": return x * y;
                    case "*": return x * y;
                    case "+": return x + y;
                    case "-": return x - y;
                    default: throw new Exception("invalid logic");
                }
            }

            // Evaluate the contence of any brackets and then replace them
            while (strExp.Contains('(')) { 
                // Remove the brackets from the string
                strExp = strExp.Remove(
                            strExp.IndexOf('('),
                            strExp.LastIndexOf(')') - strExp.IndexOf('(') + 1
                            ).Insert(   // And replace with the evaluation of said brackets
                                strExp.IndexOf('('), 
                                // Evaluation of said brackets
                                EvalMath(
                                    strExp.Substring(
                                        strExp.IndexOf('(') + 1,
                                        strExp.LastIndexOf(')') - strExp.IndexOf('(') - 1
                                    )
                                ).ToString()
                            );
            }
            
            // split the string into numbers and symbols for doing maths and 
            // convert that into a list so we can use some cool functions
            List<string> parts = (Regex.Split(strExp, @"([x*\^\/]|(?<!E)[\+\-])")).ToList();

            int i = 0;
            string opcode = " ";
            int weight = 0;

            // For every element untill there is only one element left that would have to contain the answer
            while (i < parts.Count) { 

                // Determine if parts[i] is an operator and if so order by bidmas ( weight )
                       if (parts[i] == "-" && weight <2) { opcode = parts[i]; weight = 1;
                } else if (parts[i] == "+" && weight <3) { opcode = parts[i]; weight = 2;
                } else if (parts[i] == "*" && weight <4) { opcode = parts[i]; weight = 3;
                } else if (parts[i] == "x" && weight <5) { opcode = parts[i]; weight = 4;
                } else if (parts[i] == "/" && weight <6) { opcode = parts[i]; weight = 5;
                } else if (parts[i] == "^" && weight <7) { opcode = parts[i]; weight = 6; }
                i++;

                // Once all posible operators have been checked
                if( weight != 0 && i == parts.Count-1) {
                    i = parts.LastIndexOf(opcode);
                    // Replace that element (operater) with the result of the calculation using the numbers either side of it
                    parts[i] = MathDis(parts[i], float.Parse(parts[i - 1]), float.Parse(parts[i + 1])).ToString();

                    // Remove the prior and latter elements that contained the numbers
                    parts.RemoveAt(i - 1);
                    parts.RemoveAt(i);

                    i = 0;
                    weight = 0;
                    // Otherwise move on to the next element
                }
            }
            // Return the only remaining element of "parts" which must be the result
            return float.Parse(parts[0]);
        }
    }
}
