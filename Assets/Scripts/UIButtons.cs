using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour {

    public Button btnPause;
    public Text btnText;
    public Text btnText2;

    private bool paused = false;
    private bool start = false;

    public void ToStart()
    {
        if (!start)
        {
            Messenger.Broadcast(GameEvent.START);
            btnPause.interactable = true;
            btnText.text = "RESTART";
            btnText2.text = "UNPAUSE";
            Managers.gameManager.postObjects();
            start = true;
            Managers.gameManager.start = start;
        }
        else
        {
            Messenger.Broadcast(GameEvent.RESTART);
            Time.timeScale = 1;
            Messenger.Broadcast(GameEvent.START);
            paused = false;
        }
    }
    public void ToExit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }
    public void ToPause()
    {
        if (!paused)
        {
            paused = true;
            Messenger.Broadcast(GameEvent.PAUSE);
        }
        else
        { 
            Time.timeScale = 1;
            Messenger.Broadcast(GameEvent.START);
            paused = false;
        }
    }
}
