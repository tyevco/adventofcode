using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Advent.Runner.Triggers
{
    public class CronTrigger
    {
        public CronTrigger(ILogger<CronTrigger> logger)
        {
            Logger = logger;
        }

        public ILogger<CronTrigger> Logger { get; }

        public async Task Execute(Func<Task> action)
        {

        }
    }
}
