using Newtonsoft.Json;

namespace RuleSystem
{
    public class AmountConfig
    {
        [JsonProperty] public readonly AmountType type;
        [JsonProperty] public readonly int number;
        [JsonProperty] public readonly PropertyCheck propertyCount;
        [JsonProperty] public readonly ExpressionConfig expression;
    }
}