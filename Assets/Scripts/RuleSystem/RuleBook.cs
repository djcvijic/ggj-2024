using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace RuleSystem
{
    public class RuleBook : Singleton<RuleBook>
    {
        private RuleBookConfig _config;
        private AudienceData _audienceData;

        private RuleBookConfig Config => _config ??=
            JsonConvert.DeserializeObject<RuleBookConfig>(Resources.Load<TextAsset>("ruleBook").text);

        public int Seed { get; set; }

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
        public int GetCorrectJoke(int seed)
        {
            var randomizedPages = RandomizeRules(seed);
            var currentPageNumber = _audienceData.cats.Count;
            var currentPage = randomizedPages[currentPageNumber-1];
            return currentPage.GetCorrectJoke(_audienceData);
        }

        public int GetAudienceCount()
        {
            return _audienceData.cats.Count;
        }

        /// <summary>
        /// Get info about the page [1..14] to display in the rule book interface.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public PageText GetPageText(int pageNumber)
        {
            var pageConfig = Config.pages[pageNumber - 1];
            return pageConfig.GetPageText();
        }

        public List<PageConfig> RandomizeRules(int seed)
        {
            List<PageConfig> newPages = new List<PageConfig>();
            List<List<ConditionConfig>> conditionConfigs = new List<List<ConditionConfig>>();
            List<string> conditionTexts = new List<string>();
            List<InstructionConfig> instructions = new List<InstructionConfig>();
            List<InstructionConfig> finalInstructions = new List<InstructionConfig>();

            for (int i = 0; i < 14; i++)
            {
                var pageConfig = Config.pages[i];
                var pageRules = pageConfig.rules;
                var pageConditions = pageRules.ConvertAll(x => x.conditions);
                var pageConditionTexts = pageRules.ConvertAll(x => x.text);
                var pageInstructions = pageRules.ConvertAll(x => x.instruction);
                var finalInstruction = pageConfig.elseInstruction;

                conditionConfigs.AddRange(pageConditions);
                conditionTexts.AddRange(pageConditionTexts);
                instructions.AddRange(pageInstructions);
                finalInstructions.Add(finalInstruction);
            }

            List<RuleConfig> newRules = new List<RuleConfig>();
            for (int i = 0; i < 56; i++)
            {
                Random.seed = seed;

                int RandomIndexForCondition = Random.Range(0, conditionConfigs.Count);
                var newRuleConditionConfigs = conditionConfigs[RandomIndexForCondition];
                var newRuleConditionText = conditionTexts[RandomIndexForCondition];
                conditionConfigs.RemoveAt(RandomIndexForCondition);
                conditionTexts.RemoveAt(RandomIndexForCondition);

                Random.seed = seed;

                int RandomIndexForInstruction = Random.Range(0, instructions.Count);
                var newRuleInstruction = instructions[RandomIndexForInstruction];
                instructions.RemoveAt(RandomIndexForInstruction);

                var newRuleConfig = new RuleConfig(newRuleConditionText, newRuleConditionConfigs, newRuleInstruction);
                newRules.Add(newRuleConfig);
            }

            for (int i = 0; i < 14; i++)
            {
                var newPage = new PageConfig(newRules.GetRange(i * 4, 4), finalInstructions[i]);
                newPages.Add(newPage);
            }

            return newPages;
        }
    }
}