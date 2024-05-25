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

}