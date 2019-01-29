using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(SaveManager))]
[RequireComponent(typeof(CardsManager))] // Гарантируем существование различных диспетчеров.
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(MissionManager))]
[RequireComponent(typeof(AudioManager))]

public class Managers : MonoBehaviour
{
    public SaveManager  saveManager { get; private set; }
    public static GameManager    gameManager { get; private set; }
    public static PlayerManager  Player { get; private set; }
    public static EnemyManager   Enemy { get; private set; }
    public static CardsManager   cardManager { get; private set; } //Статические свойства, которыми остальной код пользуется для доступа к диспетчерам.
    public static MissionManager Mission { get; private set; }
    public static AudioManager   Audio { get; private set; }
    public static InventoryManager Inventory { get; private set; }
    private List<IGameManager> _startSequence; //Список диспетчеров, который просматривается в цикле во время стартовой последовательности.
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Команда Unity для сохранения объекта между сценами.
        saveManager = new SaveManager(); // GetComponent<SaveManager>();
        gameManager = GetComponent<GameManager>();
        Player      = GetComponent<PlayerManager>();
        Inventory   = GetComponent<InventoryManager>();
        Enemy       = GetComponent<EnemyManager>();
        cardManager = GetComponent<CardsManager>();
        Mission     = new MissionManager(saveManager); // GetComponent<MissionManager>();
            
        Audio = GetComponent<AudioManager>();
        
        _startSequence = new List<IGameManager>();
        _startSequence.Add(saveManager);
        _startSequence.Add(Inventory);
        _startSequence.Add(Player);
        _startSequence.Add(Enemy);
        _startSequence.Add(gameManager);
        _startSequence.Add(cardManager);
        _startSequence.Add(Mission);
        _startSequence.Add(Audio);
        StartCoroutine(StartupManagers()); //Асинхронно загружаем стартовую последовательность.
}
    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup();
        }
        yield return null;
        int numModules = _startSequence.Count;
        int numReady = 0;
        while (numReady < numModules)
        { // Продолжаем цикл, пока не начнут работать все диспетчеры.
        int lastReady = numReady;
            numReady = 0;
            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);
            }
            yield return null; //Остановка на один кадр перед следующей проверкой.
        }
        //Все менеджеры загружены
    }
}