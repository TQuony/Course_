using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CreateMaps : MonoBehaviour
{

    public RectTransform panel;
    public RectTransform panelDeck;
    public GameObject ScrollPanel;
    public GameObject ScrollMap;
    public GameObject objBoss;
    public GameObject objBattle;
    public GameObject objCamp;

    private int[,] level;
    private Vector3[] point = new Vector3[5];
    private GameObject[] objMap;
    public Text Coins;

    [SerializeField] private GameObject cardPrefab;

    void Start()
    {
        //Managers.Inventory.slotPrefab = slotPrefab;           //передаем обьекты сцене

        Coins.text = Managers.Player.money.ToString();
        Managers.cardManager.postMapDeck();
        createDeck();                                           //создать панель для просмотра колоды
        level = Managers.Mission.level;
        GameObject[,] objMap = new GameObject[level.GetLength(0), level.GetLength(1)];
        point[0] = new Vector3(-1100f, 300f, 0f);
        point[1] = new Vector3(-1100f, 150f, 0f);
        point[2] = new Vector3(-1100f, 0f, 0f);
        point[3] = new Vector3(-1100f, -150f, 0f);
        point[4] = new Vector3(-1100f, -300f, 0f);
        for (int i = 0; i < level.GetLength(0); i++)
        {
            Vector3 p = point[i];
            for (int j = 0; j < level.GetLength(1); j++)
            {
                int caseSwitch = level[i, j];
                switch (caseSwitch)
                {
                    case 0:                 //обьект Camp    
                        GameObject obj0 = Instantiate(objCamp);
                        obj0.transform.SetParent(panel.transform);
                        obj0.transform.localPosition = p;
                        objMap[i, j] = obj0;
                        break;
                    case 2:                 //обьект Battle
                        GameObject obj2 = Instantiate(objBattle);
                        obj2.transform.SetParent(panel.transform);
                        obj2.transform.localPosition = p;
                        objMap[i, j] = obj2;
                        break;
                    case 100:                 //обьект Boss
                        GameObject obj100 = Instantiate(objBoss);
                        obj100.transform.SetParent(panel.transform);
                        obj100.transform.localPosition = p;
                        objMap[i, j] = obj100;
                        break;
                }
                p.x = p.x + 200f;
            }
        }
        int lev = Managers.Mission.curLevel;
        for (int i = 0; i < level.GetLength(0); i++)
        {
            if (level[i, lev] == 2)
                objMap[i, lev].GetComponent<Animator>().SetBool("Flicker_", true);
        }
        for (int i = 0; i < lev; i++)
            objMap[Managers.Mission.wayLevel[i], i].GetComponent<Animator>().SetBool("Set_", true);
        Managers.Mission.objMap = objMap;
        float dist = -1100f + lev * 200f;
        ScrollMap.GetComponent<ScrollRect>().normalizedPosition = new Vector2(dist, 0);
    }

    private void createDeck()
    {
        Card[] allPCards = Managers.cardManager.deckPlayer;
        GameObject[] deck = new GameObject[allPCards.Length];

        int remainder = allPCards.Length % 5;
        int line = allPCards.Length / 5;
        if (remainder > 0)
            line++;
        float y1 = 250f;
        Vector3 p = new Vector3(-700f, y1, 0f);
        if (line > 2)
        {
            float y = panelDeck.sizeDelta.y + 450f * (line - 2);
            panelDeck.sizeDelta = new Vector2(1920f, y);    //задаем размер   new Vectro2 (width, height)
            y1 = 225f + 225f * (line - 2);            //отнесем точку размещения
            p = new Vector3(-700f, y1, 0f);
        }
        int k = 0;
        for (int i = 0; i < line; i++) //расставляем  обьекты (карты)
        {
            for (int j = 0; j < 5; j++)
            {
                if (k < allPCards.Length)
                {
                    deck[k] = Instantiate(cardPrefab);
                    deck[k].transform.SetParent(panelDeck.transform);
                    deck[k].transform.localPosition = p;
                    deck[k].GetComponent<Draggable>().enabled = false;        //откючим скрипт Drag and Drop для сцены battle
                    CardDisplay display = deck[k].GetComponent<CardDisplay>();//Возвращает компонент типа type, если он прикреплен к игровому объекту
                    display.CardSetup(allPCards[k]);
                    deck[k].transform.localScale = new Vector3(3f, 3f, 0f);
                    p.x = p.x + 350f;
                    k++;
                }
            }
            p.x = -700f;
            p.y = p.y - 450f;
        }
        ScrollPanel.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 1);
    }
}
