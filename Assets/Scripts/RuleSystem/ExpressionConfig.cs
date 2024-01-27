using Newtonsoft.Json;

namespace RuleSystem
{
    public class ExpressionConfig
    {
        [JsonProperty] public readonly AmountConfig amount1;
        [JsonProperty] public readonly ExpressionOperation operation;
        [JsonProperty] public readonly AmountConfig amount2;
    }
}