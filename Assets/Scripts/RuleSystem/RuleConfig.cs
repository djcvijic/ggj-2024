using System.Collections.Generic;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class RuleConfig
    {
        [JsonProperty] public readonly string text;
        [JsonProperty] public readonly List<ConditionConfig> conditions;
        [JsonProperty] public readonly InstructionConfig instruction;

        public RuleConfig(string text, List<ConditionConfig> conditions, InstructionConfig instruction)
        {
            this.text = text;
            this.conditions = conditions;
            this.instruction = instruction;
        }

        public bool IsSatisfied(AudienceData audienceData)
        {
            return conditions.TrueForAll(x => x.IsSatisfied(audienceData));
        }
    }
}