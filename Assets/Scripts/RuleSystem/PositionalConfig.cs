using Newtonsoft.Json;

namespace RuleSystem
{
    public class PositionalConfig
    {
        [JsonProperty] public readonly int row;
        [JsonProperty] public readonly int cell;
        [JsonProperty] public readonly PropertyCheck propertyCheck;
    }
}