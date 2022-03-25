using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Base ")]
    [SerializeField] float speedX = 0.5f;
    [SerializeField] float speedZ = 0.5f;
    [SerializeField] float maxSwerveAmount = 3f;
    [SerializeField] float maxOffsetX = 3f;
    [Header("Player local Move")]
    [SerializeField] Transform _playerVisualTr;
    [SerializeField] Transform _palletVisual;
    [SerializeField] float maxMovePosition;
    [SerializeField] float maxTurnRotation;
    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;

    private float maxdistance = 0f;

    private Rigidbody rb;
    private InputHandler swerveInput;

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

  

    void Start()
    {

        swerveInput = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();

    }



    void Update()

    {
        if (GameManager.instance.state == GameState.Playing)
        {
            var currentMoveAmount = Mathf.Lerp(0, maxMovePosition, Mathf.Abs(swerveInput.MoveFactor));
            var currentRotateAmount = Mathf.Lerp(0, maxTurnRotation, Mathf.Abs(swerveInput.MoveFactor));
            if (swerveInput.MoveFactor > 0)
            {
                currentMoveAmount *= -1;
                currentRotateAmount *= -1;
            }

            //  currentPostion Amount smoothly
            _playerVisualTr.localPosition = Vector3.Lerp(_playerVisualTr.localPosition, new Vector3(currentMoveAmount, _playerVisualTr.localPosition.y, _playerVisualTr.localPosition.z), Time.deltaTime * moveSpeed);

            //rotate y axis by currentRotateAMount

            _playerVisualTr.rotation = Quaternion.Lerp(_playerVisualTr.rotation, Quaternion.Euler(0, 180 - currentRotateAmount, 0), Time.deltaTime * rotationSpeed);

        }
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.state == GameState.Playing)
        {
            float swerveAmount = Mathf.Clamp(swerveInput.MoveFactor, -maxSwerveAmount, maxSwerveAmount);


            float a = swerveAmount * speedX * Time.fixedDeltaTime;
            // rb.velocity = velo;
            //min max 
            if (maxdistance + a >= maxOffsetX)
            {
                swerveAmount = 0;
            }

            else if (maxdistance + a <= -maxOffsetX)
            {
                swerveAmount = 0;
            }
            else
            {
                maxdistance += a;
            }

            Vector3 velo = transform.right * swerveAmount * speedX;
            rb.velocity = -velo;
            rb.velocity += transform.forward * -speedZ;
        }
    }

    private void Events_FinalScene()
    {
        rb.velocity = Vector3.zero;
        _playerVisualTr.transform.localPosition = new Vector3(0.4719999f, _playerVisualTr.transform.localPosition.y, 3.549999f);
        _palletVisual.transform.DORotate(new Vector3(0,-90, 0), 1f);
    }

    private void Events_GameOver()
    {
        rb.velocity = Vector3.zero;
        _playerVisualTr.transform.localPosition = new Vector3(0.4719999f, _playerVisualTr.transform.localPosition.y, 3.549999f);
        _palletVisual.transform.DORotate(new Vector3(0, -90, 0), 1f);
    }
}
