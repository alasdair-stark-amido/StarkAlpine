using StarkAlpine.LiftStatus.Business.Models;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Xunit;

namespace StarkAlpine.LiftStatus.Business.Specs.Steps
{
    [Binding]
    public class LiftStatusSteps : TechTalk.SpecFlow.Steps
    {
        private readonly ILiftStatusService _liftStatusService;
        private IEnumerable<Lift> _lifts { get; set; }

        public LiftStatusSteps()
        {
            _liftStatusService = new LiftStatusService();
            _lifts = new List<Lift>();
        }

        [When(@"I ask for a collection of lifts")]
        public void WhenIAskForACollectionOfLifts()
        {
            this._lifts = this._liftStatusService.GetLifts();
        }
        
        [Then(@"a collection of lifts are returned with their current status")]
        public void ThenACollectionOfLiftsAreReturnedWithTheirCurrentStatus()
        {
            Assert.NotEmpty(_lifts);
        }
    }
}
