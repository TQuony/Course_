using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dissolve : MonoBehaviour
{

    public Material Dissolve_2D;
    public Material Dissolve_Off;
    public Image Image1;
    public Image Image2;
    public Text Text1;
    public Text Text2;

    private bool dissolve;
    private float shininess;
    Vector3 scale_position;

    private Material Dissolve_2D_prefab;
    private float scaleX;
    private float scaleY;
    void Start()
    {
        shininess = 0.05f;
        dissolve = false;
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }
    private void Update()

    {
        if (!dissolve) return;

        //float shininess = Mathf.PingPong(Time.time, 1.02f); //
        //float shininess = (Mathf.Repeat(Time.time, 1.02f));
        shininess = Mathf.Lerp(shininess, 2f, Time.deltaTime * 1f);
        Image1.material.SetFloat("_Threshold", shininess);
        if (shininess > 1.02f)
        {
            shininess = 0.05f;
            dissolve = false;
        }
    }
    public void StartDissolve()
    {
        Dissolve_2D_prefab = Instantiate(Dissolve_2D);// Ссылка на контейнер префаб материала.
        Image1.material = Dissolve_2D_prefab;
        Image2.material = Dissolve_2D_prefab;
        Text1.material = Dissolve_2D_prefab;
        Text2.material = Dissolve_2D_prefab;
        dissolve = true;
    }
    public void reMaterial()
    {
        Image1.material = Dissolve_Off;
        Image2.material = Dissolve_Off;
        Text1.material = Dissolve_Off;
        Text2.material = Dissolve_Off;
        //scale_position.x = 0.6f;
        // scale_position.y = 0.6f;
        scale_position.x = scaleX;
        scale_position.y = scaleY;
        transform.localScale = scale_position;
        dissolve = false;
    }
}