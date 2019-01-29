using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIControllerMap: MonoBehaviour {

    [SerializeField] private DeckPopup deckPopup;
    [SerializeField] private InventoryPopup inventoryPopup;
    [SerializeField] private EquppedPopup equppedPopup;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject slotPrefab;
    public GameObject slotHolder;           //под инвентарь
    public GameObject slotHolderEquipped;           //под слоты           
    private int allSlots;                   //отображение инвентаря
    private int enabledSlots;
    private GameObject[] slot;              //массив слотов инвентаря
    private GameObject[] slotEqupped;

    [SerializeField] private Text Health;       //здоровье
    [SerializeField] private Text txtStr;
    [SerializeField] private Text txtInt;
    [SerializeField] private Text txtAgi;

    [SerializeField] private Text btnText;       //изменить текст кнопки

    void Awake()
    { // Задаем подписчика для события 
        Messenger.AddListener(GameEvent.SEE_DECK, seeDeck);
        Messenger.AddListener(GameEvent.CLOSE_DECK, closeDeck);
        Messenger.AddListener(GameEvent.SEE_INVENTORY, seeInv);
        Messenger.AddListener(GameEvent.CLOSE_INVENTORY, closeInv);
        Messenger.AddListener(GameEvent.SEE_EQUIPPED, seeEqupped);
        Messenger.AddListener(GameEvent.CLOSE_EQUIPPED, closeEqupped);
        Messenger.AddListener(GameEvent.SEE_UNEQUIPPED, seeUnEqupped);
        Messenger.AddListener(GameEvent.ADD_EQUIPPED, addEqupped);
        Messenger.AddListener(GameEvent.UNEQUIPPED, unEqupped);
        Messenger.AddListener(GameEvent.SET_UI, setUI);
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SEE_DECK, seeDeck);
        Messenger.RemoveListener(GameEvent.CLOSE_DECK, closeDeck);
        Messenger.RemoveListener(GameEvent.SEE_INVENTORY, seeInv);
        Messenger.RemoveListener(GameEvent.CLOSE_INVENTORY, closeInv);
        Messenger.RemoveListener(GameEvent.SEE_EQUIPPED, seeEqupped);
        Messenger.RemoveListener(GameEvent.CLOSE_EQUIPPED, closeEqupped);
        Messenger.RemoveListener(GameEvent.SEE_UNEQUIPPED, seeUnEqupped);
        Messenger.RemoveListener(GameEvent.ADD_EQUIPPED, addEqupped);
        Messenger.RemoveListener(GameEvent.UNEQUIPPED, unEqupped);
        Messenger.RemoveListener(GameEvent.SET_UI, setUI);
    }
    void Start()
    {
        deckPopup.Close();          // Закрываем всплывающее окно в момент начала игры.
        inventoryPopup.Close();
        equppedPopup.Close();

        if (Managers.Inventory.inventoryPlayer.Length <= 18)
            allSlots = 18;
        createInventory();                                      //создать панель инвентаря
    }
    private void seeDeck()
    { // Подписчик события вызывает функцию для просмотра колоды.
        deckPopup.transform.SetAsLastSibling();
        deckPopup.Open(); // метод всплывающего окна.
    }
    private void closeDeck()
    { // Подписчик события вызывает функцию для закрытия колоды.
        deckPopup.Close(); // метод всплывающего окна.
    }
    private void seeInv()
    { // Подписчик события вызывает функцию для просмотра инвенторя.
        inventoryPopup.transform.SetAsLastSibling();
        Messenger.Broadcast(GameEvent.SET_UI);
        inventoryPopup.Open(); // метод всплывающего окна.
    }
    private void closeInv()
    { // Подписчик события вызывает функцию для закрытия инвенторя.
        inventoryPopup.Close(); // метод всплывающего окна.
    }
    private void seeEqupped()
    { // Подписчик события вызывает функцию для просмотра инвенторя.
        equppedPopup.transform.SetAsLastSibling();
        btnText.text = "Equpped";
        equppedPopup.Open(); // метод всплывающего окна.
    }
    private void seeUnEqupped()
    { // Подписчик события вызывает функцию для просмотра инвенторя.
        equppedPopup.transform.SetAsLastSibling();
        btnText.text = "Unequpped";
        equppedPopup.Open(); // метод всплывающего окна.
    }
    private void closeEqupped()
    { // Подписчик события вызывает функцию для закрытия инвенторя.
        equppedPopup.Close(); // метод всплывающего окна.
    }
    private void addEqupped()
    {
        for (int i = 0; i < slotEqupped.Length; i++)
        {
            if (slotEqupped[i].GetComponent<Slot>().equpped == false)
            {
                string nameItem = Managers.Inventory.inventoryPlayer[Managers.gameManager.IDEqupped];
                Managers.Inventory.AddEqupped(i, nameItem);                 //поместим имя итема в массив экипировки
                Managers.Inventory.DeleteItem(Managers.gameManager.IDEqupped); //удалим карту из массива инвентаря
                Managers.saveManager.CheckInventory(Managers.Inventory.inventoryPlayer);//сохраним состояние инвентаря
                Managers.saveManager.CheckEqupped(Managers.Inventory.equippedPlayer);   //сохраним состояние экипировки

                for (int k = 0; k < Managers.Inventory.allItem.Length; k++)
                {
                    if (Managers.Inventory.allItem[k].cardName == nameItem)
                    {
                        if (Managers.Inventory.allItem[k].ID == 0) //если статусная карта
                        {
                            switch (Managers.Inventory.allItem[k].statusName)            //Смотрим какой атрибуд повышает
                            {
                                case "str":
                                    Managers.Player.manaSTR+= Managers.Inventory.allItem[k].value;
                                    Managers.Player.maxManaSTR += Managers.Inventory.allItem[k].value;
                                    break;
                                case "int":
                                    Managers.Player.manaINT += Managers.Inventory.allItem[k].value;
                                    Managers.Player.maxManaINT += Managers.Inventory.allItem[k].value;
                                    break;
                                case "agi":
                                    Managers.Player.manaAGI += Managers.Inventory.allItem[k].value;
                                    Managers.Player.maxManaAGI += Managers.Inventory.allItem[k].value;
                                    break;
                                case "heart":
                                    Managers.Player.health += Managers.Inventory.allItem[k].value;
                                    Managers.Player.maxHealth += Managers.Inventory.allItem[k].value;
                                    break;
                            }
                            break;
                        }
                    }
                }
                Managers.saveManager.CheckHpPlayer(Managers.Player.health, Managers.Player.maxHealth);
                Messenger.Broadcast(GameEvent.SET_UI);
                createInventory();
                break;
            }
        }
        equppedPopup.Close(); // метод всплывающего окна.
    }
    private void unEqupped()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].GetComponent<Slot>().empty == true)
            {
                string nameItem = Managers.Inventory.equippedPlayer[Managers.gameManager.IDEqupped];

                Managers.Inventory.DeleteEqupped(Managers.gameManager.IDEqupped);

                Managers.Inventory.AddItem(nameItem);                 //поместим имя итема в массив инвентаря

                Managers.saveManager.CheckInventory(Managers.Inventory.inventoryPlayer);//сохраним состояние инвентаря
                Managers.saveManager.CheckEqupped(Managers.Inventory.equippedPlayer);   //сохраним состояние экипировки

                for (int k = 0; k < Managers.Inventory.allItem.Length; k++)
                {
                    if (Managers.Inventory.allItem[k].cardName == nameItem)
                    {
                        if (Managers.Inventory.allItem[k].ID == 0) //если статусная карта
                        {
                            switch (Managers.Inventory.allItem[k].statusName)            //Смотрим какой атрибуд повышает
                            {
                                case "str":
                                    Managers.Player.manaSTR -= Managers.Inventory.allItem[k].value;
                                    Managers.Player.maxManaSTR -= Managers.Inventory.allItem[k].value;
                                    break;
                                case "int":
                                    Managers.Player.manaINT -= Managers.Inventory.allItem[k].value;
                                    Managers.Player.maxManaINT -= Managers.Inventory.allItem[k].value;
                                    break;
                                case "agi":
                                    Managers.Player.manaAGI -= Managers.Inventory.allItem[k].value;
                                    Managers.Player.maxManaAGI -= Managers.Inventory.allItem[k].value;
                                    break;
                                case "heart":
                                    Managers.Player.health -= Managers.Inventory.allItem[k].value;
                                    Managers.Player.maxHealth -= Managers.Inventory.allItem[k].value;
                                    break;
                            }
                            break;
                        }
                    }
                }
                Managers.saveManager.CheckHpPlayer(Managers.Player.health, Managers.Player.maxHealth);
                Messenger.Broadcast(GameEvent.SET_UI);
                createInventory();
                break;
            }
        }
        equppedPopup.Close(); // метод всплывающего окна.
    }
    public void ClearChildren( GameObject obj)
    {
        int countchild = obj.transform.childCount; //Узнаем количество потомков


        for (int i = 0; i < (countchild); i++)
        {
            Destroy(obj.transform.GetChild((int)i).gameObject, 0);
        }
    }
    private void createInventory()
    {

        ClearChildren(slotHolder);
        ClearChildren(slotHolderEquipped); 
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = Instantiate(slotPrefab);
            slot[i].transform.SetParent(slotHolder.transform);
            slot[i].transform.localScale = new Vector3(1f, 1f, 1f);
            slot[i].GetComponent<Slot>().empty = true;     //слот пустой
            slot[i].GetComponent<Slot>().equpped = false; //не екепирован
        }
        for (int j = 0; j < Managers.Inventory.inventoryPlayer.Length; j++)
        {
            for (int k = 0; k < Managers.Inventory.allItem.Length; k++)
            {
                if (Managers.Inventory.allItem[k].cardName == Managers.Inventory.inventoryPlayer[j])
                {
                    for (int i = 0; i < allSlots; i++)
                    {
                        if (slot[i].GetComponent<Slot>().empty)
                        {
                            GameObject item = Instantiate(itemPrefab);// Ссылка на контейнер для исходной карты или ее копий.

                            item.transform.SetParent(slot[i].transform);
                            item.transform.localPosition = slot[i].transform.position;
                            item.transform.localScale = new Vector3(3.2f, 3.4f, 0f);
                            ItemDisplay display = item.GetComponent<ItemDisplay>();//Возвращает компонент типа type, если он прикреплен к игровому объекту
                            display.ItemSetup(Managers.Inventory.allItem[k]);
                            slot[i].GetComponent<Slot>().empty = false; //слот не пустой
                            slot[i].GetComponent<Slot>().equpped = false; //не екепирован
                            slot[i].GetComponent<Slot>().item = Managers.Inventory.allItem[k];
                            slot[i].GetComponent<Slot>().ID = i;
                            break;
                        }
                    }
                    break;
                }
            }
        }
        slotEqupped = new GameObject[6];
        for (int i = 0; i < 6; i++)
        {
            slotEqupped[i] = Instantiate(slotPrefab);
            slotEqupped[i].transform.SetParent(slotHolderEquipped.transform);
            slotEqupped[i].transform.localScale = new Vector3(1f, 1f, 1f);
            slotEqupped[i].GetComponent<Slot>().empty = true;     //слот пустой
            slotEqupped[i].GetComponent<Slot>().equpped = false; //не екепирован
        }
        for (int j = 0; j < Managers.Inventory.equippedPlayer.Length; j++)
        {
            for (int k = 0; k < Managers.Inventory.allItem.Length; k++)
            {
                if (Managers.Inventory.allItem[k].cardName == Managers.Inventory.equippedPlayer[j])
                {
                    //for (int i = 0; i < 6; i++)
                    //{
                       // if (slotEqupped[i].GetComponent<Slot>().empty)
                        //{
                            GameObject item = Instantiate(itemPrefab);// Ссылка на контейнер для исходной карты или ее копий.

                            item.transform.SetParent(slotEqupped[j].transform);
                            item.transform.localPosition = slotEqupped[j].transform.position;
                            item.transform.localScale = new Vector3(3.2f, 3.4f, 0f);
                            ItemDisplay display = item.GetComponent<ItemDisplay>();//Возвращает компонент типа type, если он прикреплен к игровому объекту
                            display.ItemSetup(Managers.Inventory.allItem[k]);
                            slotEqupped[j].GetComponent<Slot>().empty = false; //слот не пустой
                            slotEqupped[j].GetComponent<Slot>().equpped = true; //экипирован
                            slotEqupped[j].GetComponent<Slot>().item = Managers.Inventory.allItem[k];
                            slotEqupped[j].GetComponent<Slot>().ID = j;
                            //break;
                       // }
                    //}
                    break;
                }
            }
        }
    }
    private void setUI()
    {
        Health.text = Managers.Player.health + "/" + Managers.Player.maxHealth;
        txtStr.text = "" + Managers.Player.maxManaSTR;
        txtInt.text = "" + Managers.Player.maxManaINT;
        txtAgi.text = "" + Managers.Player.maxManaAGI;
    }
}