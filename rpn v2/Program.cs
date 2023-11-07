using System;
using System.Globalization;

class Program
{
    public static void Input()
    { 
        string userInput = Console.ReadLine();
        string outputString = Main(userInput);
        Console.WriteLine("rpn:", outputString);
        int result = Calculate(outputString);
        Console.WriteLine("result:", result);
    }

    public static string Main(string userInput)
    {
        Stack<string> stack = new Stack<string>();
        string outputString = "";
        string digit = "0123456789";
        for (int i = 0; i < userInput.Length; i++)
        {
            string lastStackVariable = stack.Peek();
            string variable = Convert.ToString(userInput[i]);
            switch(variable)
            {
                case "1" or "2" or "3" or "3" or "4" or "5" or "6" or "7" or "8" or "9" or "0":
                    outputString += variable;
                    break;

                case "+" or "-":
                    switch (lastStackVariable)
                    {
                        case "*" or "/":
                            while (stack.Peek() != "")
                            {
                                outputString += stack.Pop();
                            }

                            break;
                        default:
                            stack.Push(variable);
                            break;
                    }

                    break;
                case "*" or "/" or "(":
                    stack.Push(variable);
                    break;
                case ")":
                    while (stack.Peek() != "(")
                    {
                        outputString += stack.Pop();
                    }
                    break;
                    
            }
        }

        return "";
    }

    public static int Calculate(string outputString)
    {
        return 4;
    }
}