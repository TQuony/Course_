using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager//Наследуем класс и реализуем интерфейс.
{
    public ManagerStatus status { get; private set; }
    public string[] inventoryPlayer { get; set; }          //инвентарь игрока
    public string[] equippedPlayer { get; set; }           //одетые вещи
    public Item[] allItem { get; set; }                    //коллекция предметов

    public void Startup()
    {
        LoadItems();
        status = ManagerStatus.Started;
    }
    public void AddItem(string item)                        //добавим имя новго итема в инвентарь
    {
        int len = inventoryPlayer.Length + 1;
        string[] inv = new string[len];                  //инвентарь игрока +1 новый предмет
        int i = 0;
        foreach (string fVar in inventoryPlayer)
        {
            inv[i] = fVar;
            i++;
        }
        inv[i] = item;
        inventoryPlayer = inv;
    }
    public void LoadItems()
    {
        allItem = Resources.LoadAll<Item>("CardData/Item/");
    }
    public string GetRandomItem()
    {
        int r = UnityEngine.Random.Range(0, allItem.Length) + 1;
        return allItem[r - 1].cardName;
    }
    public void DeleteItem(int index)                        //удалим по индексу итем
    {
        var output = new string[inventoryPlayer.Length - 1];
        int counter = 0;

        for (int i = 0; i < inventoryPlayer.Length; i++)
        {
            if (i == index) continue;
            output[counter] = inventoryPlayer[i];
            counter++;
        }
        inventoryPlayer = output;
    }
    public void DeleteEqupped(int index)                        //очистим слот экипировки
    {
        equippedPlayer[index] = "";
    }
    public void AddEqupped(int index, string name)            //добавим по индексу имя экипированной карты
    {
        equippedPlayer[index] = name;
    }
}

//    void AddItem(int itemID, string desc, string name, Sprite art, Sprite face)