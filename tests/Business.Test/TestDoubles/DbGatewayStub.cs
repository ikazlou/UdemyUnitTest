using Business;
using Business.TestDouble.Testable;
using System;

namespace Business2.Test.TestDoubles
{
    class DbGatewayStub : IDbGateway
    {
        private WorkingStatistics _ws;

        public bool Connected => throw new NotImplementedException();

        public WorkingStatistics GetWorkingStatistics(int id)
        {
            return _ws;
        }

        public void SetWorkingStatistic(WorkingStatistics workingStatistics)
        {
            _ws = workingStatistics;
        }
    }
}
