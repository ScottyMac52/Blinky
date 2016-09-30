using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blinky;

namespace TestMorsePatternGeneratorTests
{
    [TestClass]
    public class MorseCodeTester
    {
        private readonly MorsePatternGenerator _patternGenerator = new MorsePatternGenerator();

        [TestMethod]
        public void AssertThatSosIsCorrectInMap()
        {
            // ARRANGE
            var expectedResult = @"...|---|...|";
            
            // ACT 
            var actualResult = _patternGenerator.GetCodeFromString("SOS");

            // ASSERT
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void AssertThatNumbersAreCorrectInMap()
        {
            //ARRANGE
            var expectedResult = @"-----|.----|..---|...--|....-|.....|-....|--...|---..|----.|\";

            // ACT
            var actualResult = _patternGenerator.GetCodeFromString("0123456789 ");

            //ASSERT
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
