using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarkAlpine.LiftStatus.Records;
using System;
using System.Collections.Generic;

namespace StarkAlpine.LiftStatus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LiftStatusController : ControllerBase
    {
        private static readonly string[] Lifts = { "Shadow Basin", "Curvey Basin", "McDougall's Chondola", "7th Heaven Express", "Symphony Express", "Peak Express" };

        private readonly ILogger<LiftStatusController> _logger;

        public LiftStatusController(ILogger<LiftStatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Lift> Get()
        {
            var rng = new Random();
            var liftStatusCount = Enum.GetValues<Enums.LiftStatus>().Length;

            foreach (var lift in Lifts)
            {
                yield return new Lift
                {
                    Name = lift,
                    LiftStatus = Enum.GetValues<Enums.LiftStatus>()[rng.Next(liftStatusCount)]
                };
            }
        }
    }
}
