using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BLL
{
    internal class ExpressionTokenizer
    {
        private StringReader _reader;

        internal Queue<Token> Parse(string expression, bool allowBrackets)
        {
            _reader = new StringReader(expression);

            Queue<Token> tokens = new Queue<Token>();
            while (_reader.Peek() != -1)
            {
                var c = (char)_reader.Peek();
                if (Char.IsWhiteSpace(c))
                {
                    _reader.Read();
                    continue;
                }

                if (Char.IsDigit(c))
                {
                    var nr = ParseNumber();
                    tokens.Enqueue(new NumberConstantToken(nr));
                }
                else if (c == '-')
                {
                    tokens.Enqueue(new SubtractionToken());
                    _reader.Read();
                }
                else if (c == '+')
                {
                    tokens.Enqueue(new AdditionToken());
                    _reader.Read();
                }
                else if (c == '*')
                {
                    tokens.Enqueue(new MultiplicationToken());
                    _reader.Read();
                }
                else if (c == '/')
                {
                    tokens.Enqueue(new DivisionToken());
                    _reader.Read();
                }
                else if (c == '(')
                {
                    if (allowBrackets)
                    {
                        tokens.Enqueue(new OpenParenthesisToken());
                    }
                    else
                    {
                        throw new ParserException($"Unsupported character in expression: {c}");
                    }

                    _reader.Read();
                }
                else if (c == ')')
                {
                    if (allowBrackets)
                    {
                        tokens.Enqueue(new ClosedParenthesisToken());
                    }
                    else
                    {
                        throw new ParserException($"Unsupported character in expression: {c}");
                    }

                    _reader.Read();
                }
                else
                {
                    throw new ParserException($"Unsupported character in expression: {c}");
                }
            }

            return tokens;
        }

        private double ParseNumber()
        {
            var sb = new StringBuilder();
            while (Char.IsDigit((char)_reader.Peek()) || ((char)_reader.Peek() == '.'))
            {
                var digit = (char)_reader.Read();
                if (digit == '.')
                {
                    throw new ParserException("Decimal numbers not supported.");
                }

                sb.Append(digit);
            }

            if (!double.TryParse(sb.ToString(), out double res))
            {
                throw new ParserException($"Could not parse number: {sb}");
            }

            return res;
        }
    }
}
