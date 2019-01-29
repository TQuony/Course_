using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour, IGameManager
{ //Наследуем класс и реализуем интерфейс.

    public ManagerStatus status { get; private set; }
    public int health       { get; private set; }
    public int maxHealth    { get; private set; }
    public string[] deckEnemy { get; set; }          //колода врага
    public GameObject Enemy { get; set; }
    private GameObject EnemyInTable;
    private Vector3 pointEnemy = new Vector3(670f, 30f, 0f);

    public void Startup()
    {
        status = ManagerStatus.Started;
    }
    public void postEnemy()
    {
        health = 30;        //Эти значения могут быть инициализированы
        maxHealth = 30;     //сохраненными данными.
        deckEnemy = new string[] { "Swipe", "Swipe", "Swipe", "Swipe", "Swipe", "Swipe", "Swipe", "Heal", "Heal", "Heal" };
        EnemyInTable = Instantiate(Enemy);// Ссылка на контейнер для моделик игрока.
        EnemyInTable.transform.SetParent(Managers.gameManager.Table.transform);
        EnemyInTable.transform.localPosition = pointEnemy;
        EnemyInTable.transform.localScale = new Vector3(1f, 1f, 1f);//временно
        DisplayEnemy display = EnemyInTable.GetComponent<DisplayEnemy>();//Возвращает компонент типа type, если он прикреплен к игровому объекту
        int r = UnityEngine.Random.Range(0,2);
        display.setupBattle(Managers.gameManager.allEnemy[r]);
    }
    public void ChangeHealth(int value)
    { //Другие сценарии не могут напрямую задавать переменную health, но могут вызывать эту функцию.
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0)
        {
            health = 0;
            Managers.Mission.ReachObjective(); // Вызываем новый целевой метод в сценарии MissionManager.
        }
        //Debug.Log("Health: " + health + "/" + maxHealth);
        Messenger.Broadcast(GameEvent.HP_UPDATED_ENEMY); // отправим сообщение об изменении Hp
    }
    private void DisplayDeck()
    { //Вывод на консоль сообщения о текущей деке.
        string itemDisplay = "Deck: ";
        foreach (string item in deckEnemy)
        {
            itemDisplay += item + " ";
        }
        Debug.Log(itemDisplay);
    }
    public void AddCard(string name)
    { //Другие сценарии не могут напрямую управлять списком элементов, но могут вызывать этот метод.
        //deckEnemy.Add(name);
        //DisplayDeck();
    }
    IEnumerator enemyTurn()
    {
        while (Managers.cardManager.Moved) {
            yield return null;
        }
            Managers.gameManager.StartCoroutine("NextTurn");
    }
}