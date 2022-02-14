using Business.TestDouble.Testable;


namespace Business2.Test.TestDoubles
{
    public class LoggerDummy : ILogger
    {
        public void Info(string s)
        {
            // Это пустышка и в ней нет необходимости осуществлять какую-либо логику
        }
    }
}
