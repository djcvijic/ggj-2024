namespace RuleSystem
{
    public class RuleText
    {
        public readonly string rule;
        public readonly InstructionText instruction;

        public RuleText(string rule, InstructionText instruction)
        {
            this.rule = rule;
            this.instruction = instruction;
        }
    }
}