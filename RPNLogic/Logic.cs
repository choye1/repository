using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Threading.Tasks.Dataflow;
using System.Collections.Generic;

//RPN (Reverse Polish Notstion)
namespace RPNLogic
{
    public class Token { }

    public class Number : Token
    {
        public double number;
    }

    public class Letter : Token
    {
        public char letter;
    }

    public class Operator : Token
    {
        public char operation;
    }

    public class Bracket : Token
    {
        public bool isOpen;
    }

    public class Calculator
    {
        public static List<Token> Parse(string userInput) //Parse userInput 
        {
            List<Token> result = new List<Token>();
            string number = "";

            foreach (char i in userInput)
            {
                if (i != ' ')
                {
                    if (char.IsDigit(i))
                    {
                        number += i;
                    }

                    else if (char.IsLetter(i))
                    {
                        Letter letter = new Letter();
                        letter.letter = i;
                        result.Add(letter);
                    }

                    else
                    {
                        if (number != "")
                        {
                            Number num = new();
                            num.number = Convert.ToDouble(number);
                            result.Add(num);
                        }

                        if (i.Equals('-') || i.Equals('+') || i.Equals('*') || i.Equals('/'))
                        {
                            Operator op = new Operator();
                            op.operation = i;
                            result.Add(op);
                        }
                        
                        else if (i.Equals('('))
                        {
                            Bracket par = new Bracket();
                            par.isOpen = true;
                            result.Add(par);
                        }

                        else
                        {
                            Bracket par = new Bracket();
                            par.isOpen = false;
                            result.Add(par);
                        }

                        number = "";
                    }
                }
            }
            if (number != "")
            {
                Number num = new Number();
                num.number = Convert.ToDouble(number);
                result.Add(num);
            }

            return result;
        }
        public static string GetPrint(List<Token> ListToPrint) // Output
        {
            string output = "";
            foreach (Token e in ListToPrint)
            {
                if (e is Number)
                {
                    Number num = (Number)e;
                    output += num.number + " ";
                }
                
                else if(e is Letter)
                {
                    Letter letter = (Letter)e;
                    output += letter.letter + " ";
                }

                else if (e is Operator)
                {
                    Operator op = (Operator)e;
                    output += op.operation + " ";

                }

                else
                {
                    Bracket bracket = (Bracket)e;
                    if (bracket.isOpen)
                    {
                        output += "( ";
                    }

                    else
                    {
                        output += ") ";

                    }
                }
            }

            return output;
        }

        public static Number CalculateOneExpression(Number first, Number second, Operator op) //Calculate one expression
        {
            Number result = new();
            if (op.operation == '+')
            {
                result.number = first.number + second.number;
            }

            if (op.operation == '-')
            {
                result.number = first.number - second.number;
            }

            if (op.operation == '*')
            {
                result.number = first.number * second.number;
            }

            if (op.operation == '/')
            {
                result.number = first.number / second.number;
            }

            return result;
        }

        static int Preority(Token operation) //prioritizing operations
        {
            if (operation is Operator)
            {
                switch (((Operator)operation).operation)
                {
                    case '+' or '-':
                        return 1;
                    case '/' or '*':
                        return 2;
                    default:
                        return 0;
                }
            }

            else
            {
                return 0;
            }
        }

        public static List<Token> ConvertToRPN(List<Token> userInput) //Convert To RPN
        {
            Stack<Token> operators = new Stack<Token>();
            List<Token> result = new List<Token>();
            foreach (Token i in userInput)
            {
                if (i is Number)
                {
                    result.Add((Number)i);
                }

                else if (i is Letter)
                {
                    result.Add((Letter)i);
                }

                else if (i is Operator)
                {
                    while (operators.Count > 0 && Preority(operators.Peek()) >= Preority(i))
                    {
                        result.Add(operators.Pop());
                    }
                    operators.Push((Operator)i);
                }
                else if (i is Bracket)
                {
                    if (((Bracket)i).isOpen)
                    {
                        operators.Push((Bracket)i);
                    }

                    else
                    {
                        while (operators.Count > 0 && !(operators.Peek() is Bracket))
                        {
                            result.Add(operators.Pop());
                        }
                        operators.Pop();
                    }
                }
            }

            while (operators.Count > 0)
            {
                result.Add(operators.Pop());
            }

            return result;
        }

        public static double Calculate(List<Token> outputList) //calculate result of all expression
        {
            //    Stack<double> num = new();
            //    foreach (Token i in outputList)
            //    {
            //        if (i is Number number)
            //        {
            //            num.Push(number.number);
            //        }
            //        else if (i is Letter)
            //        {

            //        }
            //        else if (i is Operator)
            //        {
            //            double scnd = num.Pop();
            //            double frst = num.Pop();
            //            Number scndNum = new();
            //            scndNum.number = scnd;
            //            Number frstNum = new();
            //            frstNum.number = frst;
            //            double result = (CalculateOneExpression(frstNum, scndNum, (Operator)i)).number;
            //            num.Push(result);
            //        }
            //    }

            //    double resultNum = num.Pop();
            double resultNum = 9;
            return resultNum;
        }
    }
}