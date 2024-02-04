using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RuleSystem
{
    public class RuleBook : Singleton<RuleBook>
    {
        public const int PageCount = 14;
        private const int RulesPerPage = 4;

        private RuleBookConfig _config;
        private List<Page> _shuffledPages;
        private AudienceData _audienceData;

        private RuleBookConfig Config => _config ??=
            JsonConvert.DeserializeObject<RuleBookConfig>(Resources.Load<TextAsset>("ruleBook").text);

        /// <summary>
        /// Call with data about each new audience at the start of each day.
        /// </summary>
        /// <param name="audienceData"></param>
        public void Initialize(AudienceData audienceData)
        {
            _audienceData = audienceData;
        }

        /// <summary>
        /// Returns the correct joke [1..70].
        /// </summary>
        /// <returns></returns>
        public int GetCorrectJoke()
        {
            var currentPageNumber = _audienceData.cats.Count;
            var currentPage = _shuffledPages[currentPageNumber - 1];
            return currentPage.GetCorrectJoke(_audienceData);
        }

        /// <summary>
        /// Get info about the page [1..14] to display in the rule book interface.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public PageText GetPageText(int pageNumber)
        {
            var pageConfig = _shuffledPages[pageNumber - 1];
            return pageConfig.GetPageText();
        }

        /// <summary>
        /// Shuffle rules and instructions around based on a seed.
        /// </summary>
        /// <param name="seed"></param>
        public void ShuffleRuleBook(string seed)
        {
            var allRules = Config.pages.SelectMany(x => x.rules).ToList();
            var allTexts = allRules.Select(x => x.text).ToList();
            var allConditions = allRules.Select(x => x.conditions).ToList();
            var allInstructions = allRules.Select(x => x.instruction).ToList();
            allInstructions.AddRange(Config.pages.Select(x => x.elseInstruction).ToList());

            var instructionsSeed = $"-{seed}";
            allTexts.Shuffle(seed);
            allConditions.Shuffle(seed);
            allInstructions.Shuffle(instructionsSeed);

            _shuffledPages = ComposePages(allTexts, allConditions, allInstructions);
        }

        private static List<Page> ComposePages(
            List<string> ruleTexts,
            List<List<ConditionConfig>> conditionLists,
            List<InstructionConfig> instructions)
        {
            var newPages = new List<Page>();
            for (var pageIndex = 0; pageIndex < PageCount; pageIndex++)
            {
                var newRules = new List<Rule>();
                for (var ruleIndex = 0; ruleIndex < RulesPerPage; ruleIndex++)
                {
                    var newText = ruleTexts[pageIndex * RulesPerPage + ruleIndex];
                    var newConditions = conditionLists[pageIndex * RulesPerPage + ruleIndex];
                    var newInstruction = instructions[pageIndex * (RulesPerPage + 1) + ruleIndex];
                    newRules.Add(new Rule(newText, newConditions, newInstruction));
                }

                var newElseInstruction = instructions[pageIndex * (RulesPerPage + 1) + RulesPerPage];
                newPages.Add(new Page(newRules, newElseInstruction));
            }

            return newPages;
        }
    }
}