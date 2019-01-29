using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectClass : MonoBehaviour {

    [SerializeField] public GameObject class_;
    private GameClass[] allClasses;
    void Start()
    {
        allClasses = Managers.gameManager.allClasses;
        Managers.gameManager.NameClass = allClasses[2].className;            //запишем номер класса
        Managers.gameManager.NumberClass = 2;
        Managers.Player.health = allClasses[2].hp;
        Managers.Player.maxHealth = allClasses[2].hp;
        Managers.Player.money = allClasses[2].money;
        Managers.Player.deckPlayer = allClasses[2].classDeck;
        Managers.Inventory.inventoryPlayer = allClasses[2].classInventory;
        Managers.Inventory.equippedPlayer = new string[6];
        DisplayClass display = class_.GetComponent<DisplayClass>();
        display.ClassSetup(allClasses[2]);
    }
    public void goWarrior()
    {
        Managers.gameManager.NameClass = allClasses[2].className;             //запишем номер класса
        Managers.gameManager.NumberClass = 2;
        Managers.Player.health = allClasses[2].hp;
        Managers.Player.maxHealth = allClasses[2].hp;
        Managers.Player.money = allClasses[2].money;
        Managers.Player.deckPlayer = allClasses[2].classDeck;
        Managers.Inventory.inventoryPlayer = allClasses[2].classInventory;
        Managers.Inventory.equippedPlayer = new string[6];
        DisplayClass display = class_.GetComponent<DisplayClass>();
        display.ClassSetup(allClasses[2]);
    }
    public void goMage()
    {
        Managers.gameManager.NameClass = allClasses[0].className;              //запишем номер класса
        Managers.gameManager.NumberClass = 0;
        Managers.Player.health = allClasses[0].hp;
        Managers.Player.maxHealth = allClasses[0].hp;
        Managers.Player.money = allClasses[0].money;
        Managers.Player.deckPlayer = allClasses[0].classDeck;
        Managers.Inventory.inventoryPlayer = allClasses[0].classInventory;
        Managers.Inventory.equippedPlayer = new string[6];
        DisplayClass display = class_.GetComponent<DisplayClass>();
        display.ClassSetup(allClasses[0]);
    }
    public void goRogue()
    {
        Managers.gameManager.NameClass = allClasses[1].className;              //запишем номер класса
        Managers.gameManager.NumberClass = 1;
        Managers.Player.health = allClasses[1].hp;
        Managers.Player.maxHealth = allClasses[1].hp;
        Managers.Player.money = allClasses[1].money;
        Managers.Player.deckPlayer = allClasses[1].classDeck;
        Managers.Inventory.inventoryPlayer = allClasses[1].classInventory;
        Managers.Inventory.equippedPlayer = new string[6];
        DisplayClass display = class_.GetComponent<DisplayClass>();
        display.ClassSetup(allClasses[1]);
    }
}
