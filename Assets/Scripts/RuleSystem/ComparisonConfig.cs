using Newtonsoft.Json;

namespace RuleSystem
{
    public class ComparisonConfig
    {
        [JsonProperty] public readonly AmountConfig amount1;
        [JsonProperty] public readonly ComparisonOperation operation;
        [JsonProperty] public readonly AmountConfig amount2;
    }
}