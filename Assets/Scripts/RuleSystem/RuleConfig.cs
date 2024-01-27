using System.Collections.Generic;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class RuleConfig
    {
        [JsonProperty] public readonly string text;
        [JsonProperty] public readonly List<ConditionConfig> conditions;
        [JsonProperty] public readonly InstructionConfig instruction;
    }
}