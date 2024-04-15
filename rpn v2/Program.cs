using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPNLogic;

namespace rpn_v2
{
    public class Program
    {
        public static void Main(string[] args) 
        {
            
        }

        public static List<Token> Logic(string userInput)
        {
            List<Token> ParsedUserInput = Calculator.Parse(userInput);
            List<Token> RPN = Calculator.ConvertToRPN(ParsedUserInput);
            return RPN;
        }

        public static string GetResultString(List<Token> RPN) 
        {
            return Calculator.GetPrint(RPN); 
        }

        public static double GetResultDouble(List<Token> RPN, double valueVar)
        {
            return Calculator.Calculate(RPN, valueVar);
        }

    }
}
