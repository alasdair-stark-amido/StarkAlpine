using AutoMapper;

namespace StarkAlpine.LiftStatus.Api.Profiles
{
    public class LiftProfile : Profile
    {
        public LiftProfile()
        {
            CreateMap<Business.Models.Lift, Records.Lift>();
        }
    }
}
