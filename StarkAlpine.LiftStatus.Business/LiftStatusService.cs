using StarkAlpine.LiftStatus.Business.Models;
using System;
using System.Collections.Generic;

namespace StarkAlpine.LiftStatus.Business
{
    public class LiftStatusService : ILiftStatusService
    {
        private static readonly string[] Lifts = { "Shadow Basin", "Curvey Basin", "McDougall's Chondola", "7th Heaven Express", "Symphony Express", "Peak Express", "Rannoch Button Tow" };

        public IEnumerable<Lift> GetLifts()
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
