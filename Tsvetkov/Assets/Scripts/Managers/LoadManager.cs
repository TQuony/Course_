using UnityEngine;
using System;
using System.Collections;
using DefaultNamespace;
using InterfaceNamespace;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class LoadManager : ILoadManager
{
    private AsyncOperation _loadOperation;

    //private object _loadOperation;
    public LoadManager(ILogicManager logicManager)
    {
        LogicManager = logicManager;
    }

    public LoadManager()
    {
       
    }


    public ILogicManager LogicManager { get; private set; }

    public void Navigate(SceneTypeEnum sceneTypeFrom, SceneTypeEnum sceneTypeTo, CustomObject customObject)
    {
        var sceneName = Strings.GetScenePath(sceneTypeTo);

        
        // Load Scene
        _loadOperation = SceneManager.LoadSceneAsync(sceneName as string);
        _loadOperation.allowSceneActivation = true;
        
       // LogicManager.Coroutiner.StartCoroutine(WaitSceneLoading(sceneTypeTo, customObject,
            //() => OnChanged(EventTypeEnum.NavigationEnd, sceneTypeTo)));
    }
    private IEnumerator WaitSceneLoading(SceneTypeEnum sceneTypeTo, CustomObject customObject, Action action)
    {
        var isLoaded = _loadOperation.isDone;
        while (isLoaded == false)
        {
            yield return new WaitForEndOfFrame();
            isLoaded = _loadOperation.isDone;
        }
        
        //Dirty code
        var sceneAttachedScripts = UnityEngine.Object.FindObjectOfType<BaseScene>();
        IScene sceneTo = sceneAttachedScripts;
        sceneTo.SetDependencies(sceneTypeTo, this);

        sceneTo.Activate();
        action.Invoke();
    }

    //public event EventHandler<EventArgsGeneric<Object>> Changed;

    private void OnChanged(EventTypeEnum eventType, object someObject)
    {
       //var handler = Changed;
       // if(handler != null)
       // {
       //     var args = new EventArgsGeneric<Object>(eventType, someObject);
       //     handler(this, args);
       //}
    }
}

/*public class LoadManager : ILoadManager
{
    public LoadManager(ILogicManager logicManager)
    {
        LogicManager = logicManager;
    }

    public ILogicManager LogicManager { get; private set; }

    public void Navigate(SceneTypeEnum sceneTypeFrom, SceneTypeEnum sceneTypeTo, CustomObject customObject)
    {
        var sceneName = Strings.GetScenePath(sceneTypeTo);


// Load Scene
        _loadOperation = SceneManager.LoadSceneAsync(sceneName);
        _loadOperation.allowSceneActivation = true;

        LogicManager.Coroutiner.StartCoroutine(WaitSceneLoading(sceneTypeTo, customObject,
            () => OnChanged(EventTypeEnum.NavigationEnd, sceneTypeTo)));
    }

    private IEnumerator WaitSceneLoading(SceneTypeEnum sceneTypeTo, CustomObject customObject, Action action)
    {
        var isLoaded = _loadOperation.isDone;
        while (isLoaded == false)
        {
            yield return new WaitForEndOfFrame();
            isLoaded = _loadOperation.isDone;
        }

// Dirty code
        var sceneAttachedScripts = UnityEngine.Object.FindObjectsOfType<BaseScene>();
        IScene sceneTo = sceneAttachedScripts[0];
        sceneTo.SetDependencies(sceneTypeTo, this);

        sceneTo.Activate();

// LogicManager.PoolManager.GetSceneBase(sceneTypeTo);
        action.Invoke();
    }
}*/

