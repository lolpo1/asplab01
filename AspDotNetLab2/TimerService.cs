using System;

namespace AspDotNetLab2.Services
{
    public interface ITimerService
    {
        DateTime GetCurrentDateTime();
    }

    public class TimerService : ITimerService
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
