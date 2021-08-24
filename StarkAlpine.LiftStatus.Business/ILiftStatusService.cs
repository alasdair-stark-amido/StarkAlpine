using StarkAlpine.LiftStatus.Business.Models;
using System.Collections.Generic;

namespace StarkAlpine.LiftStatus.Business
{
    public interface ILiftStatusService
    {
        IEnumerable<Lift> GetLifts();
    }
}
