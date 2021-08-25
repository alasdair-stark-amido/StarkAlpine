using NodaTime;
using NodaTime.Text;

namespace StarkAlpine.LiftStatus.Business.Extensions
{
    public static class StringExtensions
    {
        public static LocalTime ParseLocalTime(this string formattedString)
        {
            var timePattern = LocalTimePattern.CreateWithInvariantCulture("HHmm");

            var parsedLocalTime = timePattern.Parse(formattedString);

            return parsedLocalTime.GetValueOrThrow();
        }
    }
}
