using Business;
using Business.TestDouble.Testable;
using System;

namespace Business2.Test.TestDoubles
{
    public class DbGatewayMock : IDbGateway
    {
        private WorkingStatistics _ws;

        public bool Connected => throw new NotImplementedException();

        public int Id { get; private set; }

        public WorkingStatistics GetWorkingStatistics(int id)
        {
            Id = id;
            return _ws;
        }

        public void SetWorkingStatistic(WorkingStatistics workingStatistics)
        {
            _ws = workingStatistics;
        }

        public bool VerifyCalledWithProperId(int id)
        {
            return Id == id;
        }

    }
}
