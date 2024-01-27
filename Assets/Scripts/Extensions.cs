using UnityEngine.Events;

public static class Extensions
{
    public static void SetListener(this UnityEvent unityEvent, UnityAction action)
    {
        unityEvent.RemoveAllListeners();
        unityEvent.AddListener(action);
    }
}