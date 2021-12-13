using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task5;

namespace UnitTestProjectTask5
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PlusTest()
        {
            Calc calc = new Calc();
            decimal result = calc.Calculate("5+5+5+5");
            Assert.AreEqual(20, result, "Plus not working");
        }

        [TestMethod]
        public void MinusTest()
        {
            Calc calc = new Calc();
            decimal result = calc.Calculate("5-5-5-5");
            Assert.AreEqual(-10, result, "Minus not working");
            result = calc.Calculate("-5");
            Assert.AreEqual(-5, result, "Minus not working");
        }


        [TestMethod]
        public void MinusAndPlusTest()
        {
            Calc calc = new Calc();
            decimal result = calc.Calculate("5+5-5+5");
            Assert.AreEqual(10, result, "Minus and plus not working together");
        }

        [TestMethod]
        public void MultiplyTest()
        {
            Calc calc = new Calc();
            decimal result = calc.Calculate("5*5");
            Assert.AreEqual(25, result, "Multiply not working");
        }

        [TestMethod]
        public void DivideTest()
        {
            Calc calc = new Calc();
            decimal result = calc.Calculate("5/5");
            Assert.AreEqual(1, result, "Divide not working");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Divide by zero check failed")]
        public void DivideByZeroTest()
        {
            Calc calc = new Calc();
            decimal result = calc.Calculate("5/0");
        }

        [TestMethod]
        public void AllTogether()
        {
            Calc calc = new Calc();
            decimal result = calc.Calculate("5+5*2");
            Assert.AreEqual(15, result);

            calc = new Calc();
            result = calc.Calculate("5/5+5*5");
            Assert.AreEqual(26, result);

            calc = new Calc();
            result = calc.Calculate("5-5*5");
            Assert.AreEqual(-20, result);
        }

        [TestMethod]
        public void BracketsTest()
        {
            Calc calc = new Calc();
            decimal result = calc.Calculate("(5+5)*2");
            Assert.AreEqual(20, result);

            calc = new Calc();
            result = calc.Calculate("(((5+5)*(5/5))*5)");
            Assert.AreEqual(50, result);
        }

    }
}
