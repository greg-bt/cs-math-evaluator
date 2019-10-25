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
        int firstBracket = strExp.IndexOf('(');
        int lastBracket = strExp.LastIndexOf(')');
        // Remove the bracketed expressions from the string and solve said bracketed expressions
        // by calling EvalMath and then replace them with the solved bracketed expression, simples!
        strExp = strExp.Remove( firstBracket, ( lastBracket - firstBracket + 1 )
                    ).Insert( firstBracket, 
                        EvalMath( strExp.Substring( ( firstBracket + 1 ), ( lastBracket - firstBracket - 1 ) )).ToString());
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
