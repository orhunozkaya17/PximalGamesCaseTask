using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public static UIController instance;


    [Header("Game Start")]
    [SerializeField] GameObject gamestartcursor;
    [SerializeField] RectTransform HandSprite;

    [Header("Finish Scene")]
    [SerializeField] GameObject finalScene;
    [SerializeField] Slider finalSceneSlider;
    [SerializeField] Slider succesSlider;
    [SerializeField] Image finalSceneSliderBackGround;
    [Header("Game Finish Panel")]
    [SerializeField] GameObject gamefinish;
    [SerializeField] CanvasGroup gamePanel;
    [SerializeField] RectTransform GreatText;
    [SerializeField] TextMeshProUGUI BoxCount;
    [SerializeField] GameObject gameoverButton;

    float direction = 1;
  
    bool slideractive = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void Start()
    {
        HandSprite.DOAnchorPosX(-200, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnEnable()
    {
        Events.gameStart += Events_GameStart;
        Events.FinalScene += Events_FinalScene;
        Events.GameOver += Events_GameOver;
    }

   

    private void OnDisable()
    {
        Events.gameStart -= Events_GameStart;
        Events.FinalScene -= Events_FinalScene;
        Events.GameOver -= Events_GameOver;
    }

    void Update()
    {
        if (slideractive == true)
        {
            finalSceneSlider.value += Time.deltaTime * 100f * direction;
            finalSceneSliderBackGround.color = Color.Lerp(Color.red, Color.green, finalSceneSlider.value / 100f);
            
            if (finalSceneSlider.value >= 100 || finalSceneSlider.value <= 0)
            {
                direction *= -1;
            }
        }
    }
    private void Events_FinalScene()
    {
        finalScene.SetActive(true);
        sliderStart();
    }
    public float sliderValue()
    {
        return finalSceneSlider.value;
    }
    public void sliderStop()
    {
        slideractive = false;
    }
    public void sliderStart()
    {
        slideractive = true;
        finalSceneSlider.value = 0;

    }
    public void CloseSlider()
    {
        sliderStop();
        finalScene.SetActive(false);
    }
    private void Events_GameStart()
    {
        gamestartcursor.SetActive(false);
      
    }

   

    private void Events_GameOver()
    {
     
        CloseSlider();
        GameFinishPanel();
    }
    public void succesSliderSet(float value )
    {
        succesSlider.value = value;
    }
    void GameFinishPanel()
    {
        gamefinish.gameObject.SetActive(true);
        BoxCount.SetText(GameManager.instance.boxCount.ToString()+" Boxs");
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1.5f);
        seq.Append(gamePanel.DOFade(1, 2f));
        seq.Append(GreatText.DOAnchorPosY(0, 2f));
        seq.Append(BoxCount.transform.DOScale(1, 2f));
        seq.Append(gameoverButton.transform.DOScale(1, 2f));
    }
}
