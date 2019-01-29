using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemDisplay : MonoBehaviour {

    [HideInInspector]     //скрыть public переменную в Inspector
    public Item item;
    public Text descriptionText;
    public Text nameText;
    public Image cardImage;
    public Image cardFace;
    public void ItemSetup(Item thisItem)
    {
        item = thisItem;
        nameText.text = thisItem.cardName;
        descriptionText.text = thisItem.description;
        cardImage.sprite = thisItem.art;
        cardFace.sprite = thisItem.face;
    }
}
