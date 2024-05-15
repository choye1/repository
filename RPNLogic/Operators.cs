using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNLogic
{
    class CalculateExpression
    {
        public static Number CalculateOneExpression(Number first, Number second, Operator op) //Calculate one expression
        {
            Number result = new();
            switch (op.operation)
            {
                case ('+'):
                    result.number = first.number + second.number;
                    break;

                case ('-'):
                    result.number = first.number - second.number;
                    break;

                case ('*'):
                    result.number = first.number * second.number;
                    break;

                case ('/'):
                    result.number = first.number / second.number;
                    break;

                case ('^'):
                    result.number = Math.Pow(first.number, second.number);
                    break;
            }

            return result;
        }

        public static double CalculateLetOp(LetOperator letOp)
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

                case ("sqrt"):
                    return Math.Sqrt(values[0]);

                case ("rt"):
                    return Math.Pow(values[0], (1.0 / values[1]));


                default: break;
            }

            return 0;
        }
    }
}
