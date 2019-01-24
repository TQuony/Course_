using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }
    private List<IGameManager> _startSequence; //Список диспетчеров, который просматривается в цикле во время стартовой последовательности.
    void Awake()
    {
       
        gameManager = GetComponent<GameManager>();
        _startSequence = new List<IGameManager>();
        _startSequence.Add(gameManager);
        StartCoroutine(StartupManagers());
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
       // Messenger.Broadcast(GameEvent.RESTART);
    }
}