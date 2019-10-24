# cs-math-evaluator
A simple c# function capable of evaluating a mathematical operation which is given in the form of a string, such as "(5+6)x(3/9)".<br>
The EvalMath function takes one argument in the form of a string and returns the result as a float.<br>
The string should be sanitised before being passed into the function as it will result in an error otherwise.<br>
Requires:
```
System.Text.RegularExpressions;
System.Collections.Generic;
System.Linq;
```
