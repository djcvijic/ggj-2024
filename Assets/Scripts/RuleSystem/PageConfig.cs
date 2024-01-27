using System.Collections.Generic;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class PageConfig
    {
        [JsonProperty] public readonly List<RuleConfig> rules;
        [JsonProperty] public readonly InstructionConfig instruction;
    }
}