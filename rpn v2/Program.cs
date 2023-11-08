using System;
using System.Globalization;
using System.Linq.Expressions;

//RPN (Reverse Polish Notation)
class Program
{
    public static void Main() //input, transformation, calculate, output.
    { 
        string userInput = Console.ReadLine();
        List<object> parsedInput = Parsing(userInput);
        List<object> outputList = Convertation(parsedInput);
        Console.Write("rpn: ");
        foreach(object part in outputList) {Console.Write(part.ToString() + " ");}
        /*  int result = Calculate(outputString);
          Console.WriteLine("result:", result);*/
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
                        if (lastIsDigit) { num += variable; }
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

    public static int Preority(object operation) 
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

    public static List<object> Convertation(List<object> parsedInput)
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
                    output.Add(stack.Pop());
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
            while (stack.Count>0) output.Add(stack.Pop());
        }

        return output;
    }

    public static int Calculate(string outputString)
    {
        return 4;
    }
}

