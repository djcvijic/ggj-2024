using System.Collections.Generic;

namespace RuleSystem
{
    public class Page
    {
        public readonly List<Rule> rules;
        public readonly InstructionConfig elseInstruction;

        public Page(List<Rule> rules, InstructionConfig elseInstruction)
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