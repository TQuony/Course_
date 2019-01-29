using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquppedPopup : MonoBehaviour
{
    public GameObject item;
    public void Open()
    {
        //gameObject.GetComponent<Item>. = Managers.gameManager.ItemEqupped;

        ItemDisplay display = item.GetComponent<ItemDisplay>();//Возвращает компонент типа type, если он прикреплен к игровому объекту
        display.ItemSetup(Managers.gameManager.ItemEqupped);
        gameObject.SetActive(true); // Активируйте этот объект, чтобы открыть окно.
    }

    public void Close()
    {
        gameObject.SetActive(false); // Деактивируйте объект, чтобы закрыть окно.
    }

}
