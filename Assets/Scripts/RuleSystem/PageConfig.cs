using System.Collections.Generic;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class PageConfig
    {
        [JsonProperty] public readonly List<RuleConfig> rules;
        [JsonProperty] public readonly InstructionConfig elseInstruction;

        public int GetCorrectJoke(AudienceData audienceData)
        {
            var firstSatisfiedRule = rules.Find(x => x.IsSatisfied(audienceData));
            return firstSatisfiedRule != null
                ? firstSatisfiedRule.instruction.jokeNumber
                : elseInstruction.jokeNumber;
        }
    }
}