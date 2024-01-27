using System.Collections.Generic;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class RuleBookConfig
    {
        [JsonProperty] public readonly List<PageConfig> pages;
    }
}