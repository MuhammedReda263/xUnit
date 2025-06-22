namespace CRUDTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int x =5; int y = 6;
            int expectedValue = 11;

            MyMath myMath = new MyMath();
            int AcualValue = myMath.Add(x, y);

            Assert.Equal(expectedValue, AcualValue);
        }
    }
}