using HelloWorldApp;


namespace HelloWorld.Test
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void GetHelloWorld_ReturnsCorrectString()
        {
            var expected = "Hello, World!";
            var result = Program.GetHelloWorld();

            Assert.AreEqual(expected, result);
        }
    }
}
