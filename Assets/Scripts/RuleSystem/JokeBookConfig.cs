using Newtonsoft.Json;
using RuleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokeBookConfig
{
    [JsonProperty] public readonly List<JokeConfig> jokes;

    public JokeConfig GetJokeConfig(int index)
    {
        return jokes[index];
    }
}
