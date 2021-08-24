namespace StarkAlpine.LiftStatus.Api.Records
{
    public record Lift
    {
        public string Name { get; init; }
        public Enums.LiftStatus LiftStatus { get; init; }
    }
}
