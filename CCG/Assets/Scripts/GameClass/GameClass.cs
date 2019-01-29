using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//добавляем функцию создания в CreateAssetMenu 
[CreateAssetMenu(fileName = "New Class", menuName = "GameClass")]
public class GameClass : ScriptableObject
{
    public string description;      //описание
    public string className;        
    public string classNameDesc;    //для вывода
    public Sprite art;              //изображение
    public int strength;            //атрибут - сила
    public int intelligence;        //атрибут - интелект
    public int agility;
    public int hp;                  //начальное здоровье
    public int money;               //начальные деньги
    public string[] classDeck;      //стартовая колода
    public string[] classInventory; //стартовый инвентарь
}