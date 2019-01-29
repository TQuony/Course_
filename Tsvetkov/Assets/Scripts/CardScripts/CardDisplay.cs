using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    [HideInInspector]     //скрыть public переменную в Inspector
    public Card card;
    public Text descriptionText;
    public Text nameText;
    public Text manaText;
    public Image cardImage;
    public Image cardFace;
    public void CardSetup(Card thisCard)
    {
        card = thisCard;
        nameText.text = card.cardName;
        descriptionText.text = card.description;
        cardImage.sprite = card.art;
        cardFace.sprite = card.face;
        manaText.text = card.mana.ToString();
    }
}
