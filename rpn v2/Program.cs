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
            double startClc = Convert.ToDouble(input[1].Replace('.', ','));
            double endClc = Convert.ToDouble(input[2].Replace('.', ','));
            double stepClc = Convert.ToDouble(input[3].Replace('.',','));

            double i = startClc;
            while (i <= endClc)
            {
                dataX.Add(i);
                dataY.Add(GetResultDouble(RPN, i));
                i += stepClc;
            }
        }

    }
}
