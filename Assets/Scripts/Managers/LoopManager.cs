using UnityEngine;
using System;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance;
    public int loopCount = 0;
    public int maxLoops = 5;
    public event Action<int> OnLoopChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void NextLoop()
    {
        loopCount++;
        if (loopCount > maxLoops) loopCount = maxLoops;
        Debug.Log("LoopManager: loop = " + loopCount);
        OnLoopChanged?.Invoke(loopCount);
    }

    public void ResetLoops()
    {
        loopCount = 0;
        OnLoopChanged?.Invoke(loopCount);
    }
}
