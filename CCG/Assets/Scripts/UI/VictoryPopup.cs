using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class VictoryPopup : MonoBehaviour
{
    [SerializeField] private GameObject[] Cards;
    [SerializeField] private Text text;
    private int coins;
    private int coins_;
    public void Open()
    {
        gameObject.SetActive(true); // Активируйте этот объект, чтобы открыть окно.
        Card[] all = Managers.cardManager.allPlayerCards;
        bool flag; 
        int r;
        int[] arrIndex = new int[3] {0,0,0};
        for (int i = 0; i < 3; i++)
        {
            flag = true;
            r = 0;
            while (flag)                    //что бы карты были разные сравним с теми что уже занесли
            {
                flag = false;
                r = UnityEngine.Random.Range(1, all.Length+1);
                for (int j = 0; j <=i;j++)
                {
                    if (r == arrIndex[j])
                    {
                        flag = true;
                        break;
                    }
                }
            }
            Card card = all[r - 1];

            arrIndex[i] = r;
            CardDisplay display = Cards[i].GetComponent<CardDisplay>();//Возвращает компонент типа type, если он прикреплен к игровому объекту
            display.CardSetup(card);
        }
        StartCoroutine("UpCoins");
    }
    public void Close()
    {
        gameObject.SetActive(false); // Деактивируйте объект, чтобы закрыть окно.
    }

    IEnumerator UpCoins()
    {
        coins = UnityEngine.Random.Range(8, 20);
        Managers.Player.money += coins;
        Managers.saveManager.CheckMoney(Managers.Player.money); //save money
        coins_ = 0;
        while (coins_ < coins)
        {
            coins_++;
            text.text = "+ " + coins_.ToString();
            yield return new WaitForSeconds(0.15f);

        }
    }
}