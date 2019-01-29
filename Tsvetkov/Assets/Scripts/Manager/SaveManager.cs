using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class SaveManager : MonoBehaviour, IGameManager//Наследуем класс и реализуем интерфейс.
{
    public ManagerStatus status { get; private set; }
    public Button btnContinue { get; set; }
    private Save sv = new Save();
    public void Startup()
    {
        status = ManagerStatus.Started;
    }
    public void CheckLoad()
    {
        if (!PlayerPrefs.HasKey("Save"))
            btnContinue.interactable = false;
        else
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
            if (sv.nameClass != "")
            {
                Managers.gameManager.NumberClass = sv.numClass;
                Managers.gameManager.NameClass = sv.nameClass;
                Managers.Player.health = sv.hpPlayer;
                Managers.Player.maxHealth = sv.maxHpPlayer;
                Managers.Player.deckPlayer = sv.deckPlayer;
                Managers.Player.money = sv.money;
                Managers.Inventory.inventoryPlayer = sv.invPlayer;
                Managers.Inventory.equippedPlayer = sv.equippedPlayer;
                Managers.Player.setMana();
                int k = 0;
                int[,] array = new int[(sv.levelArray.Length/ sv.lenArray1), sv.lenArray1];
                for (int i = 0; i < (sv.levelArray.Length / sv.lenArray1); i++)
                    for(int j = 0; j < sv.lenArray1; j++)
                    {
                        array[i,j] = sv.levelArray[k];
                        k++;
                    }
                Managers.Mission.level = array;
                Managers.Mission.curLevel = sv.curLevel;        //передаем текущий этап
                Managers.Mission.wayLevel = sv.wayLevel;        //и сохраненный путь
                btnContinue.interactable = true;
            }
        }
    }
    public void CheckClass(string name, int num)
    {
        sv.nameClass = name;
        sv.numClass = num;
    }
    public void CheckLevel(int[,] arr)              //сохраняем уровень
    {
        int len_arr = arr.GetLength(0) * arr.GetLength(1);
        int[] arr_save = new int[len_arr];
        int k = 0;
        for (int j = 0; j < arr.GetLength(0); j++)
        {
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                arr_save[k] = (arr[j, i]);
                k++;
            }

        }
        Debug.Log("Save level");
        sv.lenArray1 = arr.GetLength(1);    //длина
        sv.levelArray = arr_save;
    }
    public void CheckHpPlayer(int hp, int maxhp)
    {
        sv.hpPlayer = hp;   //путь игрока 
        sv.maxHpPlayer = maxhp;
    }
    public void CheckMoney(int mon)
    {
        sv.money = mon;   //капитал игрока
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }
    public void CheclCurLevel(int curLvl)
    {
        sv.curLevel = curLvl;
    }
    public void WayLevel(int[] curLvl)
    {
        sv.wayLevel = curLvl;
    }
    public void CheckDeck(string[] deck)        //сохраняем колоду игрока
    {
        sv.deckPlayer = deck;
    }
    public void CheckInventory(string[] inv)    //сохраняем инвентарь игрока
    {
        sv.invPlayer = inv;
    }
    public void CheckEqupped(string[] equipped)    //сохраняем инвентарь игрока
    {
        sv.equippedPlayer = equipped;
    }
#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv));
    }
#endif
    public void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv));
    }
    [Serializable]
    public class Save
    {
        public string nameClass;  //название класса
        public int numClass;      //номер класса
        public int lenArray1;     //длина массива
        public int[] levelArray;  //массив уровня
        public int curLevel;      //текущий этап уровня
        public int[] wayLevel;    //путь игрока 
        public int hpPlayer;      //текущее хп игрока 
        public int maxHpPlayer;   //максимальное хп игрока 
        public int money;         //капитал игрока 
        public string[] deckPlayer;//колода игрока
        public string[] invPlayer;//инвентарь игрока
        public string[] equippedPlayer;//одетые карты игрока
    }
}