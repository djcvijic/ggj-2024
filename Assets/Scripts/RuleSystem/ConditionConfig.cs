using System;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class ConditionConfig
    {
        [JsonProperty] public readonly ConditionType type;
        [JsonProperty] public readonly ComparisonConfig comparison;
        [JsonProperty] public readonly PositionalConfig positional;
        [JsonProperty] public readonly TemporalConfig temporal;

        public bool IsSatisfied(AudienceData audienceData)
        {
            return type switch
            {
                ConditionType.Comparison => comparison.IsSatisfied(audienceData),
                ConditionType.Positional => positional.IsSatisfied(audienceData),
                ConditionType.Temporal => temporal.IsSatisfied(audienceData),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}