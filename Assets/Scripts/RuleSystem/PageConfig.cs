using System.Collections.Generic;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class PageConfig
    {
        [JsonProperty] public readonly List<RuleConfig> rules;
        [JsonProperty] public readonly InstructionConfig elseInstruction;

        public PageConfig(List<RuleConfig> rules, InstructionConfig elseInstruction)
        {
            this.rules = rules;
            this.elseInstruction = elseInstruction;
        }

        public int GetCorrectJoke(AudienceData audienceData)
        {
            var firstSatisfiedRule = rules.Find(x => x.IsSatisfied(audienceData));
            return firstSatisfiedRule != null
                ? firstSatisfiedRule.instruction.jokeNumber
                : elseInstruction.jokeNumber;
        }

        public PageText GetPageText()
        {
            return new PageText(
                               rules.ConvertAll(x => new RuleText(x.text, new InstructionText(x.instruction.Text))),
                                              new InstructionText(elseInstruction.Text));
        }
    }
}