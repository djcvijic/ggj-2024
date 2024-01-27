using Newtonsoft.Json;

namespace RuleSystem
{
    public class InstructionConfig
    {
        [JsonProperty] public readonly InstructionType type;
        [JsonProperty] public readonly int jokeNumber;
    }
}