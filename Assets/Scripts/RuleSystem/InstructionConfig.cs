using System;
using Newtonsoft.Json;

namespace RuleSystem
{
    public class InstructionConfig
    {
        [JsonProperty] public readonly InstructionType type;
        [JsonProperty] public readonly int jokeNumber;

        public string Text
        {
            get
            {
                return type switch
                {
                    InstructionType.TellJoke => $"Tell joke {jokeNumber}",
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}