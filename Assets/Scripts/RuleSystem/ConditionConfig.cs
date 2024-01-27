using Newtonsoft.Json;

namespace RuleSystem
{
    public class ConditionConfig
    {
        [JsonProperty] public readonly ConditionType type;
        [JsonProperty] public readonly ComparisonConfig comparison;
        [JsonProperty] public readonly PositionalConfig positional;
        [JsonProperty] public readonly TemporalConfig temporal;
    }
}