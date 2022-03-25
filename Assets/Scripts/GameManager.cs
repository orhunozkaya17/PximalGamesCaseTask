using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    [HideInInspector] public int boxCount=0;
    private void Awake()
    {
        state = GameState.Start;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Events.gameStart+= Events_gameStarted;
        Events.FinalScene += Events_FinalScene;
        Events.GameOver +=Events_GameOver ;
    }
    private void OnDisable()
    {
        Events.gameStart -= Events_gameStarted;
        Events.FinalScene -= Events_FinalScene;
        Events.GameOver -= Events_GameOver;
    }

   

    private void Events_gameStarted()
    {
        state = GameState.Playing;
    }
    private void Events_FinalScene()
    {
        state = GameState.FinishScene;
    }
    private void Events_GameOver()
    {

        state = GameState.GameOver;
    }
}
