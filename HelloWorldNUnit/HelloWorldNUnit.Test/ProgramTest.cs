using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldNUnit.Test
{
    [TestFixture]
    public class ProgramTest
    {
        [Test]
        public void GetHelloWorld_ReturnsCorrectString()
        {
            var expected = "Hello, World!";

            var result = Program.GetHelloWorld();

            Assert.AreEqual(expected, result);
        }
    }
}
