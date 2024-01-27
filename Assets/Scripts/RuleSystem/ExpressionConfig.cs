using System;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class ExpressionConfig
    {
        [JsonProperty] public readonly AmountConfig amount1;
        [JsonProperty] public readonly ExpressionOperation operation;
        [JsonProperty] public readonly AmountConfig amount2;

        public int Evaluate(AudienceData audienceData)
        {
            return operation switch
            {
                ExpressionOperation.Sum => amount1.Evaluate(audienceData) + amount2.Evaluate(audienceData),
                ExpressionOperation.Difference => amount1.Evaluate(audienceData) - amount2.Evaluate(audienceData),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}