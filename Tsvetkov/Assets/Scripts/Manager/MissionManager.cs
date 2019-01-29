using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine.UI;
public class MissionManager : IGameManager
{
    private readonly ISaveManager _saveManager;
    public ManagerStatus status { get; private set; }
    public int curLevel { get;  set; }
    public int maxLevel { get; private set; }
    public int[,]  level{ get;  set; }
    public Animator _animator { get; set; }
    public GameObject[,]  objMap { get; set; }
    public int[] wayLevel { get; set; }

    public MissionManager(ISaveManager saveManager)
    {
        _saveManager = saveManager;
    }

    public void Startup()
    {
        status = ManagerStatus.Started;
    }
    //public void GoToNext()
    //{
    //    if (curLevel < maxLevel)
    //    { //Рассылаем аргументы вместе с объектом WWW, используя объект WWWForm.
    //        curLevel++;
    //        string name = "Battle";
    //        Debug.Log("Loading " + name);
    //        Application.LoadLevel(name); // Проверяем, достигнут ли последний уровень.
    //    }
    //    else
    //    {
    //        Debug.Log("Last level");
    //    }
    //}
    public void ReachObjective()
    {
        // здесь может быть код обработки нескольких целей
        Messenger.Broadcast(GameEvent.BATTLE_COMPLETE);
    }

    public void createLevel(int size)
    {
        int r;
        size = size + 2;
        curLevel = 0;
        maxLevel = size;
        int[,] arr = new int[5, size];
        wayLevel = new int[size];
        arr[0, 0] = 1;
        arr[1, 0] = 1;
        arr[2, 0] = 2;
        arr[3, 0] = 1;
        arr[4, 0] = 1;
        for (int j = 0; j < 5; j++)
        {
            bool fl = false;
            for (int i = 1; i < size - 1; i++)
            {
                r = UnityEngine.Random.Range(-5, 30);
                if (r == 0 || r == 10|| r == 20 || r == 30 || r == -5)             //если Camp (1 лагерь на секцию)
                {
                    if (fl == false)
                    {
                        fl = true;      //Запомним что 1 Camp уже есть
                        r = 0;
                    }
                    else
                    {
                        r = UnityEngine.Random.Range(-10, 10);
                        if (r < 0)
                            r = 1;
                        else
                            r = 2;
                    }
                }
                else
                {
                    r = UnityEngine.Random.Range(-10, 10);
                    if (r < 0)
                        r = 1;
                    else
                        r = 2;
                }
                arr[j, i] = r;
            }
        }
        arr[0, size-1] = 1;
        arr[1, size - 1] = 1;
        arr[2, size-1] = 100;
        arr[3, size-1] = 1;
        arr[4, size - 1] = 1;
        _saveManager.CheckLevel(arr);
        _saveManager.CheclCurLevel(curLevel);
        level = arr;
    }
}