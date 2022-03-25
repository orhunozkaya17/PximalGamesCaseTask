using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator ChracterAnim;
    [SerializeField] private Animator RigAnimator;

    #region ANIMATOR PARAMETERS
    // Animator parameters - Player
    public static int Move = Animator.StringToHash("Move");
    public static int Dance = Animator.StringToHash("Dance");
    public static int FinalScene = Animator.StringToHash("FinalScene");
    //kick
    public static int Kick1 = Animator.StringToHash("Kick1");
    public static int Kick2 = Animator.StringToHash("Kick2");
    public static int Kick3 = Animator.StringToHash("Kick3");
    // Animator parameters - Rig
    public static int HandClose = Animator.StringToHash("HandClose");
    public static int HandOpen = Animator.StringToHash("HandOpen");
    #endregion
    float currentKick = 0;
    bool isKick = false;
    StackController stackController;
    private void Awake()
    {
        stackController = GetComponent<StackController>();
    }
    private void OnEnable()
    {
        Events.gameStart += Events_gameStarted;
        Events.FinalScene += Events_FinalScene;
        Events.GameOver += Events_GameOver;
    }


    private void OnDisable()
    {
        Events.gameStart -= Events_gameStarted;
        Events.FinalScene -= Events_FinalScene;
        Events.GameOver -= Events_GameOver;
    }

  
    private void Events_gameStarted()
    {
        ChracterAnim.SetTrigger(Move);
        RigAnimator.SetTrigger(HandOpen);

    }
    private void Events_FinalScene()
    {
        RigAnimator.SetTrigger(HandClose);
        ChracterAnim.SetTrigger(FinalScene);
    }

    public void Kick()
    {
        if (isKick)
        {
            return;
        }
        if (stackController.endStack())
        {
            GameManager.instance.state = GameState.GameOver;
        }
        isKick = true;
       

        if (currentKick % 3 == 0)
        {
            ChracterAnim.SetTrigger(Kick1);
        }
        else if (currentKick % 3 == 1)
        {
            ChracterAnim.SetTrigger(Kick2);
        }
        else if (currentKick % 3 == 2)
        {
            ChracterAnim.SetTrigger(Kick3);
        }
        currentKick++;
        UIController.instance.sliderStop();
    }
    public void gameFinished()
    {
        ChracterAnim.SetTrigger(Dance);
    }
    public void KickFinish()
    {
        isKick = false;
       
    }
    private void Events_GameOver()
    {
        RigAnimator.SetTrigger(HandClose);
        ChracterAnim.SetTrigger(Dance);
    }

}
