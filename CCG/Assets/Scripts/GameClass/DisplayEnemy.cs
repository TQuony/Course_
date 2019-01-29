using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemy : MonoBehaviour
{

    [HideInInspector]     //скрыть public переменную в Inspector
    public Enemy enemy_;
    public Image enemyImage;
    public Text descriptionText;
    public Text nameText;

    public void ClassSetup(Enemy thisEnemy)
    {
        enemy_ = thisEnemy;
        nameText.text = thisEnemy.enemyName;
        descriptionText.text = thisEnemy.description;
        enemyImage.sprite = thisEnemy.art;
    }
    public void setupBattle(Enemy thisClass)
    {
        enemy_ = thisClass;
        enemyImage.sprite = thisClass.art;
    }
}