using System;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class ComparisonConfig
    {
        [JsonProperty] public readonly AmountConfig amount1;
        [JsonProperty] public readonly ComparisonOperation operation;
        [JsonProperty] public readonly AmountConfig amount2;

        public bool IsSatisfied(AudienceData audienceData)
        {
            return operation switch
            {
                ComparisonOperation.GreaterThan =>
                    amount1.Evaluate(audienceData) > amount2.Evaluate(audienceData),
                ComparisonOperation.GreaterThanOrEqual =>
                    amount1.Evaluate(audienceData) >= amount2.Evaluate(audienceData),
                ComparisonOperation.LessThan =>
                    amount1.Evaluate(audienceData) < amount2.Evaluate(audienceData),
                ComparisonOperation.LessThanOrEqual =>
                    amount1.Evaluate(audienceData) <= amount2.Evaluate(audienceData),
                ComparisonOperation.Equal =>
                    amount1.Evaluate(audienceData) == amount2.Evaluate(audienceData),
                ComparisonOperation.NotEqual =>
                    amount1.Evaluate(audienceData) != amount2.Evaluate(audienceData),
                _ =>
                    throw new ArgumentOutOfRangeException()
            };
        }
    }
}