using System.Collections.Generic;

namespace RuleSystem
{
    public class PageText
    {
        public readonly List<RuleText> rules;
        public readonly InstructionText elseInstruction;

        public PageText(List<RuleText> rules, InstructionText elseInstruction)
        {
            this.rules = rules;
            this.elseInstruction = elseInstruction;
        }
    }
}