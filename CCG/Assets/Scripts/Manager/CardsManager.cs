using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.Linq;

public class CardsManager : MonoBehaviour, IGameManager//Наследуем класс и реализуем интерфейс.
{
    public ManagerStatus status { get; private set; }
    public GameObject cardPrefab       { get;  set; }
    public GameObject enemyCardPrefab  { get; set; }
    public GameObject deckPrefab       { get; set; }
    public EditPath playerPath         { get; set; }
    public EditPath enemyPath          { get; set; }
    public EditPath enemyPathHand      { get; set; }

    public Card[] allPlayerCards { get; set; }          //коллекция карт Player
    private Card[] allEnemyCards;                       //коллекция карт Enemy
    private Vector3 pointDeck = new Vector3(-865f, -380f, 0f); //точка размещения деки
    private Vector3 pointEnemyDeck = new Vector3(900f, -240f, 0f); //точка размещения деки Enemy
    public Card[] deckPlayer { get; private set; }
    private Card[] deckEnemy;
    private GameObject[] deckPlayerInTable;             //колода Player из prefabs 
    private GameObject[] deckEnemyInTable;              //колода Enemy из prefabs 

    public Vector3[] arrayPos { get; private set; }
    public int Pos; 

    private int  countCard;                             //переменная, количество отданных карт игроку из колоды 
    private int  sizeDeck;                              //текущий размер колоды
    private int  countEnemyCard;                        //переменная, количество отданных карт игроку из колоды 
    private int  sizeEnemyDeck;                         //текущий размер колоды
    private bool movedCard;                             //одна из карт в движении
    public void Startup()
    {

        status = ManagerStatus.Started;
    }
    public void LoadCards()
    {
        allPlayerCards = Resources.LoadAll<Card>("CardData/Player/");
        allEnemyCards  = Resources.LoadAll<Card>("CardData/Enemy/");
    }
    public void postCard()
    {
        LoadCards();
        deckPlayer = new Card[0];               //Инициализируем пустой список элементов колоды
        deckEnemy = new Card[0];               //Инициализируем пустой список элементов колоды                                  
        countCard = -1;
        countEnemyCard = -1;
        deckPlayer = createDeck(Managers.Player.deckPlayer, true);
        deckEnemy = createDeck(Managers.Enemy.deckEnemy, false);
        deckPlayerInTable = postDeck(deckPlayer, true);            //разместим колоду на поле Table
        deckEnemyInTable = postDeck(deckEnemy, false);
        sizeDeck = deckPlayer.Length;
        sizeEnemyDeck = deckEnemy.Length;
    }
    public void postMapDeck()
    {
        LoadCards();
        deckPlayer = new Card[0];               //Инициализируем пустой список элементов колоды                        
        deckPlayer = createDeck(Managers.Player.deckPlayer, true);
    }
    IEnumerator AddCardPlayer(int count)      //раздать карты игроку (количество)
    {
        slotInHand(count);
        int i = 0;
        while ( i < count)
        {
            if (countCard == sizeDeck - 1)
            {
                yield return StartCoroutine("restartPlayerDeck");    //ждем выполнения корутины перестановки колоды
            }
            else
            {
                Pos = i;
                countCard++;
                int size = sizeDeck - countCard - 1;
                deckPrefab.GetComponent<setTxt>().CountCard(size.ToString());
                CardMove cardMove = deckPlayerInTable[countCard].GetComponent<CardMove>();
                cardMove.Speed = 2000.0f;    //придадим карте скорости
                cardMove.StartPoint = 0;     //вернем стартовую точку
                movedCard = true;
                yield return cardMove.StartCoroutine("cardMoved");      //ждем выполнения корутины перемещения карты
                i++;
                movedCard = false;
            }
        }
    }
    public void slotInHand(int count)               //расчитаем смещение по X для карт в Hand
    {
        if(count %2 ==0)    //делится без остатка
        {
            arrayPos = new Vector3[count];
            Vector3 startPos = new Vector3 ( 0f, 0f, 0f );
            var f =  (-60f * (count / 2)) -30f;
            startPos.x = f;
            int i = 0;
            for (int j = 0; j < count; j++)
            {
                startPos.x += 60f;
                Debug.Log("i ="+i+ "startPos =" + startPos);
                arrayPos[j] = startPos;
                i++;
            }
        }
    }
    IEnumerator enemyCardsPlay()
    {
        int i = 0;
        while (i < deckEnemyInTable.Length)
        {
            if (deckEnemyInTable[i].transform.parent.gameObject.name == "enemyHand")
            {//если карта в руке Enemy - сыграем ее
                EnemePlayCard cardMove = deckEnemyInTable[i].GetComponent<EnemePlayCard>();
                cardMove.Speed = 2000.0f;    //придадим карте скорости
                cardMove.StartPoint = 0;     //вернем стартовую точку
                movedCard = true;
                yield return cardMove.StartCoroutine("cardMoved");      //ждем выполнения корутины перемещения карты
                movedCard = false;
            }
            i++;
        }
        yield return Managers.gameManager.StartCoroutine("NextTurn");
    }
    IEnumerator AddEnemyCard(int count)
    {
        int i = 0;
        while (i < count)
        {
            if (countEnemyCard == sizeEnemyDeck - 1)
            {
                yield return StartCoroutine("restartEnemyDeck");    //ждем выполнения корутины перестановки колоды
            }
            else
            {
                countEnemyCard++;
                EnemePlayCard playCard = deckEnemyInTable[countEnemyCard].GetComponent<EnemePlayCard>();
                playCard.Speed = 1000.0f;
                playCard.StartPoint = 0;     //вернем стартовую точку
                movedCard = true;
                yield return playCard.StartCoroutine("cardInEnemyHand");      //ждем выполнения корутины перемещения карты
                i++;
                movedCard = false;
            }
        }
    }

    private Card[] createDeck(string[] listCard, bool flag)     //создать колоду игрока (не визуально)
    {
        Card[]deck = new Card[0];
        Card[] allPCards = new Card[0];
        Array.Resize(ref deck, listCard.Length);
        if (flag)
            allPCards = allPlayerCards; 
        else
            allPCards = allEnemyCards;
        for (int i=0; i < listCard.Length; i++)
            for(int j=0; j < allPCards.Length;j++)
            {
                if (listCard[i] == allPCards[j].cardName)
                {
                    deck[i] = allPCards[j];
                    break;
                }
            }
        return deck;
    }
    private GameObject[] postDeck(Card[] cards, bool flag) //разместить колоду (визуально)
    {
        GameObject[] deck = new GameObject[cards.Length];
        for (int i = cards.Length - 1; i >= 0; i--) //расставляем  обьекты (карты)
        {
            if (flag)
            {
                deck[i] = Instantiate(cardPrefab);// Ссылка на контейнер для исходной карты или ее копий.
                deck[i].transform.SetParent(Managers.gameManager.Table.transform);
                deck[i].transform.localPosition = pointDeck;
            }
            else { 
                deck[i] = Instantiate(enemyCardPrefab);// Ссылка на контейнер для исходной карты или ее копий.
                deck[i].transform.SetParent(Managers.gameManager.Table.transform);
                deck[i].transform.localPosition = pointEnemyDeck;
            }
            CardDisplay display = deck[i].GetComponent<CardDisplay>();//Возвращает компонент типа type, если он прикреплен к игровому объекту
            display.CardSetup(cards[i]);  
        }
        deck = ShuffleDeck(deck);                            //перетасуем колоду
        if (flag)
        {
            //deckInTable = Instantiate(deckPrefab);               //рубашку на сцене поместим
            //deckInTable.transform.SetParent(Managers.gameManager.Table.transform);
            //deckInTable.transform.localPosition = pointDeck;
            deckPrefab.transform.SetAsLastSibling();
            deckPrefab.GetComponent<setTxt>().CountCard(sizeDeck.ToString());
        }
        return deck;
    }
    IEnumerator restartPlayerDeck()         //когда карты закончились в колоде, разместить ее по новой
    {
        GameObject[] newArray = new GameObject[0];
        int len = 0;
        for (int i = 0; i < deckPlayerInTable.Length; i++)
        {
            if (deckPlayerInTable[i].transform.parent.gameObject.name == "ActivateZone")
            {
                Array.Resize(ref newArray, newArray.Length + 1);
                newArray[len] = deckPlayerInTable[i];
                len++;
            }
        }
        newArray = ShuffleDeck(newArray);  //перетасуем колоду
        SizeDeck = newArray.Length;        //обновим количество карт в колоде ( на столе)  
        for (int i = 0; i < deckPlayerInTable.Length; i++)
        {
            if (deckPlayerInTable[i].transform.parent.gameObject.name == "Hand")
            {
                Array.Resize(ref newArray, newArray.Length + 1);
                newArray[len] = deckPlayerInTable[i];
                len++;
            }
        }
        deckPlayerInTable = newArray;
        for (int i = 0; i < SizeDeck; i++)
        {
            deckPlayerInTable[i].transform.SetParent(Managers.gameManager.Table.transform);
            deckPlayerInTable[i].transform.localPosition = pointDeck;
            deckPlayerInTable[i].gameObject.GetComponent<Dissolve>().reMaterial();  //отменим распыление карты, применим материал по умолчани.
        }
        //deckInTable.transform.SetParent(Managers.gameManager.Hand.transform);
        // deckInTable.transform.SetParent(Managers.gameManager.Table.transform);
        //deckInTable.transform.localPosition = pointDeck;
        deckPrefab.transform.SetAsLastSibling();
        countCard = -1;                  //счетчик карты на раздачу в ноль
        yield return null;
    }
    IEnumerator restartEnemyDeck()         //когда карты закончились в колоде, разместить ее по новой
    {
        GameObject[] newArray = new GameObject[0];
        int len = 0;
        for (int i = 0; i < deckEnemyInTable.Length; i++)
        {
            if (deckEnemyInTable[i].transform.parent.gameObject.name == "ActivateZone")
            {
                Array.Resize(ref newArray, newArray.Length + 1);
                newArray[len] = deckEnemyInTable[i];
                len++;
            }
        }
        newArray = ShuffleDeck(newArray);  //перетасуем колоду
        sizeEnemyDeck = newArray.Length;        //обновим количество карт в колоде ( на столе)  
        for (int i = 0; i < deckEnemyInTable.Length; i++)
        {
            if (deckEnemyInTable[i].transform.parent.gameObject.name == "enemyHand")
            {
                Array.Resize(ref newArray, newArray.Length + 1);
                newArray[len] = deckEnemyInTable[i];
                len++;
            }
        }
        deckEnemyInTable = newArray;
        for (int i = 0; i < sizeEnemyDeck; i++)
        {
            deckEnemyInTable[i].transform.SetParent(Managers.gameManager.Table.transform);
            deckEnemyInTable[i].transform.localPosition = pointEnemyDeck;
            Vector3 endScale = new Vector3(0f, 0f, 0);
            deckEnemyInTable[i].gameObject.GetComponent<EnemePlayCard>().cardBack.gameObject.SetActive(true);
            deckEnemyInTable[i].gameObject.GetComponent<EnemePlayCard>().cardFront.gameObject.SetActive(false);
            deckEnemyInTable[i].gameObject.GetComponent<Dissolve>().reMaterial();  //отменим распыление карты, применим материал по умолчани.
            deckEnemyInTable[i].transform.localScale = endScale;
        }
        countEnemyCard = -1;                  //счетчик карты на раздачу в ноль
        yield return null;
    }

    public void highlightCard()
    {
        Debug.Log("outline");
        for (int i = 0; i < deckPlayerInTable.Length; i++)
        {
            Debug.Log("outline");
            if (deckEnemyInTable[i].transform.parent.gameObject.name == "Hand")
            {
               
                deckEnemyInTable[i].gameObject.GetComponent<Outline>().StartOutline();
            }
        }
    }
    private GameObject[] ShuffleDeck(GameObject[] cards)// Реализация алгоритма тасования Кнута.
    {
        GameObject[] newArray = cards.Clone() as GameObject[];
        for (int i = 0; i < newArray.Length; i++)
        {
            GameObject tmp = newArray[i];
            int r = UnityEngine.Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }
    public int countAddCard           //установить значение переменной countCard    
    {
        set
        {
            countCard = value;
        }
        get
        {
            return countCard;
        }
    }
    public int countAddEnemyCard         //установить значение переменной countCard    
    {
        set
        {
            countEnemyCard = value;
        }
        get
        {
            return countEnemyCard;
        }
    }
    public int SizeDeck
    {
        get
        {
            return sizeDeck;
        }
        set
        {
            sizeDeck = value;
        }
    }
    public bool Moved           //установить значение переменной move    
    {
        get
        {
            return movedCard;
        }
    }
}
