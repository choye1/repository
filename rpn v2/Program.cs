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
        static void Main(string[] args, string userInput) 
        {
            
        }

        public static void Start(string[] args, string userInput)
        {
            List<Token> ParsedUserInput = Calculator.Parse(userInput);
            List<Token> RPN = Calculator.ConvertToRPN(ParsedUserInput);
            double result = Calculator.Calculate(RPN);
            Calculator.Print(RPN);
        }
    }
}
