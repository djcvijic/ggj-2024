using System.Collections.Generic;

namespace RuleSystem
{
    public class Rule
    {
        public readonly string text;
        public readonly List<ConditionConfig> conditions;
        public readonly InstructionConfig instruction;

        public Rule(string text, List<ConditionConfig> conditions, InstructionConfig instruction)
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