using System.Collections.Generic;

namespace RuleSystem
{
    public class RuleBook : Singleton<RuleBook>
    {
        private List<CatData> audienceData;

        /// <summary>
        /// Call with data about each new audience at the start of each day.
        /// </summary>
        /// <param name="newAudienceData"></param>
        public void Initialize(List<CatData> newAudienceData)
        {
            audienceData = newAudienceData;
        }

        /// <summary>
        /// Check if the joke with the selected number [1..56] is the correct one.
        /// </summary>
        /// <param name="jokeNumber"></param>
        /// <returns></returns>
        public bool IsCorrectJoke(int jokeNumber)
        {
            return false;
        }

        /// <summary>
        /// Get info about the current page to display in the rule book interface.
        /// </summary>
        /// <returns></returns>
        public PageText GetPageText()
        {
            return new PageText(new List<RuleText>
            {
                new("", new InstructionText("")), 
                new("", new InstructionText("")),
                new("", new InstructionText("")),
                new("", new InstructionText(""))
            }, new InstructionText(""));
        }
    }
}