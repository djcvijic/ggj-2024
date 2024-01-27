using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace RuleSystem
{
    public class RuleBook : Singleton<RuleBook>
    {
        private RuleBookConfig _config;
        private List<CatData> _audienceData;

        private RuleBookConfig Config => _config ??=
            JsonConvert.DeserializeObject<RuleBookConfig>(Resources.Load<TextAsset>("ruleBook").text);

        /// <summary>
        /// Call with data about each new audience at the start of each day.
        /// </summary>
        /// <param name="audienceData"></param>
        public void Initialize(List<CatData> audienceData)
        {
            _audienceData = audienceData;
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
        /// Get info about the page [1..14] to display in the rule book interface.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public PageText GetPageText(int pageNumber)
        {
            var pageConfig = Config.pages[pageNumber - 1];
            return new PageText(
                pageConfig.rules.ConvertAll(x => new RuleText(x.text, new InstructionText(x.instruction.Text))),
                new InstructionText(pageConfig.instruction.Text));
        }
    }
}