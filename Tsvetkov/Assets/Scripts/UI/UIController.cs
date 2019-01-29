using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text Health;
    [SerializeField] private VictoryPopup victoryPopup;
    [SerializeField] private VictoryPopup defeatPopup;
    [SerializeField] private Image HpEnemy;
    [SerializeField] private Image HpPlayer;
    [SerializeField] private Text txtManaStr;
    [SerializeField] private Image ManaStr;
    [SerializeField] private Text txtManaInt;
    [SerializeField] private Image ManaInt;
    [SerializeField] private Text txtManaAgi;
    [SerializeField] private Image ManaAgi;
    void Awake()
    { // Задаем подписчика для события обновления здоровья.
        Messenger.AddListener(GameEvent.HP_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.BATTLE_COMPLETE, BattleComplete);
        Messenger.AddListener(GameEvent.GAME_OVER, GameOver);
        Messenger.AddListener(GameEvent.SET_UI, setUI);
        Messenger.AddListener(GameEvent.SET_MANA, setMana);
        Messenger.AddListener(GameEvent.MANA_STR_UPDATED, setManaStr);
        Messenger.AddListener(GameEvent.MANA_INT_UPDATED, setManaInt);
        Messenger.AddListener(GameEvent.MANA_AGI_UPDATED, setManaAgi);
        Messenger.AddListener(GameEvent.HP_UPDATED_ENEMY, HealthEnemyUpdate);
        Messenger.AddListener(GameEvent.OPEN_REWARD, OpenReward);
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.HP_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.BATTLE_COMPLETE, BattleComplete);
        Messenger.RemoveListener(GameEvent.GAME_OVER, GameOver);
        Messenger.RemoveListener(GameEvent.SET_UI, setUI);
        Messenger.RemoveListener(GameEvent.SET_MANA, setMana);
        Messenger.RemoveListener(GameEvent.MANA_STR_UPDATED, setManaStr);
        Messenger.RemoveListener(GameEvent.MANA_INT_UPDATED, setManaInt);
        Messenger.RemoveListener(GameEvent.MANA_AGI_UPDATED, setManaAgi);
        Messenger.RemoveListener(GameEvent.HP_UPDATED_ENEMY, HealthEnemyUpdate);
        Messenger.RemoveListener(GameEvent.OPEN_REWARD, OpenReward);
    }
    void Start()
    {
        //Managers.Player.setMana();
        victoryPopup.Close(); // Закрываем всплывающее окно в момент начала игры.
        defeatPopup.Close();
    }
    private void OnHealthUpdated()
    { // Подписчик события вызывает функцию для обновления метки health.
        Health.text = Managers.Player.health + "/" + Managers.Player.maxHealth;
        float f = (float)(Managers.Player.health) / (float)(Managers.Player.maxHealth);
        HpPlayer.fillAmount = f;
    }
    private void HealthEnemyUpdate()
    { // Подписчик события вызывает функцию для обновления метки health.
        float f = (float)(Managers.Enemy.health) / (float)(Managers.Enemy.maxHealth);
        HpEnemy.fillAmount = f;
    }
    private void BattleComplete()
    {
        Managers.Audio.PlayMusic2();
        Managers.Mission.curLevel = Managers.Mission.curLevel + 1;
        Managers.saveManager.CheclCurLevel(Managers.Mission.curLevel); //save текущий этап
        Managers.saveManager.CheckHpPlayer(Managers.Player.health, Managers.Player.maxHealth);//save hp
        victoryPopup.transform.SetAsLastSibling();
        victoryPopup.Open(); // метод всплывающего окна.
        victoryPopup.GetComponent<Animator>().SetBool("Open", true);
    }
    private void OpenReward()
    {
        victoryPopup.GetComponent<Animator>().SetBool("Reward", true);
    }
    private void GameOver()
    {
        defeatPopup.transform.SetAsLastSibling();
        defeatPopup.Open(); // метод всплывающего окна.
    }

    private void setUI()
    {
        float f = (float)(Managers.Player.health) / (float)(Managers.Player.maxHealth);
        HpPlayer.fillAmount = f;
        Health.text = Managers.Player.health + "/" + Managers.Player.maxHealth;
        HpEnemy.fillAmount = 1;
        Managers.Player.manaSTR = Managers.Player.maxManaSTR;
        Managers.Player.manaINT = Managers.Player.maxManaINT;
        Managers.Player.manaAGI = Managers.Player.maxManaAGI;
    }
    private void setMana()
    {
        float f = (float)(Managers.Player.manaSTR) / (float)(Managers.Player.maxManaSTR);
        ManaStr.fillAmount = f;
        txtManaStr.text = Managers.Player.manaSTR + "/" + Managers.Player.maxManaSTR;
        f = (float)(Managers.Player.manaINT) / (float)(Managers.Player.maxManaINT);
        ManaInt.fillAmount = f;
        txtManaInt.text = Managers.Player.manaINT + "/" + Managers.Player.maxManaINT;
        f = (float)(Managers.Player.manaAGI) / (float)(Managers.Player.maxManaAGI);
        ManaAgi.fillAmount = f;
        txtManaAgi.text = Managers.Player.manaAGI + "/" + Managers.Player.maxManaAGI;
    }
    private void setManaStr()   //изменяем отображенения манапула по силе
    {
        txtManaStr.text = Managers.Player.manaSTR + "/" + Managers.Player.maxManaSTR;
        float f = (float)(Managers.Player.manaSTR) / (float)(Managers.Player.maxManaSTR);
        ManaStr.fillAmount = f;
    }
    private void setManaInt()   //изменяем отображенения манапула по силе
    {
        txtManaInt.text = Managers.Player.manaINT + "/" + Managers.Player.maxManaINT;
        float f = (float)(Managers.Player.manaINT) / (float)(Managers.Player.maxManaINT);
        ManaInt.fillAmount = f;
    }
    private void setManaAgi()   //изменяем отображенения манапула по силе
    {
        txtManaAgi.text = Managers.Player.manaAGI + "/" + Managers.Player.maxManaAGI;
        float f = (float)(Managers.Player.manaAGI) / (float)(Managers.Player.maxManaAGI);
        ManaAgi.fillAmount = f;
    }
}