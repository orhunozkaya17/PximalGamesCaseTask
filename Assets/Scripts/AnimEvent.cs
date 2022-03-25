using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    [SerializeField] Transform kickpos;
    FinalScene finalScene;
    AnimatorHandler animatorHandler;
    private void Awake()
    {
        finalScene = GetComponentInParent<FinalScene>();
        animatorHandler = GetComponentInParent<AnimatorHandler>();
       
    }

    public void Kick()
    {
        SoundManager.instance.PlayBoxPauch(transform.position);
        ParticalEffectManager.instance.HitEffect(kickpos.position);
        if (GameManager.instance.state==GameState.GameOver)
        {
            finalScene.StackGo();
            Events.CallGameOver();
           
        }
        else
        {
            finalScene.StackGo();
            animatorHandler.KickFinish();
            UIController.instance.sliderStart();
        }
       
    }
}
