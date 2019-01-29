using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler,IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
       // Debug.Log("OnPointerEnter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //
    }
    public void OnDrop(PointerEventData eventData) {
        if (!Managers.cardManager.Moved)
        {
            if (gameObject.name == "ActivateZone" ) //если карта попала в зону активации
            { 
                Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
                Image a = eventData.pointerDrag.GetComponent<Dissolve>().Image1;
                if (a.material.name != "Dissolve_2D(Clone)")
                {
                    bool flag = false;       //хватает манапула на розыгрыш карты
                    switch (d.getCardObj.manaName)            //Смотрим на тип карты
                    {
                        case "str":
                            if (Managers.Player.manaSTR - d.getCardObj.mana >= 0)
                            {
                                flag = true;
                                Managers.Player.ChangeManaStr(-d.getCardObj.mana);
                            }
                            break;
                        case "int":
                            if (Managers.Player.manaINT - d.getCardObj.mana >= 0)
                            {
                                flag = true;
                                Managers.Player.ChangeManaInt(-d.getCardObj.mana);
                            }
                            break;
                        case "agi":
                            if (Managers.Player.manaAGI - d.getCardObj.mana >= 0)
                            {
                                flag = true;
                                Managers.Player.ChangeManaAgi(-d.getCardObj.mana);
                            }
                            break;
                    }
                    if (flag)
                    {
                        d.parentToReturnTo = this.transform;
                        d.returnPos = eventData.position;
                        if (d.getCardObj.damage < 0)
                        {      //если damage карты < 0, ударить врага на это значение
                            Managers.gameManager.changeHpEnemy(d.getCardObj.damage);
                        }
                        if (d.getCardObj.damage > 0)
                        {      //если damage карты > 0, карта хилит игрока
                            Managers.gameManager.changeHpPlayer(d.getCardObj.damage);
                        }
                        d.gameObject.GetComponent<Dissolve>().StartDissolve();
                        
                    }
                }
            }
        }
    }
}
