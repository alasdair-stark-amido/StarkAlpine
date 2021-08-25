using StarkAlpine.LiftStatus.Business.Models;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using NodaTime.Extensions;
using NodaTime.Testing;
using NodaTime.Text;
using StarkAlpine.LiftStatus.Business.Configuration;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace StarkAlpine.LiftStatus.Business.Specs.Steps
{
    [Binding]
    public class LiftStatusSteps : TechTalk.SpecFlow.Steps
    {
        private ResortOptions _resortOptions { get; set; }
        private ILiftStatusService _liftStatusService { get; set; }
        private IEnumerable<Lift> _lifts { get; set; }

        public LiftStatusSteps()
        {
            _lifts = new List<Lift>();
        }

        [Given(@"the following resort:")]
        public void GivenTheFollowingResort(Table table)
        {
            _resortOptions = table.CreateInstance<ResortOptions>();
        }

        [Given(@"the current UTC time is (.*)")]
        public void GivenTheCurrentUtcTimeIs(string currentTime)
        {
            // year / month / day do not matter to us at this point as the resort has the same opening and closing times every day
            var year = 2000;
            var month = 1;
            var day = 1;

            var currentTimePattern = LocalTimePattern.CreateWithInvariantCulture("HHmm");
            var localTime = currentTimePattern.Parse(currentTime).Value;

            var instant = Instant.FromUtc(year, month, day, localTime.Hour, localTime.Minute, 0);

            var fakeClock = new FakeClock(instant);
            
            _liftStatusService = new LiftStatusService(fakeClock, _resortOptions);
        }


        [When(@"I ask for a collection of lifts")]
        public void WhenIAskForACollectionOfLifts()
        {
            this._lifts = this._liftStatusService.GetLifts();
        }

        [Then(@"lifts should all be closed")]
        public void ThenLiftsShouldAllBeClosed()
        {
            Assert.True(this._lifts.All(l => l.LiftStatus == Enums.LiftStatus.Closed));
        }

        [Then(@"lifts should not be closed")]
        public void ThenLiftsShouldNotBeClosed()
        {
            Assert.True(this._lifts.All(l => l.LiftStatus != Enums.LiftStatus.Closed));
        }
    }
}
