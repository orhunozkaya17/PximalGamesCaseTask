using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class Events
{
    public static event Action gameStart;

    public static event Action FinalScene;
    public static event Action GameOver;
    public static void CallGameStart()
    {
        gameStart?.Invoke();
    }
 
    public static void CallFinalScene()
    {
        FinalScene?.Invoke();
    }
    public static void CallGameOver()
    {
        GameOver?.Invoke();
    }
}
