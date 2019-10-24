# cs-math-evaluator
A simple c# function capable of evaluating a mathematical operation which is given in the form of a string, such as "(5+6)x(3/9)".

The EvalMath function takes one argument in the form of a string and returns the result as a float.

The string should be sanitised before being passed into the function as it will result in an error otherwise.

Requires:
```
System.Text.RegularExpressions;
System.Collections.Generic;
System.Linq;
```

# EvalMath Function

```cs
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
            default: throw new Exception("invalid logic");
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

    string[] signs = {"-","+","*","x","/","^"}; // Weight is indicated by a sign's index
    string opcode = " "; // The operator going to be used
    int weight = 0; // Denotes how early on a particular sign comes in bidmas
    int i = 0;

    // When only one element exists it must be the answer
    while (i < mathExp.Count) { 

        // Determine if mathExp[i] is a sign
        for (int j =0;j < 6;j++) {
            // Determine if the sign comes before the others in bidmas
            if(mathExp[i] == signs[j] && weight < j) {
                opcode = mathExp[i]; // Update the to earliest sign found so far
                weight = i; // Update to the greatest weight found so far
            }
        }
        i++;

        // Once all posible operators have been checked
        if( weight != 0 && i == mathExp.Count-1) {
            i = mathExp.LastIndexOf(opcode); // Using the earliest bidmas sign found . . 
            // Replace that sign with the result of the calculation using the numbers either side of it
            mathExp[i] = MathDis(mathExp[i],                  // The sign
                                 float.Parse(mathExp[i - 1]), // First number
                                 float.Parse(mathExp[i + 1])  // Second number
                                ).ToString();

            // Remove the prior and latter elements that contained the numbers
            // so that only the result is returned to the list
            mathExp.RemoveAt(i - 1); // prior
            mathExp.RemoveAt(i);     // latter

            i = 0;
            weight = 0;
            // Otherwise move on to the next element because it isn't a sign
        }
    }
    // Return the only remaining element of "mathExp" which must be the result
    return float.Parse(mathExp[0]);
}

```
