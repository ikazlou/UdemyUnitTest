using System;
using System.IO;

namespace Business.TestDouble.Untestable
{
    // Проблема написания тестов для такого рода класса заключается в том
    // что он может получать "внешние ресурсы" которые мы не знаем как себя ведут
    // и они могут получать данные из разных источников например баз данных 
    // которые во время выполнения тестов могут быть недоступны и тесту нас получатся
    // будут временно недоступны. Для использования дублеров необходимо отрефокторить код и 
    // исползовать иныверсию зависимостей те реализация классов или подлючение их через
    // соответствующие интерфейсы
    public class Customer
    {
        private readonly IDbGateway _dbGateway;
        private readonly ILogger _logger;

        public IDbGateway DbGateway { get; set; }

        public ILogger Logger { get; set; }

        // Сделать инверисию зависимостей можно двумя способоми
        // 1 - через конструктор
        // 2 - через публичные свойства
        public Customer(IDbGateway dbGateway, ILogger logger)
        {
            _dbGateway = dbGateway;
            _logger = logger;
        }

        public decimal CalculateWage(int id)
        {
            WorkingStatistics ws = _dbGateway.GetWorkingStatistics(id);

            decimal wage;
            if (ws.PayHourly)
            {
                wage = ws.WorkingHours * ws.HourSalary;
            }
            else
            {
                wage = ws.MonthSalary;
            }
            _logger.Info($"Customer ID={id}, Wage:{wage}");

            return wage;
        }
    }

    internal class Logger : ILogger
    {
        public void Info(string s)
        {
            File.WriteAllText(@"C:\tmp:\log.txt", s);
        }
    }

    public class DbGateway : IDbGateway
    {
        public bool Connected { get; }

        public WorkingStatistics GetWorkingStatistics(int id)
        {
            //a real gateway can experience any possible problems
            //like "no connection" throwing an exception
            throw new NoConnection();
        }
    }

    public class NoConnection : Exception
    {
    }
}
