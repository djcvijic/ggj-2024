using Newtonsoft.Json;

namespace RuleSystem
{
    public class PositionalConfig
    {
        [JsonProperty] public readonly int row;
        [JsonProperty] public readonly int seat;
        [JsonProperty] public readonly PropertyCheck propertyCheck;

        public bool IsSatisfied(AudienceData audienceData)
        {
            var cat = audienceData.cats.Find(x => x.rowNumber == row && x.seatNumber == seat);
            return cat?.PassesPropertyCheck(propertyCheck) ?? false;
        }
    }
}