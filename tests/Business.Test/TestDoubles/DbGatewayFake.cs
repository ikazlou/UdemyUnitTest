using Business;
using Business.TestDouble.Testable;
using System;
using System.Collections.Generic;

namespace Business2.Test.TestDoubles
{
    public class DbGatewayFake : IDbGateway
    {
        public bool Connected => throw new NotImplementedException();

        public int Id { get; private set; }
        public WorkingStatistics _ws { get; private set; }

        public Dictionary<int, WorkingStatistics> _storage = new Dictionary<int, WorkingStatistics>()
        {
            {1, new WorkingStatistics() { HourSalary = 5, PayHourly = true, WorkingHours = 10 } },
            {2, new WorkingStatistics() { PayHourly = false, MonthSalary = 500 } },
            {3, new WorkingStatistics() { HourSalary = 8, PayHourly = true, WorkingHours = 100 } },

        };

        public WorkingStatistics GetWorkingStatistics(int id)
        {
            return _storage[id];
        }
    }
}
