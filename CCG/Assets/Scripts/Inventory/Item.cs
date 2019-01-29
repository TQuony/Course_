using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//добавляем функцию создания в CreateAssetMenu 
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]

public class Item : ScriptableObject
{

    public int ID;              //0 - статусная карта
    public string statusName;   //какой статус подымает карта Heart, Str и т.д
    public int value;           //и на какое значение

    public string description;
    public string cardName;
    public Sprite art;
    public Sprite face;//лицевая сторона


}
