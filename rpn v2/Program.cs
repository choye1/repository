//5 лаба находится во второй ветке.
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Threading.Tasks.Dataflow;

//RPN (Reverse Polish Notation)
class Program
{
    public static void Main() //input, transformation, calculate, output.
    {
        string userInput = Console.ReadLine();
        List<object> parsedInput = Parsing(userInput);
        List<object> outputList = Conversion(parsedInput);
        Console.Write("rpn: ");
        foreach (object part in outputList)  Console.Write(part.ToString() + " "); 

        string result = Calculate(outputList).ToString();
        Console.Write("result:");
        Console.Write(result);
    }

    public static List<object> Parsing(string userInput) //Parsing expression.
    {
        bool lastIsDigit = false;
        string num = "";
        List<object> parsingList = new List<object>();
        foreach (char variable in userInput)
        {
            if (variable != ' ') 
            {
                if (char.IsDigit(variable))
                {  
                    if (num == "") num += variable;
                    else
                    {
                        if (lastIsDigit)  num += variable; 
                        else 
                        {
                            parsingList.Add(num);
                            num = Convert.ToString(variable);
                        }

                    }
                    lastIsDigit = true;
                }

                else { parsingList.Add(num);  parsingList.Add(variable); lastIsDigit = false; num = ""; }

            } 

        }

        if (num != "") parsingList.Add(num);
        return parsingList;
    }

    public static int Preority(object operation) //prioritizing operations
    {
        switch (operation) 
        {
            case '+' or '-':
                return 1;
            case '/' or '*':
                return 2;
            default:
                return 0;
        }

    }

    public static List<object> Conversion(List<object> parsedInput) //conversion to RPN
    {
        Stack<object> stack = new Stack<object>();
        List<object> output = new List<object>();
        foreach (object variable in parsedInput)
        {
            if (variable is string) output.Add(variable);
            else if (Convert.ToChar(variable) == '(') stack.Push(variable);
            else if (Convert.ToChar(variable) == ')' && stack.Count != 0)
            {
                while (stack.Count != 0 && Convert.ToChar(stack.Peek()) != '(')
                {
                    object item = stack.Pop();
                    output.Add(item);
                }

                stack.Pop();
            }

            else if ((stack.Count == 0) || (Preority(stack.Peek()) <= Preority(variable))) stack.Push(variable);
            else if (Preority(stack.Peek())> Preority(variable))
            {
                while ((stack.Count != 0) || (Convert.ToChar(stack.Peek()) != '(')) output.Add(stack.Pop());
                stack.Push(variable);
            }
            
        }
        if (stack.Count > 0)
        {
            while (stack.Count > 0)
            {
                object item = stack.Pop();
                output.Add(item); 
            }
        }

        return output;
    }

    public static double CalculateOneExpression(double firstNum, double secondNum, char operation) //calculate result of 1 expression
    {
        switch (operation)
        {
            case '+':
                return firstNum + secondNum;
            case '-':
                return firstNum - secondNum;
            case '*':
                return firstNum * secondNum;
            case '/':
                return firstNum / secondNum;
            default:
                return 0;
        }

    }

    public static object Calculate(List<object> outputList) //calculate result of all expression
    {
        for (int i = 0; i < outputList.Count; i++)
        {
            if (outputList[i] is char)
            {
                double fisrtNumber = Convert.ToSingle(outputList[i - 2]);
                double secondNumber = Convert.ToSingle(outputList[i - 1]);

                double result = CalculateOneExpression(fisrtNumber, secondNumber, Convert.ToChar(outputList[i]));

                outputList.RemoveRange(i - 2, 3);
                outputList.Insert(i - 2, result);
                i -= 2;
            }
            outputList.Remove("");
        }

         return outputList[0];
    }
}

