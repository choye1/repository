using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Threading.Tasks.Dataflow;
using System.Collections.Generic;
using System.ComponentModel;

//RPN (Reverse Polish Notation)
namespace RPNLogic
{
    public class Token { }

    public class Number : Token
    {
        public double number;
        public double valueVar;
    }

    public class Letter : Token
    {
        public char letter;
    }

    public class Operator : Token
    {
        public char operation;
    }
    public class LetOperator:Token
    {
        public char[] letOp;
        public string name;
        public int countOfValue;
        public int[] values;
    }

    public class Bracket : Token
    {
        public bool isOpen;
    }

    public class Calculator
    {
        public static List<Token> Parse(string userInput) //Parse userInput 
        {
            string order = "";
            List<Token> result = new List<Token>();
            string number = "";

            foreach (char i in userInput)
            {
                if (i != ' ')
                {
                    if (order.Length > 0)
                    {
                    }

                    if (char.IsDigit(i))
                    {
                        if (order.Length > 0)
                        {
                            order += i;
                        }

                        else { number += i; }
                    }

                    else if (char.IsLetter(i))
                    {
                        order += i;
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
                            if (order.Length == 1)
                            {
                                Letter letter = new Letter();
                                letter.letter = i;
                                result.Add(letter);
                                order = "";
                            }

                            Operator op = new Operator();
                            op.operation = i;
                            result.Add(op);
                        }

                        else if (i.Equals(','))
                        {
                            order += i;
                        }

                        else if (i.Equals('('))
                        {
                            if (order.Length > 0)
                            {
                                order += i;
                            }

                            else
                            {
                                Bracket par = new Bracket();
                                par.isOpen = true;
                                result.Add(par);
                            }
                        }

                        else
                        {
                            if (order.Length > 0)
                            {
                                order += i;
                                LetOperator letOp = new LetOperator();
                                letOp.letOp = order.ToCharArray();
                                result.Add(letOp);
                                order = "";
                            }

                            else
                            {
                                Bracket par = new Bracket();
                                par.isOpen = false;
                                result.Add(par);
                            }
                        }

                        number = "";
                    }
                }
            }

            if (order != "")
            {
                Letter letter = new Letter();
                letter.letter = Convert.ToChar(order);
                result.Add(letter);
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

                else if(e is LetOperator)
                {
                    string letOp = "";
                    LetOperator letOperator = (LetOperator)e;
                    foreach (char i in letOperator.letOp)
                    {
                        letOp += i;
                    }
                    
                    output += letOp + " ";
                }

                else if (e is Operator)
                {
                    Operator op = (Operator)e;
                    output += op.operation + " ";

                }

                else if (e is Bracket)
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

                else if (i is LetOperator)
                {
                    result.Add((LetOperator)i);
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

        public static double Calculate(List<Token> outputList, double valueVar) //calculate result of all expression
        {
            Stack<double> num = new();
            foreach (Token i in outputList)
            {

                if (i is Number number)
                {
                    num.Push(number.number);
                }

                else if (i is Letter)
                {
                    num.Push(valueVar);
                }

                else if (i is LetOperator)
                {
                    ParseLetOp((LetOperator)i,valueVar);
                    
                    num.Push(CalculateLetOp((LetOperator)i));
                }

                else if (i is Operator)
                {
                    double scnd = num.Pop();
                    double frst = num.Pop();
                    Number scndNum = new();
                    scndNum.number = scnd;
                    Number frstNum = new();
                    frstNum.number = frst;
                    double result = (CalculateOneExpression(frstNum, scndNum, (Operator)i)).number;
                    num.Push(result);
                }
            }

            double resultNum = 1;
            if (num.Count != 0) { resultNum = num.Pop(); }
            return resultNum;
        }

        public static void ParseLetOp(LetOperator letOp, double valueVar)
        {

            char[] oper = letOp.letOp;
            string order = "";
            List<int> values = new List<int>();
            foreach (char c in oper) 
            {
                if (char.IsLetter(c))
                {
                    order += c;
                }

                else if (c == '(')
                {
                    letOp.name = order;
                    order = "";
                }

                else if (char.IsDigit(c))
                {
                    order += c;
                }

                else if (c == ',' || c == ')')
                {
                    bool isNumberic = int.TryParse(order,out _);
                    if (isNumberic)
                    {
                        values.Add(Convert.ToInt32(order));
                    }

                    else
                    {
                        values.Add(Convert.ToInt32(valueVar));
                    }

                    order = "";
                }
            }

            letOp.values = values.ToArray();
            letOp.countOfValue = values.Count;
        }

        static double CalculateLetOp(LetOperator letOp)
        {
            int[] values = letOp.values;
            switch (letOp.name) 
            {
                case ("log"):
                    return Math.Log(values[1], values[0]);

                case ("abs"):
                    return Math.Abs(values[0]);

                case ("sin"):
                    return Math.Sin(values[0]);

                case ("cos"):
                    return Math.Abs(values[0]);

                case ("ln"):
                    return Math.Log(values[0]);

                case ("lg"):
                    return Math.Log10(values[0]);

                case ("asin"):
                    return Math.Asin(values[0]);

                default: break;
            }

            return 0;
        }
    }
}