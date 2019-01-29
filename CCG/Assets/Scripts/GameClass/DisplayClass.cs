using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayClass : MonoBehaviour {

    [HideInInspector]     //скрыть public переменную в Inspector
    public GameClass class_;
    public Image classImage;
    public Text descriptionText;
    public Text nameText;

    public void ClassSetup(GameClass thisClass)
    {
        class_ = thisClass;
        nameText.text = thisClass.className;
        descriptionText.text = thisClass.description;
        classImage.sprite = thisClass.art;
    }
    public void setupBattle(GameClass thisClass)
    {
        class_ = thisClass;
        classImage.sprite = thisClass.art;
    }
}