using NodaTime;
using NodaTime.Extensions;
using StarkAlpine.LiftStatus.Business.Configuration;
using StarkAlpine.LiftStatus.Business.Extensions;
using StarkAlpine.LiftStatus.Business.Models;
using System;
using System.Collections.Generic;

namespace StarkAlpine.LiftStatus.Business
{
    public class LiftStatusService : ILiftStatusService
    {
        private static readonly string[] Lifts = { "Shadow Basin", "Curvey Basin", "McDougall's Chondola", "7th Heaven Express", "Symphony Express", "Peak Express", "Rannoch Button Tow" };

        private readonly IClock _clock;
        private readonly ResortOptions _resortOptions;

        public LiftStatusService(
            IClock clock,
            ResortOptions resortOptions)
        {
            _clock = clock;
            _resortOptions = resortOptions;
        }

        public IEnumerable<Lift> GetLifts()
        {
            // Parse the opening and closing time of the resort from the options
            var openingTime = _resortOptions.OpeningTime.ParseLocalTime();
            var closingTime = _resortOptions.ClosingTime.ParseLocalTime();

            // Get the current time in the time zone of the resort
            var zone = DateTimeZoneProviders.Tzdb[_resortOptions.TimeZone];
            var currentTimeInResortTimeZone = _clock.InZone(zone).GetCurrentTimeOfDay();

            var rng = new Random();
            var liftStatusCount = Enum.GetValues<Enums.LiftStatus>().Length;

            foreach (var lift in Lifts)
            {
                // If the resort is open then lift status is set to a random value other than closed
                yield return new Lift
                {
                    Name = lift,
                    LiftStatus = currentTimeInResortTimeZone >= openingTime && currentTimeInResortTimeZone < closingTime ? 
                        Enum.GetValues<Enums.LiftStatus>()[rng.Next(liftStatusCount - 1) + 1] : 
                        Enums.LiftStatus.Closed
                };
            }
        }
    }
}
