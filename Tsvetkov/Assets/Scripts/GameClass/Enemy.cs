using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//добавляем функцию создания в CreateAssetMenu 
[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string description;
    public string enemyName;
    public Sprite art;
}