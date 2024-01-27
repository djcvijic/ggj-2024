using Newtonsoft.Json;

namespace RuleSystem
{
    public class PropertyCheck
    {
        [JsonProperty] public readonly CatProperty property;
        [JsonProperty] public readonly CatPropertyValue value;
        [JsonProperty] public readonly CatProperty extraProperty;
        [JsonProperty] public readonly CatPropertyValue extraValue;
    }
}