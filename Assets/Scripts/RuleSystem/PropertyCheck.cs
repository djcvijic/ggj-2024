using System;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class PropertyCheck
    {
        [JsonProperty] public readonly CatProperty property;
        [JsonProperty] public readonly CatPropertyValue value;
        [JsonProperty] public readonly PropertyCheck secondaryCheck;

        public bool Passes(CatColor color, CatBuild build, CatAge age, CatStatus status, CatGender gender)
        {
            return MainCheck(color, build, age, status, gender)
                   && (secondaryCheck == null || secondaryCheck.MainCheck(color, build, age, status, gender));
        }

        private bool MainCheck(CatColor color, CatBuild build, CatAge age, CatStatus status, CatGender gender)
        {
            return property switch
            {
                CatProperty.Color => CheckColor(color),
                CatProperty.Build => CheckBuild(build),
                CatProperty.Age => CheckAge(age),
                CatProperty.Status => CheckStatus(status),
                CatProperty.Gender => CheckGender(gender),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool CheckColor(CatColor color)
        {
            return color switch
            {
                CatColor.Black => value == CatPropertyValue.Black,
                CatColor.White => value == CatPropertyValue.White,
                CatColor.Orange => value == CatPropertyValue.Orange,
                CatColor.Tabby => value == CatPropertyValue.Tabby,
                CatColor.Calico => value == CatPropertyValue.Calico,
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
            };
        }

        private bool CheckBuild(CatBuild build)
        {
            return build switch
            {
                CatBuild.Fat => value == CatPropertyValue.Fat,
                CatBuild.Skinny => value == CatPropertyValue.Skinny,
                CatBuild.Muscular => value == CatPropertyValue.Muscular,
                _ => throw new ArgumentOutOfRangeException(nameof(build), build, null)
            };
        }

        private bool CheckAge(CatAge age)
        {
            return age switch
            {
                CatAge.Boomer => value == CatPropertyValue.Boomer,
                CatAge.Millennial => value == CatPropertyValue.Millennial,
                CatAge.Zoomer => value == CatPropertyValue.Zoomer,
                _ => throw new ArgumentOutOfRangeException(nameof(age), age, null)
            };
        }

        private bool CheckStatus(CatStatus status)
        {
            return status switch
            {
                CatStatus.Inside => value == CatPropertyValue.Inside,
                CatStatus.Outside => value == CatPropertyValue.Outside,
                CatStatus.Stray => value == CatPropertyValue.Stray,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }

        private bool CheckGender(CatGender gender)
        {
            return gender switch
            {
                CatGender.Male => value == CatPropertyValue.Male,
                CatGender.Female => value == CatPropertyValue.Female,
                CatGender.Schrodinger => value == CatPropertyValue.Schrodinger,
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };
        }
    }
}