using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    // Use this for initialization
    private Vector3 gForce;   //вектор гравитации
    private Rigidbody rb;
   
    private float k;   //k = 0 - абсолютно неупругое столкновение (мяч прилипнет) 
                       //k = 1 - абсолютно упругое(будет бесконечно прыгать)
    private Vector3 elasticity;     //сила, с которой шарик отскочит от поверхности платформы
    void Start()
    {
        k = 1f;
        gForce= new Vector3(0, -9.81f, 0); 
        elasticity = new Vector3(0, 0, 0);        
        rb = gameObject.GetComponent<Rigidbody>();

    }
    void Update()
    {
        if (transform.position.y < -4)
        {
            Messenger.Broadcast(GameEvent.MINUSHE_HEART);
            gameObject.SetActive(false);
            gameObject.GetComponent<Animator>().SetBool("dissolve", false);
            Vector3 v3 = new Vector3(0f, 8f, 0f);
            gameObject.transform.position = v3;
        }

    }
    void FixedUpdate()
    {
        Vector3 newVelocity = rb.velocity + gForce * rb.mass * Time.deltaTime;
        rb.velocity = newVelocity;
        elasticity = newVelocity;           //сохраняю текущую скорость шарика
    }

    void OnCollisionEnter(Collision collision)      //при столкновении 
    {
        gameObject.GetComponent<Animator>().SetBool("dissolve", true);

        float y = -k * elasticity.y;
        rb.velocity = new Vector3(rb.velocity.x, y, rb.velocity.z);

        Managers.gameManager.count++;
        Messenger.Broadcast(GameEvent.COUNT);
    }
}