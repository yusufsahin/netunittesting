using System;
using DortIslemNUnit;


namespace DortIslem.Test
{
    [TestFixture]
    public class CalculatorTest
    {
        private Calculator _calculator;


        [SetUp]
        public void Setup()
        { 
            _calculator = new Calculator(); 
        }

        [Test]
        public void Add_WithTwoNumbers_ReturnsCorrectSum()
        {
            int result = _calculator.Add(2, 3);
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Subtract_WithTwoNumbers_ReturnsCorrectDifference()
        {
            int result = _calculator.Subtract(5, 3);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Multiply_WithTwoNumbers_ReturnsCorrectProduct()
        {
            int result = _calculator.Multiply(2, 3);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Divide_WithNonZeroDivider_ReturnsCorrectQuotient()
        {
            int result = _calculator.Divide(6, 3);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => _calculator.Divide(6, 0));
        }
    }
}
