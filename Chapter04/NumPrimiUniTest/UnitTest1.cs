using NumeriPrimiLib;
using Xunit;

namespace NumPrimiUniTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test40()
        {
            //arrange
            int a = 40;
            
            string expected = "5 x 2 x 2 x 2";

            Primi calc = new();

            //act
            string actual = calc.GeneraPrimi(a);

            //assert 
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Test99()
        {
            //arrange
            int a = 99;

            string expected = "11 x 3 x 3";

            Primi calc = new();

            //act
            string actual = calc.GeneraPrimi(a);

            //assert 
            Assert.Equal(expected, actual);

        }
    }
}