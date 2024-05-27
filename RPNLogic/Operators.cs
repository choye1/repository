using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNLogic
{
    public abstract class Operator : Token 
    {
        public abstract string Name { get; }
        public abstract int Priority { get; }
        public abstract int ArgsCount { get; }
        public abstract bool IsFunction { get; }
        public abstract double Execute(params Number[] number);
        public override string ToString()
        {
            return Name;
        }
    }

    class Plus : Operator 
    {
        public override string Name => "+";
        public override int Priority => 1;
        public override int ArgsCount => 2;
        public override bool IsFunction => false;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            double number2 = number[1].number;
            return number1 + number2;
        }

    }

    class Minus : Operator
    {
        public override string Name => "-";
        public override int Priority => 1;
        public override int ArgsCount => 2;
        public override bool IsFunction => false;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            double number2 = number[1].number;
            return number1 - number2;
        }
    }

    class Multiply : Operator
    {
        public override string Name => "*";
        public override int Priority => 2;
        public override int ArgsCount => 2;
        public override bool IsFunction => false;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            double number2 = number[1].number;
            return number1 * number2;
        }
    }

    class Devide : Operator
    {
        public override string Name => "/";
        public override int Priority => 2;
        public override int ArgsCount => 2;
        public override bool IsFunction => false;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            double number2 = number[1].number;
            return number1 / number2;
        }
    }

    class Sin : Operator
    {
        public override string Name => "sin";
        public override int Priority => 3;
        public override int ArgsCount => 1;
        public override bool IsFunction => true;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            return Math.Sin(number1);
        }
    }

    class Cos : Operator
    {
        public override string Name => "cos";
        public override int Priority => 3;
        public override int ArgsCount => 1;
        public override bool IsFunction => true;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            return Math.Tan(number1);
        }
    }

    class Tg : Operator
    {
        public override string Name => "tg";
        public override int Priority => 3;
        public override int ArgsCount => 1;
        public override bool IsFunction => true;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            return Math.Tan(number1);
        }
    }

    class Ctg : Operator
    {
        public override string Name => "ctg";
        public override int Priority => 3;
        public override int ArgsCount => 1;
        public override bool IsFunction => true;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            return 1.0 / (Math.Tan(number1));
        }
    }

    class Log : Operator
    {
        public override string Name => "log";
        public override int Priority => 3;
        public override int ArgsCount => 2;
        public override bool IsFunction => true;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            double number2 = number[1].number;
            return Math.Log(number1,number2);
        }
    }

    class Lg : Operator
    {
        public override string Name => "lg";
        public override int Priority => 3;
        public override int ArgsCount => 1;
        public override bool IsFunction => true;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            return Math.Log(number1);
        }
    }

    class Rt : Operator
    {
        public override string Name => "rt";
        public override int Priority => 3;
        public override int ArgsCount => 2;
        public override bool IsFunction => true;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            double number2 = number[1].number;
            return Math.Pow(number1, 1.0/number2);
        }
    }

    class Sqrt : Operator
    {
        public override string Name => "sqrt";
        public override int Priority => 3;
        public override int ArgsCount => 1;
        public override bool IsFunction => true;
        public override double Execute(params Number[] number)
        {
            double number1 = number[0].number;
            return Math.Sqrt(number1);
        }
    }

    class Pow : Operator
    {
        public override string Name => "^";
        public override int Priority => 3;
        public override int ArgsCount => 2;
        public override bool IsFunction => true;
        public override double Execute(params Number[] number)
        {
            double number2 = number[0].number;
            double number1 = number[1].number;
            return Math.Pow(number1, number2);
        }
    }
}