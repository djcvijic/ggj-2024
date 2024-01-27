using Newtonsoft.Json;
using RuleSystem;
using UnityEngine;

namespace DefaultNamespace
{
    public class JsonTest : MonoBehaviour
    {
        private void Start()
        {
            var ruleExampleJson = Resources.Load<TextAsset>("ruleExample").text;
            var ruleExample = JsonConvert.DeserializeObject<RuleConfig>(ruleExampleJson);
            Debug.Log(ruleExample);
        }
    }
}