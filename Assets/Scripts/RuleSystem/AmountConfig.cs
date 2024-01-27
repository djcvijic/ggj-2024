using System;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class AmountConfig
    {
        [JsonProperty] public readonly AmountType type;
        [JsonProperty] public readonly int number;
        [JsonProperty] public readonly PropertyCheck propertyCount;
        [JsonProperty] public readonly ExpressionConfig expression;

        public int Evaluate(AudienceData audienceData)
        {
            return type switch
            {
                AmountType.Number => number,
                AmountType.PropertyCount => audienceData.cats
                    .FindAll(x => propertyCount.Passes(x.color, x.build, x.age, x.status, x.gender))
                    .Count,
                AmountType.Expression => expression.Evaluate(audienceData),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}