using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IPointerClickHandler{

    public bool empty;              //пустой ли слот
    public bool equpped;
    public Item item;
    public int ID;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!empty)             //если слот не пустой
        {
            if (!equpped)       //и не экипирован
            {
                Managers.gameManager.ItemEqupped = item;
                Managers.gameManager.IDEqupped = ID;
                Messenger.Broadcast(GameEvent.SEE_EQUIPPED);
            }
            else
            {
                Managers.gameManager.ItemEqupped = item;
                Managers.gameManager.IDEqupped = ID;
                Messenger.Broadcast(GameEvent.SEE_UNEQUIPPED);
            }
        }
    }
    public GameObject getObj()
    {
        return transform.GetChild(0).gameObject;
    }
}
