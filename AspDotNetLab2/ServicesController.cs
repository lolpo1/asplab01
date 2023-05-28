using AspDotNetLab2.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspDotNetLab2.Controllers
{
    [Route("services")]
    public class ServicesControllerBase : ControllerBase
    {
        private readonly ITimerService _timerService;
        private readonly IRandomService _randomService;
        private readonly IGeneralCounterService _generalCounterService;

        public ServicesControllerBase(
            ITimerService timerService,
            IRandomService randomService,
            IGeneralCounterService generalCounterService)
        {
            _timerService = timerService;
            _randomService = randomService;
            _generalCounterService = generalCounterService;
        }

        [HttpGet("list")]
        public IActionResult GetServicesList()
        {
            var services = new[]
            {
                new { Type = "Transient", Name = "TimerService", Description = "Returns the current date and time." },
                new { Type = "Scoped", Name = "RandomService", Description = "Returns a randomly generated number." },
                new { Type = "Singleton", Name = "GeneralCounterService", Description = "Counts the number of requests made to each URL." },
            };

            return Ok(services);
        }

        [HttpGet("timer")]
        public IActionResult GetTimer()
        {
            return Ok(_timerService.GetCurrentDateTime());
        }

        [HttpGet("random")]
        public IActionResult GetRandom()
        {
            var randomNumber1 = _randomService.GetRandomNumber();
            var randomNumber2 = _randomService.GetRandomNumber();

            return Ok(new { Number1 = randomNumber1, Number2 = randomNumber2 });
        }

        [HttpGet("general-counter")]
        public IActionResult GetGeneralCounter()
        {
            return Ok(_generalCounterService.GetCount(HttpContext.Request.Path.Value));
        }
    }
}
