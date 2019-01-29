using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outline : MonoBehaviour {

    public Material Outline_2D;
    public Image Image;
    private Material Outline_2D_prefab;

    public void StartOutline()
    {
        Outline_2D_prefab = Instantiate(Outline_2D);// Ссылка на контейнер префаб материала.
        Image.material = Outline_2D_prefab;
        Image.material.SetFloat("_Outline", 1);
}
    public void StopOutline()
    {
        Image.material.SetFloat("_Outline", 0);
    }
}
