using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour , IBeginDragHandler, IDragHandler,IEndDragHandler{


     public Transform parentToReturnTo = null;
     public Vector3 returnPos;
     // private CardMove cardMove;
     private Card card;
     private Vector3 scale;
     public  void OnBeginDrag(PointerEventData evenData)
     {
        if (Managers.gameManager.fSeeDeck == false) //если карта находится не на сцене для просмотра колоды
        {
            card = (this.gameObject.GetComponent<CardDisplay>()).card;//захватим данные о обьекте который двигаем\
                                                                      //cardMove = gameObject.GetComponent<CardMove>();
            if (!Managers.cardManager.Moved)
            {
                if (transform.parent.gameObject.name != "ActivateZone")
                {
                    Debug.Log(" "+ this.transform.parent);
                    parentToReturnTo = this.transform.parent;
                    returnPos = this.transform.position;
                    this.transform.SetParent(this.transform.parent.parent);
                    GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
            }
        }
     }
    public void OnDrag(PointerEventData evenData)
    {
        if (Managers.gameManager.fSeeDeck == false)
        {
            //cardMove = gameObject.GetComponent<CardMove>();
            if (!Managers.cardManager.Moved)
            {
                if (transform.parent.gameObject.name != "ActivateZone")
                    this.transform.position = evenData.position;

            }
        }
    }
    public void OnEndDrag(PointerEventData evenData) {
        if (Managers.gameManager.fSeeDeck == false)
        {
            if (!Managers.cardManager.Moved)
            {
                if (transform.parent.gameObject.name != "ActivateZone")
                {
                    this.transform.SetParent(parentToReturnTo);
                    this.transform.position = returnPos;
                    GetComponent<CanvasGroup>().blocksRaycasts = true;
                }
            }
        }
    }
    public Card getCardObj
    {
        get
        {
            return card;
        }
    }
}
