using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DortIslem.Test
{
    [TestClass]
    public class CalculatorTest
    {
        private Calculator _calculator;

        [TestInitialize]
        public void Setup() { 
            _calculator = new Calculator(); 
        
        }

        [TestMethod]
        public void Add_WithTwoNumbers_ReturnsCorrectSum()
        {
            int result = _calculator.Add(5, 6);
            Assert.AreEqual(11, result);
        }

        [TestMethod]
        public void Substract_WithTwoNumbers_ReturnsCorrectDiffirence()
        {
            int result = _calculator.Substract(5, 3);
            Assert.AreEqual(2, result);

        }

        [TestMethod]
        public void Multiply_WithTwoNumbers_ReturnsCorrectProduct()
        {
            int result = _calculator.Multiply(3, 4);
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void Divide_WithTwoNonZeroDevider_ReturnsCorrectQuotient()
        {
            int result = _calculator.Divide(12, 4);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Divide_ByZero_ThrowsDevideByZeroException()
        {
            Assert.ThrowsException<DivideByZeroException>(() => _calculator.Divide(12,0));
        }
    }
}
