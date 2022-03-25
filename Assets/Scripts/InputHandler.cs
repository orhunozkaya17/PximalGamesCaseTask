using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private float _LastFrameFingerPostionX;
    private float _movefactor;
 

    private bool callstart;
    public float MoveFactor
    {
        get { return _movefactor; }
    }
    private AnimatorHandler animhandler;
    private void Awake()
    {
        animhandler = GetComponent<AnimatorHandler>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            _LastFrameFingerPostionX = Input.mousePosition.x;
            if (GameManager.instance.state==GameState.FinishScene )
            {
                animhandler.Kick();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            _movefactor = Input.mousePosition.x - _LastFrameFingerPostionX;
            _LastFrameFingerPostionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _movefactor = 0f;

        }
        if (GameManager.instance.state == GameState.Start && !callstart)
        {


            if (_movefactor != 0)
            {
                callstart = true;
                Events.CallGameStart();
            }

        }
    }

}
