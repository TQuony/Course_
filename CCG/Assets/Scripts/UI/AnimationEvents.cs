using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvents: MonoBehaviour {
    public void Start_()
    {
        Managers.gameManager.nameScene = "Battle";
        SceneManager.LoadScene("Load");
    }
    public void StopAnim()
    {
        transform.GetComponent<Animator>().enabled = false;
    }
    public void StartReward()
    {
        transform.GetComponent<Animator>().SetBool("Reward_", true);
    }
}
