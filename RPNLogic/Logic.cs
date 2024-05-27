using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Threading.Tasks.Dataflow;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

//RPN (Reverse Polish Notation)
namespace RPNLogic
{
    public class Token { }

    public class Number : Token
    {
        public double number { get; set; }
        public double valueVar;
        public Number(double number) 
        {
            this.number = number;
        }
        public Number() { }

    }

    public class Letter : Token
    {
        public string letter;
        public Letter(string let) 
        {
            letter = let;
        }
    }

    public class Bracket : Token
    {
        public bool isOpen { get; }
        public Bracket(bool isOpen) 
        {
            this.isOpen = isOpen;
        }
    }


    public class Calculator
    {
        
        private static List<Operator> availableOperators = new List<Operator>()
        {
            new Plus(), new Minus(), new Multiply(), new Devide(), new Sin(), new Cos(), new Log(), new Lg(), new Rt(), new Sqrt(), new Pow()

        };


        public static List<Token> Parse(string userInput) //Parse userInput 
        {
            userInput = userInput.Replace('.', ',');
            string order = "";
            List<Token> result = new List<Token>();
            string number = "";

            foreach (char i in userInput)
            {
                if (i != ' ')
                {
                    if (char.IsDigit(i))
                    {
                        number += i;
                        if (order.Length > 0)
                        {
                            result.Add(Create(order));
                            order = "";
                        }
                    }
                    else
                    {
                        if (i.Equals(','))
                        {
                            result.Add(new Number(Convert.ToDouble(number)));
                            number = "";
                            continue;
                        }

                        if (number != "")
                        {
                            result.Add(new Number(Convert.ToDouble(number)));
                            number = "";
                        }

                        if (i.Equals('(') || i.Equals(')'))
                        {


                            if (order.Length > 0)
                            {
                                result.Add(Create(order));
                                order = "";
                            }
                            result.Add(Create(i.ToString()));

                        }

                        else
                        {
                            order += i;
                        }
                    }

                }
            }

            if (order != "")
            {
                result.Add(Create(order));
                order = "";
            }

            if (number != "")
            {
                result.Add(new Number(Convert.ToDouble(number)));
                number = "";
            }

            return result;
        }

        public static Token Create(string name) 
        {
            foreach(Operator op in availableOperators) 
            {
                if (op.Name == name) 
                {
                    return op;
                }
            }

            if (name == "(")
            {
                return new Bracket(true);
            }
            else if (name == ")")
            {
                return new Bracket(false);
            }
            else 
            {
                return new Letter(name);
            }
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

                else if (e is Letter)
                {
                    Letter letter = (Letter)e;
                    output += letter.letter + " ";
                }

                else if (e is Operator)
                {
                    Operator op = (Operator)e;
                    output += op.Name + " ";

                }

                else if (e is Bracket)
                {
                    if (((Bracket)e).isOpen)
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

        public static List<Token> ConvertToRPN(List<Token> userInput) //Convert To RPN
        {
            Stack<Token> operators = new Stack<Token>();
            List<Token> result = new();
            foreach (Token i in userInput)
            {
                if (i is Letter)
                {
                    result.Add((Letter)i);
                }

                else if (i is Number)
                {
                    result.Add((Number)i);
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

                else if (operators.Count > 0 && (operators.Peek() is Bracket))
                {
                    operators.Push(i);
                }

                else if (i is Operator)
                {
                    while (operators.Count > 0 && (Priority(operators.Peek()) >= Priority(i)))
                    {
                        result.Add(operators.Pop());
                    }

                    operators.Push((Operator)i);
                }
            }

            while (operators.Count > 0)
            {
                result.Add(operators.Pop());
            }

            return result;
        }

        public static int Priority(Token token) 
        {
            if (token is Operator) { return ((Operator)token).Priority; }
            else if (token is Bracket) { return 0;  }
            else { return 0; }
        }

        public static double Calculate(List<Token> outputList, double valueVar) //calculate result of all expression
        {
            Stack<double> num = new();
            List<Token> calculateList = ReplaceLetter(outputList, valueVar);
            foreach (Token i in calculateList)
            {
                if (i is Number number)
                {
                    num.Push(number.number);
                }

                else if (i is Operator)
                {
                    List <Number> arg = new List<Number>();
                    for(int j  = 0; j < ((Operator)i).ArgsCount; j++) 
                    {
                        Number number1 = new();
                        number1.number = num.Pop();
                        arg.Add(number1);
                    }

                    num.Push(((Operator)i).Execute(arg.ToArray()));
                }
            }

            double resultNum = 1;
            if (num.Count != 0) { resultNum = num.Pop(); }
            return resultNum;
        }

        public static List<Token> ReplaceLetter(List<Token> calculateList, double valuevar)
        {
            List<Token> result = new();
            for (int i = 0; i < calculateList.Count; i++) 
            {
                if (calculateList[i] is Letter) 
                {
                    result.Add(new Number(valuevar));
                }
                else
                {
                    result.Add(calculateList[i]);
                }
            }

            return result;
        }


    }
}