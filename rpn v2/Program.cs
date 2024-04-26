using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPNLogic;
using ScottPlot;

namespace rpn_v2
{
    public class Program
    {
        public static void Main(string[] args) 
        {

        }

        public static List<Token> Logic(string[] input, List<double> dataX, List<double> dataY)
        {

            List<Token> ParsedUserInput = Calculator.Parse(input[0]);
            List<Token> RPN = Calculator.ConvertToRPN(ParsedUserInput);
            GetCoordinate(input, dataX, dataY, RPN);
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

        public static void GetCoordinate(string[] input, List<double> dataX, List<double> dataY, List<Token> RPN) 
        {
            int startClc = Convert.ToInt32(input[1]);
            int endClc = Convert.ToInt32(input[2]);
            int stepClc = Convert.ToInt32(input[3]);
            for (int i = startClc; i <= endClc; i += stepClc)
            {
                dataX.Add(i);
                dataY.Add(GetResultDouble(RPN, i));
            }
        }

    }
}
