using System;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class TemporalConfig
    {
        [JsonProperty] public readonly DayOfWeek dayOfWeek;

        public bool IsSatisfied(AudienceData audienceData)
        {
            return audienceData.dayOfWeek == dayOfWeek;
        }
    }
}