using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinalScene : MonoBehaviour
{


    [Header("Final Scene")]
    [SerializeField] Transform firstBoxPos;
    [SerializeField] float SuscessPercent;
    [SerializeField] Transform[] failPos;
    [Header("Truck")]
    [SerializeField] Transform truck;
    [SerializeField] Transform truckDoorR;
    [SerializeField] Transform truckDoorL;
    [Header("Postion")]
    [SerializeField] float xposDistance;
    [SerializeField] float zposDistance;
    [SerializeField] float horizontalCount;


    StackController stackController;
    InputHandler swerveInput;
    Vector3 currentStackPos;
    Tween truckDoorRTween;
    Tween truckDoorLTween;
    int currentStackCount = 0;

    private void Awake()
    {
        stackController = GetComponent<StackController>();
        swerveInput = GetComponent<InputHandler>();
        currentStackPos = firstBoxPos.position;
        truckDoorRTween = truckDoorR.DORotate(new Vector3(0, -120, 0), 4f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack).SetAutoKill(false);
        truckDoorLTween = truckDoorL.DORotate(new Vector3(0, 120, 0), 4f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack).SetAutoKill(false);
        truckDoorRTween.Pause();
        truckDoorLTween.Pause();
        UIController.instance.succesSliderSet(SuscessPercent);
    }
    private void OnEnable()
    {
        Events.FinalScene += Events_FinalScene;
        Events.GameOver += Events_GameOver;
    }
    private void OnDisable()
    {
        Events.FinalScene -= Events_FinalScene;
        Events.GameOver -= Events_GameOver;
    }



    private void Update()
    {

    }
    private void Events_FinalScene()
    {
        truckDoorRTween.PlayForward();
        truckDoorLTween.PlayForward();
        SoundManager.instance.PlayTruckDoor(truck.position);
    }
    private void Events_GameOver()
    {
        truckDoorRTween.PlayBackwards();
        truckDoorLTween.PlayBackwards();
        
        SoundManager.instance.PlayTruckDoor(truck.position);
        SoundManager.instance.PlayTruckSound(truck.position);
        truck.DOPunchScale(Vector3.one*.01f, 0.5f,2,0.5f).SetLoops(-1,LoopType.Yoyo);

    }
    private void MoveStack(Stack stack)
    {
        if (UIController.instance.sliderValue() >= SuscessPercent)
        {
            GameManager.instance.boxCount++;
            stack.setPartical(true);
            Sequence seq = DOTween.Sequence();
            seq.Append(stack.transform.DOJump(currentStackPos, 1f, 1, 1f));
            seq.Join(stack.transform.DOScale(0.6f, 1f));
            seq.Join(stack.getVisual().DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.WorldAxisAdd));
            seq.Append(truck.DOPunchScale(Vector3.one * 0.1f, 0.1f).SetLoops(1, LoopType.Yoyo));
            seq.OnComplete(() => {ParticalEffectManager.instance.pufEffect(stack.transform.position); stack.setPartical(false);SoundManager.instance.PlayBoxInTruck(stack.transform.position);});
            currentStackPos.x += xposDistance;
            currentStackCount++;
            if (currentStackCount % horizontalCount == 0)
            {
                currentStackPos.z += zposDistance;
                currentStackPos.x = firstBoxPos.position.x;
            }
        }
        else
        {
            stack.setPartical(true);
            Sequence seq = DOTween.Sequence();
            seq.Append(stack.transform.DOMove(failPos[UnityEngine.Random.Range(0, failPos.Length)].position, 0.8f));
            seq.Join(stack.transform.DOScale(0.6f, 0.8f));
            seq.Join(stack.getVisual().DORotate(new Vector3(0f, 360f, 0f), 0.8f, RotateMode.WorldAxisAdd));

            seq.OnComplete(() => { stack.gameObject.SetActive(false); ParticalEffectManager.instance.BrokenEffect(stack.transform.position); stack.setPartical(false); SoundManager.instance.PlayBoxBreak(transform.position); });
        }


    }

    public void StackGo()
    {

        MoveStack(stackController.getStack());
    }

}
