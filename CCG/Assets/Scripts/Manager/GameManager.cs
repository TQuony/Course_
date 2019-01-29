using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager//Наследуем класс и реализуем интерфейс.
{
    public ManagerStatus status { get; private set; }

    public GameObject Table      { get;  set; }
    public GameObject Hand       { get;  set; }    //Обьект сцены, "рука" игрока
    public GameObject enemyHand  { get;  set; }
    public GameObject ActiveZone { get;  set; }    //Обьект сцены, зона активации
    public GameObject txtDamage  { get;  set; }    //префаб вылетающего текста (урон, хил)  
    private moveTxtDamage[] txtPoolDamage = new moveTxtDamage[10];//пул обьектов из префаба txtDamage
    public GameClass[] allClasses { get; set; }       //коллекция классов
    public Enemy[] allEnemy { get; set; }             //коллекция врагов
    public string NameClass { get; set; }             //имя класса
    public int    NumberClass { get;  set; }          //номер класса
    public bool reward { get; set; }                  //флаг, получени награды
    public Item ItemEqupped { get; set; }             //объект экипировки для показа
    public int IDEqupped { get; set; }                //
    private int   countPoolDamage = 0;
    public string nameScene { get; set; }
    public bool fSeeDeck  { get;  set; }            
    public void   Startup()
    {

        nameScene = "Menu";
        fSeeDeck = false;
        LoadClass();
        status = ManagerStatus.Started;
    }
    IEnumerator NextTurn()                          //смена ходов
    {

            if (Managers.Player.playertTurn)
            {
                Managers.Player.playertTurn = false;
                yield return Managers.cardManager.StartCoroutine("AddEnemyCard", 2);
                Managers.cardManager.StartCoroutine("enemyCardsPlay");
            }
            else
            if (Managers.Player.health > 0)
            {
                Managers.Player.playertTurn = true;
                Managers.Player.manaSTR = Managers.Player.maxManaSTR;
                Managers.Player.manaINT= Managers.Player.maxManaINT;
                Managers.Player.manaAGI = Managers.Player.maxManaAGI;
                Messenger.Broadcast(GameEvent.SET_MANA);            //установить значение манапула
                Managers.cardManager.StartCoroutine("AddCardPlayer", 4);
                Managers.cardManager.highlightCard();
            }

    }
    public void postObjects()
    {
        for (int i = 0; i < txtPoolDamage.Length; i++)
        {
            txtPoolDamage[i] = Instantiate(txtDamage, Table.transform).GetComponent<moveTxtDamage>();
            txtPoolDamage[i].gameObject.SetActive(false);
        }
    }
    public void changeHpPlayer(int hp)
    {
        Managers.Player.ChangeHealth(hp);
        txtPoolDamage[countPoolDamage].gameObject.SetActive(true);
        txtPoolDamage[countPoolDamage].startMove(hp);
        countPoolDamage = countPoolDamage == txtPoolDamage.Length - 1 ? 0 : countPoolDamage + 1;
    }
    public void changeHpEnemy(int hp)
    {
        Managers.Enemy.ChangeHealth(hp);
        txtPoolDamage[countPoolDamage].gameObject.SetActive(true);
        txtPoolDamage[countPoolDamage].startMove(hp);
        countPoolDamage = countPoolDamage == txtPoolDamage.Length - 1 ? 0 : countPoolDamage + 1;
    }
    public void LoadClass()
    {
        allClasses = Resources.LoadAll<GameClass>("ClassData/");
        allEnemy = Resources.LoadAll<Enemy>("EnemyData/");
    }
}
