using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLL.Tests
{
    [TestClass]
    public class ExpressionEvaluatorTest
    {
        OperatorPrecedenceParser evaluator = new OperatorPrecedenceParser();

        [TestMethod]
        public void EvaluateWithoutBracketsTest()
        {
            IList<Tuple<string, double>> testcases = new List<Tuple<string, double>>
            {
                new Tuple<string, double>("2+3", 5),
                new Tuple<string, double>("32+23", 55),
                new Tuple<string, double>("99-23", 76),
                new Tuple<string, double>("12*12", 144),
                new Tuple<string, double>("164/4", 41),
                new Tuple<string, double>("5-7*7+5", -39),
                new Tuple<string, double>("5-7*7+5*8", -4),
                new Tuple<string, double>("4+5*2", 14),
                new Tuple<string, double>("4+5/2", 6.5),
                new Tuple<string, double>("4+5/2 - 1", 5.5),
                new Tuple<string, double>("5-7*7+5*8+16/4", 0)
            };

            foreach (var testcase in testcases)
            {
                double result = evaluator.Parse(testcase.Item1);
                Assert.AreEqual<double>(testcase.Item2, result);
            }
        }

        [TestMethod]
        public void EvaluateWithWhiteSpaceTest()
        {
            IList<Tuple<string, double>> testcases = new List<Tuple<string, double>>
            {
                new Tuple<string, double>("5 - 7*7 + 5", -39),
                new Tuple<string, double>("5- 7*7 +  5*8", -4),
                new Tuple<string, double>("4 + 5 *2", 14),
                new Tuple<string, double>("4 + 5/2", 6.5),
                new Tuple<string, double>("4 + 5/2 - 1", 5.5),
                new Tuple<string, double>("5- 7*7 + 5*8 + 16/4", 0)
            };

            foreach (var testcase in testcases)
            {
                double result = evaluator.Parse(testcase.Item1);
                Assert.AreEqual<double>(testcase.Item2, result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void InvalidCharactersInExpressionThrowExceptionText()
        {
            string input = "a1+15";
            double result = evaluator.Parse(input);
        }

        [TestMethod]
        public void DecimalPointerNotSupportedThrowExceptionText()
        {
            string input = "4.5+3";
            Exception expectedException = null;
            try
            {
                double result = evaluator.Parse(input);
            }
            catch (ParserException ex)
            {
                expectedException = ex;
            }

            Assert.IsNotNull(expectedException);
            Assert.IsTrue(expectedException.Message.Contains("Decimal numbers not supported."));
        }

        [TestMethod]
        public void IncorrectlyFormedExpressionThrowExceptionText()
        {
            string input = "3++7";
            Exception expectedException = null;
            try
            {
                double result = evaluator.Parse(input);
            }
            catch (ParserException ex)
            {
                expectedException = ex;
            }

            Assert.IsNotNull(expectedException);
            Assert.IsTrue(expectedException.Message.Contains("Expecting Real number instead got"));
        }

        [TestMethod]
        public void DivisionByuZeroThrowExceptionText()
        {
            string input = "3+5-2/0";
            Exception expectedException = null;
            try
            {
                double result = evaluator.Parse(input);
            }
            catch (ParserException ex)
            {
                expectedException = ex;
            }

            Assert.IsNotNull(expectedException);
            Assert.IsTrue(expectedException.Message.Contains("Dvision by Zero detected"));
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void IncorrectlyFormedExpressionWithBracketsThrowExceptionText()
        {
            string input = "3(+7-5)";
            double result = evaluator.Parse(input);
        }        

    }
}
