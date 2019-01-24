using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneControlle : MonoBehaviour {
    [SerializeField] private GameObject prefBall;
    [SerializeField] private GameObject prefPlatform;
    [SerializeField] private GameObject Popup;

    [SerializeField] private Image Progress;
    [SerializeField] private GameObject HUD;
    [SerializeField] private Button  button1;
    [SerializeField] private Button button2;
    [SerializeField] private Image[] hearts;
    private bool pause;
    private int countHeart;
    private void Awake()
    {
        Popup.SetActive(false);
        Messenger.AddListener(GameEvent.COUNT, setProgress);
        Messenger.AddListener(GameEvent.START, setStart);
        Messenger.AddListener(GameEvent.PAUSE, setPause);
        Messenger.AddListener(GameEvent.RESTART, reStart);
        Messenger.AddListener(GameEvent.MINUSHE_HEART, heart); 
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.COUNT, setProgress);
        Messenger.RemoveListener(GameEvent.START, setStart);
        Messenger.RemoveListener(GameEvent.PAUSE, setPause);
        Messenger.RemoveListener(GameEvent.RESTART, reStart);
        Messenger.RemoveListener(GameEvent.MINUSHE_HEART, heart);
    }
    void Start () {
        countHeart = 3;
        pause = false;
        Managers.gameManager.ball = prefBall;
        Managers.gameManager.platform = prefPlatform;
        HUD.GetComponent<Animator>().SetBool("startGame", true);
    }
    private void reStart()
    {
        Popup.SetActive(false);
        countHeart = 3;
        pause = false;
        hearts[0].fillAmount = 1;
        hearts[1].fillAmount = 1;
        hearts[2].fillAmount = 1;
        Managers.gameManager.restarPoolBall();
        Progress.fillAmount = 0;
        Time.timeScale = 1;
    }
    private void setProgress()
    {
        float f = (float)(Managers.gameManager.count) / (float)(Managers.gameManager.progress);
        if (f == 1)
            Progress.fillAmount = 0;
        else
            Progress.fillAmount = f;
    }
    private void setStart()
    {
        pause = false;
        HUD.GetComponent<Animator>().SetBool("start", true);
        HUD.GetComponent<Animator>().SetBool("pause", false);
    }
    private void setPause()
    {
        HUD.GetComponent<Animator>().SetBool("pause", false);
        HUD.GetComponent<Animator>().SetBool("pause", true);
        pause = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            if (!pause)
            {
                button1.OnPointerClick(new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current));
            }
        if(countHeart == 0)
        {
            Popup.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void heart()
    {
        
        for (int i = 0; i < 3; i++)
        {
            if (hearts[i].fillAmount == 1)
            {
                hearts[i].fillAmount = 0;
                break;
            }
        }
        countHeart--;
    }
}