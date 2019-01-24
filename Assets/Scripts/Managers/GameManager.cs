using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager//Наследуем класс и реализуем интерфейс.
{
    public ManagerStatus status { get; private set; }

    public GameObject ball { get;  set; }        //префаб шарика
    public GameObject platform { get;  set; }    //префаб платформы
    public bool start { get; set; }
    public int progress { get; private set; }
    public int count { get;  set; }
    private GameObject gamePlatform;
    private GameObject[] poolBall = new GameObject[25];//пул обьектов из префаба Ball
    private int countBall;
    private float timeS;
    private float mass;
    public void Startup()
    {
        progress = 5;
        count = 0;
        timeS = 2f;
        start = false;
        countBall = 0;
        mass = 0.75f;
        status = ManagerStatus.Started;
    }

    public void postObjects()
    {

        for (int i = 0; i < poolBall.Length; i++)
        {
            poolBall[i] = Instantiate(ball);
            poolBall[i].gameObject.SetActive(false);
        }
        gamePlatform = Instantiate(platform);
        gamePlatform.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (start) 
            StartCoroutine("HailBalls");
        if (count == progress)
        {
            if (timeS != 1.25f)
            {
                count = 0;
                progress += 5;
                timeS -= 0.25f;
            }
            if (mass != 1.5f)
            {
                mass += 0.25f;
            }
        }
    }
    IEnumerator HailBalls()
    {
        start = false;
        while (!start)
        {
            int randomX = UnityEngine.Random.Range(-7, 6);
            if (countBall == 25)
                countBall = 0;
            Vector3 v3 = new Vector3(randomX, poolBall[countBall].transform.position.y, poolBall[countBall].transform.position.z);
            if (poolBall[countBall].transform.position.y == 8)
            {
                poolBall[countBall].GetComponent<Rigidbody>().mass = mass;
                poolBall[countBall].transform.position = v3;
                poolBall[countBall].SetActive(true);
            }
            countBall++;
            yield return new WaitForSeconds(timeS);
        }
    }
    public void restarPoolBall()
    {
        for (int i = 0; i < poolBall.Length; i++)
        {
            if (poolBall[i].transform.position.y != 8)
            {
                poolBall[i].gameObject.SetActive(false);
                poolBall[i].GetComponent<Animator>().SetBool("dissolve", false);
                poolBall[i].transform.position = new Vector3(0f, 8f, 0f);
                poolBall[i].GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);        //обнулим скорость
                
            }
        }
        gamePlatform.gameObject.transform.position = new Vector3(0f, -3f, 0f);
        progress = 5;
        count = 0;
        timeS = 3;
        countBall = 0;
        mass = 0.75f;
    }
}