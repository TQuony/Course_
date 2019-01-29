using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, IGameManager
{ //Наследуем класс и реализуем интерфейс.

    public ManagerStatus status { get; private set; }
    public int health           { get;  set; }         //текущее здоровье
    public int maxHealth        { get;  set; }         //макс здоровье
    public int money            { get; set; }          //капитал :)
    public int manaSTR { get; set; }                   //текущая мана STR
    public int maxManaSTR { get; set; }                //макс мана STR
    public int manaINT { get; set; }                   //текущая мана Int
    public int maxManaINT { get; set; }                //макс мана Int
    public int manaAGI { get; set; }                   //текущая мана Agi
    public int maxManaAGI { get; set; }                //макс мана Agi
    public string[] deckPlayer  { get; set; }          //колода игрока
    public GameObject Player    { get;  set; }
    public GameObject PlayerInTable { get; set; }
    private bool playertturn;
    //private List<string> deckPlayer;
    private Vector3 pointPlayer = new Vector3(-500f, 100f, 0f);
    public void Startup()
    {
        status = ManagerStatus.Started;
    }
    public void ChangeHealth(int value)
    { //Другие сценарии не могут напрямую задавать переменную health, но могут вызывать эту функцию.
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
            Messenger.Broadcast(GameEvent.GAME_OVER);
        }
        Messenger.Broadcast(GameEvent.HP_UPDATED); // отправим сообщение об изменении Hp
    }
    public void setMana()
    {
        //устанавливаем стартовые параметры в зависимости от класса
        manaSTR = Managers.gameManager.allClasses[Managers.gameManager.NumberClass].strength;
        maxManaSTR = Managers.gameManager.allClasses[Managers.gameManager.NumberClass].strength;
        manaINT = Managers.gameManager.allClasses[Managers.gameManager.NumberClass].intelligence;
        maxManaINT = Managers.gameManager.allClasses[Managers.gameManager.NumberClass].intelligence;
        manaAGI = Managers.gameManager.allClasses[Managers.gameManager.NumberClass].agility;
        maxManaAGI = Managers.gameManager.allClasses[Managers.gameManager.NumberClass].agility;
        //просмотрим экипировку
        for (int j = 0; j < Managers.Inventory.equippedPlayer.Length; j++)
        {
            for (int k = 0; k < Managers.Inventory.allItem.Length; k++)
            {
                if (Managers.Inventory.allItem[k].cardName == Managers.Inventory.equippedPlayer[j])
                {
                    if (Managers.Inventory.allItem[k].ID == 0) //если статусная карта
                    {
                        switch (Managers.Inventory.allItem[k].statusName)            //Смотрим какой атрибуд повышает
                        {
                            case "str":
                                manaSTR +=Managers.Inventory.allItem[k].value;
                                maxManaSTR+= Managers.Inventory.allItem[k].value;
                                break;
                            case "int":
                                manaINT += Managers.Inventory.allItem[k].value;
                                maxManaINT += Managers.Inventory.allItem[k].value;
                                break;
                            case "agi":
                                manaAGI += Managers.Inventory.allItem[k].value;
                                maxManaAGI += Managers.Inventory.allItem[k].value;
                                break;
                        }
                        break;
                    }
                }
            }
        }
    }
    public void postPlayer()
    {
        //deckPlayer = new List<string>() { "Crash", "Crash", "Crash", "Swing", "Swing", "Swing", "Swing", "Swing", "Heal", "Heal" };
        PlayerInTable = Instantiate(Player);// Ссылка на контейнер для моделки игрока.
        PlayerInTable.transform.SetParent(Managers.gameManager.Table.transform);
        PlayerInTable.transform.localPosition = pointPlayer;
        PlayerInTable.transform.localScale = new Vector3(1f, 1f, 1f);//временно
        DisplayClass display = PlayerInTable.GetComponent<DisplayClass>();//Возвращает компонент типа type, если он прикреплен к игровому объекту
        display.setupBattle(Managers.gameManager.allClasses[Managers.gameManager.NumberClass]);
    }
    public bool playertTurn             
    {
        set
        {
            playertturn = value;
        }
        get
        {
            return playertturn;
        }
    }
    public void ChangeManaStr(int value)
    { //Другие сценарии не могут напрямую задавать переменную health, но могут вызывать эту функцию.
        manaSTR += value;
        Messenger.Broadcast(GameEvent.MANA_STR_UPDATED); // отправим сообщение об изменении Hp
    }
    public void ChangeManaInt(int value)
    { //Другие сценарии не могут напрямую задавать переменную health, но могут вызывать эту функцию.
        manaINT += value;
        Messenger.Broadcast(GameEvent.MANA_INT_UPDATED); // отправим сообщение об изменении Hp
    }
    public void ChangeManaAgi(int value)
    { //Другие сценарии не могут напрямую задавать переменную health, но могут вызывать эту функцию.
        manaAGI += value;
        Messenger.Broadcast(GameEvent.MANA_AGI_UPDATED); // отправим сообщение об изменении Hp
    }
}