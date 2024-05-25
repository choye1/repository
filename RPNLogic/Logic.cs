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
        public char letter;
    }

    //public class Operator : Token
    //{
    //    public char operation;
    //}
    public class LetOperator : Token
    {
        public char[] letOp;
        public string name;
        public int countOfValue;
        public int[] values;
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
            new Plus(), new Minus(), new Multiply(), new Devide(), new Sin()
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
                        if (number != "")
                        {
                            result.Add(new Number(Convert.ToDouble(number)));
                            number = "";
                        }

                        if (i.Equals('(') || i.Equals(')'))
                        {
                            //result.Add(Create(i.ToString()));
                            if (order.Length > 0)
                            {
                                result.Add(Create(order));
                                order = "";
                            }

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


                    //        else if (char.IsLetter(i))
                    //        {
                    //            order += i;
                    //        }

                    //        else
                    //        {
                    //            if (order.Length == 1)
                    //            {

                    //                Letter letter = new Letter();
                    //                letter.letter = Convert.ToChar(order);
                    //                result.Add(letter);
                    //                order = "";
                    //            }

                    //            if (number != "")
                    //            {
                    //                Number num = new();
                    //                num.number = Convert.ToDouble(number);
                    //                result.Add(num);
                    //            }

                    //            if (i.Equals('-') || i.Equals('+') || i.Equals('*') || i.Equals('/') || i.Equals('^'))
                    //            {
                    //                result.Add(Create(i.ToString()));
                    //            }

                    //            else if (i.Equals(','))
                    //            {
                    //                continue;
                    //            }

                    //            else if (i.Equals('('))
                    //            {
                    //                Bracket par = new Bracket();
                    //                par.isOpen = true;
                    //                result.Add(par);
                    //            }
                    //            else if (i.Equals(')'))
                    //            {
                    //                Bracket par = new Bracket();
                    //                par.isOpen = false;
                    //                result.Add(par);
                    //            }

                    //            else
                    //            {
                    //                if (order.Length > 0)
                    //                {
                    //                    order += i;
                    //                    result.Add(Create(order));
                    //                    order = "";
                    //                }


                    //            }

                    //            number = "";
                    //        }
                    //    }
                    //}

                    //if (order != "")
                    //{
                    //    Letter letter = new Letter();
                    //    letter.letter = Convert.ToChar(order);
                    //    result.Add(letter);
                    //}

                    //if (number != "")
                    //{
                    //    Number num = new Number();
                    //    num.number = Convert.ToDouble(number);
                    //    result.Add(num);
                    //}

                    
                
            

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
            else
            {
                return new Bracket(false);
            }
            return new Token(); 
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

                else if (e is LetOperator)
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
                    output += op.Name + " ";

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
                    while (operators.Count > 0 && ((Operator)operators.Peek()).Priority >= ((Operator)i).Priority)
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
                    bool isNumberic = int.TryParse(order, out _);
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
    }
}