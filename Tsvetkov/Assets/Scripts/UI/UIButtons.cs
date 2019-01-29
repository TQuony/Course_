using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIButtons : MonoBehaviour {

    public void EndTurn()
    { // Метод, вызываемый кнопкой настроек. !Managers.cardManager.startTurn &&
        if (!Managers.cardManager.Moved)  //если не начало хода ( идет раздача карт)
            if (Managers.Player.playertTurn)         //если ход Player, то передать ход Enemy
                Managers.gameManager.StartCoroutine("NextTurn");
    }

    public void ToMenu()
    {
        Managers.gameManager.nameScene = "Menu";
        SceneManager.LoadScene("Load");
    }
    public void ToMenuAndSave()
    {
        Managers.saveManager.OnApplicationQuit();
        Managers.gameManager.nameScene = "Menu";
        SceneManager.LoadScene("Load");
    }
    public void ToStart()
    {
        Managers.saveManager.CheckClass(Managers.gameManager.NameClass, Managers.gameManager.NumberClass);
        Managers.saveManager.CheckHpPlayer(Managers.Player.health, Managers.Player.health);
        Managers.saveManager.CheckMoney(Managers.Player.money);
        Managers.saveManager.CheckDeck(Managers.Player.deckPlayer);
        Managers.saveManager.CheckInventory(Managers.Inventory.inventoryPlayer);
        Managers.saveManager.CheckEqupped(Managers.Inventory.equippedPlayer);
        Managers.Player.setMana();
        Managers.Mission.createLevel(10);
        Managers.gameManager.fSeeDeck = true;
        Managers.gameManager.nameScene = "Map";
        SceneManager.LoadScene("Load");
    }
    public void ToBattle()
    {
        Managers.gameManager.fSeeDeck = false;
        Vector3 pos = transform.position;
        int lev = Managers.Mission.curLevel;
        for (int i = 0; i < Managers.Mission.level.GetLength(0); i++)
        {
            if (Managers.Mission.level[i, lev] == 2)
            {
                if (Managers.Mission.objMap[i, lev].transform.position == pos)
                {
                    if (Managers.Mission.objMap[i, lev].GetComponent<Animator>().GetBool("Flicker_") == true)
                    {
                        Managers.Mission.objMap[i, lev].GetComponent<Animator>().SetBool("Flicker_", false);
                        Managers.Mission.objMap[i, lev].GetComponent<Animator>().SetBool("Click_", true);
                        Managers.Mission.wayLevel[lev] = i;
                        Debug.Log("Managers.Mission.wayLevel =  " + Managers.Mission.wayLevel[lev]);
                        Managers.saveManager.WayLevel(Managers.Mission.wayLevel);
                        break;
                    }
                }
            }
        }
    }
    public void ToSelectClass()
    {
        Managers.saveManager.DeleteSave();
        Managers.gameManager.nameScene = "SelectClass";
        SceneManager.LoadScene("Load");
    }

    public void ToLoad()
    {
        Managers.saveManager.CheckDeck(Managers.Player.deckPlayer);
        Managers.gameManager.fSeeDeck = true;
        Managers.gameManager.nameScene = "Map";
        SceneManager.LoadScene("Load");
    }
    public void ToDefeat()
    {
        Managers.saveManager.DeleteSave();
        Managers.gameManager.nameScene = "Menu";
        SceneManager.LoadScene("Load");
    }
    public void ToDeck()
    {
        Messenger.Broadcast(GameEvent.SEE_DECK); // отправим сообщение для показа колоды
    }
    public void ToInventory()
    {
        Messenger.Broadcast(GameEvent.SEE_INVENTORY); // отправим сообщение для показа колоды
    }
    public void ToCloseDeck()
    {
        Messenger.Broadcast(GameEvent.CLOSE_DECK); // отправим сообщение для закрытия колоды
    }
    public void ToCloseInventory()
    {
        Messenger.Broadcast(GameEvent.CLOSE_INVENTORY); // отправим сообщение для закрытия инвентаря
    }
    public void ToCloseEqupped()
    {
        Messenger.Broadcast(GameEvent.CLOSE_EQUIPPED); // отправим сообщение для закрытия инвентаря
    }
    public void ToAddEqupped()
    {
        if(gameObject.transform.GetChild(0).GetComponent<Text>().text=="Unequpped")
            Messenger.Broadcast(GameEvent.UNEQUIPPED); // отправим сообщение для закрытия инвентаря
        else
            Messenger.Broadcast(GameEvent.ADD_EQUIPPED); // отправим сообщение для закрытия инвентаря
    }
}
