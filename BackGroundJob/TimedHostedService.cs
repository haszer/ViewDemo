﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication1.BackGroundJob
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;

        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            DateTime date=DateTime.Now;
            if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)  //如果当前时间是10点30分
                Console.WriteLine("Time is" + " " +date);
            Console.WriteLine("No_" + Guid.NewGuid() + ".Done At"+" "+date);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            _logger.LogInformation("Timed Background Service is stopping.");

            
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
