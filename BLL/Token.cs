using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    internal abstract class Token
    {
    }

    internal abstract class OperatorToken : Token
    {
        public abstract int Precedence { get; }
        public abstract Func<double, double, double> Func { get; }
    }

    internal class AdditionToken : OperatorToken
    {
        public override int Precedence { get { return 1; } }
        public override Func<double, double, double> Func { get { return (a, b) => a + b; } }
    }

    internal class SubtractionToken : OperatorToken
    {
        public override int Precedence { get { return 1; } }
        public override Func<double, double, double> Func { get { return (a, b) => a - b; } }
    }

    internal class MultiplicationToken : OperatorToken
    {
        public override int Precedence { get { return 2; } }
        public override Func<double, double, double> Func { get { return (a, b) => a * b; } }
    }

    internal class DivisionToken : OperatorToken
    {
        public override int Precedence { get { return 2; } }
        public override Func<double, double, double> Func { get { return (a, b) => b != 0? a / b : throw new ParserException("Dvision by Zero detected."); } }
    }

    internal class ParenthesisToken : Token
    {

    }

    internal class OpenParenthesisToken : ParenthesisToken
    {
    }

    internal class ClosedParenthesisToken : ParenthesisToken
    {
    }


    internal class NumberConstantToken : Token
    {
        private readonly double _value;

        public NumberConstantToken(double value)
        {
            _value = value;
        }

        public double Value
        {
            get { return _value; }
        }
    }
}
