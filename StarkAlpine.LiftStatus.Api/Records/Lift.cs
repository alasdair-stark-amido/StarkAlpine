namespace StarkAlpine.LiftStatus.Api.Records
{
    public record Lift
    {
        public string Name { get; init; }
        public Business.Enums.LiftStatus LiftStatus { get; init; }
    }
}
