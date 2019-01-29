using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneController : MonoBehaviour
{
    //[SerializeField] private GameObject prefWarrior;//Сериализованная переменная для связи с объектом-шаблоном
    //[SerializeField] private GameObject prefRogue;
    // [SerializeField] private GameObject prefMage;
    [SerializeField] private GameObject prefPlayer;
    [SerializeField] private GameObject tablePrefab;
    [SerializeField] private GameObject handPrefab;
    [SerializeField] private GameObject activePrefab;
    [SerializeField] private GameObject txtDamagePrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject enemyCardPrefab;
    [SerializeField] private GameObject deckPrefab;
    [SerializeField] private EditPath playerPath;
    [SerializeField] private EditPath enemyPath;
    [SerializeField] private EditPath enemyPathHand;
    [SerializeField] private GameObject enemyHandPrefab;
    [SerializeField] private string strBattle;
    [SerializeField] private string strBattle2;
    [SerializeField] private AudioSource musBattle;
    [SerializeField] private AudioSource musBattle2;
    void Start()
    {
        Managers.Audio.music1 = strBattle;
        Managers.Audio.music2 = strBattle2;
        Managers.Audio.music1Source = musBattle;
        Managers.Audio.music2Source = musBattle2;
        Managers.Audio.music1Source.ignoreListenerVolume = true;
        Managers.Audio.music2Source.ignoreListenerVolume = true;
        Managers.Audio.music1Source.ignoreListenerPause = true;
        Managers.Audio.music2Source.ignoreListenerPause = true;
        Managers.Audio.soundVolume = 1f;
        Managers.Audio.musicVolume = 1f;
        Managers.Audio._activeMusic = Managers.Audio.music1Source; // Инициализируем один из источников как активный.
        Managers.Audio._inactiveMusic = Managers.Audio.music2Source;

        Managers.Audio.PlayMusic1();

        Managers.gameManager.Table = tablePrefab;           //передаем обьекты сцены
        Managers.gameManager.Hand  = handPrefab;
        Managers.gameManager.enemyHand = enemyHandPrefab;
        Managers.gameManager.ActiveZone = activePrefab;
        Managers.gameManager.txtDamage = txtDamagePrefab;
        //switch (Managers.gameManager.NumberClass)            //Размещаем игрока
        //{
        //    case 0:
        //        Managers.Player.Player = prefMage;              
        //        break;
        //    case 1:
        //        Managers.Player.Player = prefRogue;              
        //        break;
        //    case 2:
        //        Managers.Player.Player = prefWarrior;              
        //        break;
        //}
        Managers.Player.Player = prefPlayer;
        Managers.Player.postPlayer();               
        Managers.Player.playertTurn = true;

        Managers.Enemy.Enemy = enemyPrefab;              //Размещаем врага
        Managers.Enemy.postEnemy();

        Managers.gameManager.postObjects();                 //разместим обьекты (цифры урона)

        Managers.cardManager.cardPrefab = cardPrefab;
        Managers.cardManager.enemyCardPrefab = enemyCardPrefab;
        Managers.cardManager.deckPrefab = deckPrefab;
        Managers.cardManager.playerPath = playerPath;
        Managers.cardManager.enemyPath = enemyPath;
        Managers.cardManager.enemyPathHand = enemyPathHand;
        Managers.cardManager.postCard();

        Messenger.Broadcast(GameEvent.SET_UI);
        Messenger.Broadcast(GameEvent.SET_MANA);            //установить значение манапула
        Managers.gameManager.reward = true;
        Managers.cardManager.StartCoroutine("AddCardPlayer", 4);
    }
}
