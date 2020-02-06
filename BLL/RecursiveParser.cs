using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RecursiveParser : IParser
    {
        private string expression;
        private Queue<Token> tokenQueue;

        public double Parse(string expression)
        {
            this.expression = expression;
            tokenQueue = new ExpressionTokenizer().Parse(expression, true);
            return ExpressionValue();
        }

        public double ExpressionValue()
        {
            var valueOfExpression = TermValue();
            while (NextIsAdditionOrSubtraction())
            {
                var op = GetTermOpertor();
                var nextTermValue = TermValue();
                var opToken = op as OperatorToken;
                if (opToken != null)
                {
                    valueOfExpression = opToken.Func(valueOfExpression, nextTermValue);
                }
            }

            if (tokenQueue.Count > 0)
            {
                var peek = tokenQueue.Peek();
                if (!(peek is ClosedParenthesisToken))
                {
                    throw new ParserException("Malformed expression.");
                }
            }

            return valueOfExpression;
        }

        private double TermValue()
        {
            var totalVal = FactorValue();
            if (NextIsMultiplicationOrDivision())
            {
                var op = GetFactorOpertor();
                var nextFactor = TermValue();

                var opToken = op as OperatorToken;
                if (opToken != null)
                {
                    totalVal = opToken.Func(totalVal, nextFactor);
                }
            }

            return totalVal;
        }

        private double FactorValue()
        {
            if (NextIsDigit())
            {
                return GetNumber();
            }

            if (!NextIsOpeningBracket())
            {
                throw new ParserException($"Expecting Real number or '(' in expression, instead got : {GetNextTokenString()}");
            }

            tokenQueue.Dequeue();
            var val = ExpressionValue();
            if (!NextIsClosingBracket())
            {
                throw new ParserException($"Expecting ')' in expression, instead got: {GetNextTokenString()}");
            }

            tokenQueue.Dequeue();
            return val;
        }

        private Token Peek()
        {
            return tokenQueue.Count > 0 ? tokenQueue.Peek() : null;
        }

        private string GetNextTokenString()
        {
            var peekValue = tokenQueue.Count > 0 ? tokenQueue.Peek() : null;
            return peekValue != null ? peekValue.ToString() : "End of expression";
        }

        private bool NextIsOpeningBracket()
        {
            var peek = tokenQueue.Count > 0 ? tokenQueue.Peek() : null;
            var op = peek as OpenParenthesisToken;
            if (op != null)
            {
                return true;
            }

            return false;
        }

        private bool NextIsClosingBracket()
        {
            var peek = tokenQueue.Count > 0 ? tokenQueue.Peek() : null;
            var op = peek as ClosedParenthesisToken;
            if (op != null)
            {
                return true;
            }

            return false;
        }

        private Token GetTermOpertor()
        {
            var op = tokenQueue.Dequeue();
            if (op is AdditionToken)
                return op;
            if (op is SubtractionToken)
                return op;

            throw new ParserException($"Expected term operand '+' or '-' but found {op}");
        }

        private Token GetFactorOpertor()
        {
            var op = tokenQueue.Dequeue();
            if (op is DivisionToken)
                return op;
            if (op is MultiplicationToken)
                return op;

            throw new ParserException($"Expected factor operand '/' or '*' but found {op}");
        }

        private double GetNumber()
        {
            var next = tokenQueue.Dequeue();
            if (next != null)
            {
                var nr = next as NumberConstantToken;
                if (nr != null)
                {
                    return nr.Value;
                }
            }

            throw new ParserException($"Expecting Real number but got {next}");
        }        

        private bool NextIsDigit()
        {
            var peek = Peek();
            return peek != null && peek is NumberConstantToken;
        }

        private bool NextIsAdditionOrSubtraction()
        {
            var peek = Peek();
            if (peek != null)
            {
                return peek is AdditionToken || peek is SubtractionToken;
            }

            return false;
        }

        private bool NextIsMultiplicationOrDivision()
        {
            var peek = Peek();
            if (peek != null)
            {
                return peek is MultiplicationToken || peek is DivisionToken;
            }

            return false;
        }
    }
}
