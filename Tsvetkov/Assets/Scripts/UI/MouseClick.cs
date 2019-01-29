using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    private string[] deckPlayer;                      //колода игрока
    void OnMouseDown()
    {
        if (Managers.gameManager.reward)
        {
            Messenger.Broadcast(GameEvent.OPEN_REWARD); // отправим сообщение для получения награды
            Managers.gameManager.reward = false;
        }
        else
        {
            CardDisplay display = transform.GetComponent<CardDisplay>();
            string name = display.nameText.text;
            int len = Managers.Player.deckPlayer.Length + 1;
            deckPlayer = new string[len];
            string[] deckPlayer1 = Managers.Player.deckPlayer;
            int i = 0;
            foreach (string fVar in deckPlayer1)
            {
                deckPlayer[i] = fVar;
                i++;
            }
            deckPlayer[i] = name;
            Managers.Player.deckPlayer = deckPlayer;
            Managers.gameManager.fSeeDeck = true;
            Managers.saveManager.CheckDeck(Managers.Player.deckPlayer); //Сохраняем новую колоду
            Managers.Inventory.AddItem(Managers.Inventory.GetRandomItem());
            Managers.Inventory.AddItem(Managers.Inventory.GetRandomItem());
            Managers.Inventory.AddItem(Managers.Inventory.GetRandomItem());;
            Managers.saveManager.CheckInventory(Managers.Inventory.inventoryPlayer);//Сохраняем новый предмет
            Managers.gameManager.nameScene = "Map";
            SceneManager.LoadScene("Load");
        }
    }
}
