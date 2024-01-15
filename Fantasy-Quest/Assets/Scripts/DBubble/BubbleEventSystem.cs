using System;
using UnityEngine;

public class BubbleEventSystem : MonoBehaviour
{
    public static BubbleEventSystem Instance = null;

    public static Action<bool> OnTriggerBubble;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void TriggerBubble(bool canShow)
    {
        OnTriggerBubble?.Invoke(canShow);
    }
}
