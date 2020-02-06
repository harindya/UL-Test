using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class OperatorPrecedenceParser : IParser
    {
        private string expression;
        private Queue<Token> tokenQueue;
        private int currentOpPrecedence = 0;

        public double Parse(string expression)
        {
            this.expression = expression;
            tokenQueue = new ExpressionTokenizer().Parse(expression, false);
            currentOpPrecedence = 0;
            return Parse();
        }

        public double Parse()
        {
            var lhs = EvaluateExpression();
            while (NextIsHigherPrecedence())
            {
                var op = GetOperator();
                var rhs = Parse();
                var opToken = op as OperatorToken;
                if (opToken != null)
                {
                    lhs = opToken.Func(lhs, rhs);
                    currentOpPrecedence = opToken.Precedence;
                }
            }

            return lhs;
        }

        private double EvaluateExpression()
        {
            if (NextIsDigit())
            {
                return GetNumber();
            }

            throw new ParserException($"Expecting Real number instead got : {GetNextTokenString()} ");            
        }

        private string GetNextTokenString()
        {
            var peekValue = tokenQueue.Count > 0 ? tokenQueue.Peek() : null;
            return peekValue != null ? peekValue.ToString() : "End of expression";
        }

        private Token Peek()
        {
            return tokenQueue.Count > 0 ? tokenQueue.Peek() : null;
        }

        private Token GetOperator()
        {
            var token = tokenQueue.Dequeue();
            if (token != null)
            {
                var opToken = token as OperatorToken;
                if (opToken != null)
                {
                    currentOpPrecedence = opToken.Precedence;
                    return token;
                }
            }

            throw new ParserException($"Expected an operator but found {token}");
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

        private bool NextIsHigherPrecedence()
        {
            var peek = Peek();
            if (peek != null)
            {
                var opToken = peek as OperatorToken;
                if (opToken != null && opToken.Precedence >= currentOpPrecedence)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
