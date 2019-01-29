using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//добавляем функцию создания в CreateAssetMenu 
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string description;
    public string cardName;
    public Sprite art;
    public int damage;
    public Sprite face;//лицевая сторона
    public string manaName;
    public int mana;
}