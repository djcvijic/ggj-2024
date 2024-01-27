using System;
using System.Collections.Generic;

namespace RuleSystem
{
    public class AudienceData
    {
        public readonly List<CatData> cats;
        public readonly DayOfWeek dayOfWeek;

        public AudienceData(List<CatData> cats, DayOfWeek dayOfWeek)
        {
            this.cats = cats;
            this.dayOfWeek = dayOfWeek;
        }
    }
}