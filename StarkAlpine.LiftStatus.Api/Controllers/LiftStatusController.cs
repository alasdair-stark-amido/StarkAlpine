using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarkAlpine.LiftStatus.Api.Records;
using StarkAlpine.LiftStatus.Business;
using System.Collections.Generic;

namespace StarkAlpine.LiftStatus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LiftStatusController : ControllerBase
    {
        private readonly ILogger<LiftStatusController> _logger;
        private readonly IMapper _mapper;
        private readonly ILiftStatusService _liftStatusService;

        public LiftStatusController(
            ILogger<LiftStatusController> logger,
            IMapper mapper,
            ILiftStatusService liftStatusService)
        {
            _logger = logger;
            _mapper = mapper;
            _liftStatusService = liftStatusService;
        }

        [HttpGet]
        public IEnumerable<Lift> Get()
        {
            return _mapper.Map<IEnumerable<Lift>>(_liftStatusService.GetLifts());
        }
    }
}
