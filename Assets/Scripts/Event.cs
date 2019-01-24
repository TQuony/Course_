using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour {

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void setStartPosistion()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<Animator>().SetBool("dissolve", false);
        gameObject.transform.position = new Vector3(0f, 8f, 0f);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);        //обнулим скорость
    }
}
