using System.Collections;

using System.Collections.Generic;
using InterfaceNamespace;
using UnityEngine;


public class Bootstrapper : MonoBehaviour
{
    private ISaveManager  saveManager;
    private IGameManager  gameManager;
    private IAnimaManager animaManager;
    private ILoadManager loadManager;

    private ILogicManager _logicManager;
    
    public Publisher publisher;
   

    void Start()
   {
       DontDestroyOnLoad(gameObject);             // Команда Unity для сохранения объекта между сценами.
       
       publisher = new Publisher();
       saveManager = new SaveManager();
       gameManager = new GameManager();
       animaManager = new AnimaManager();
       //logicManager = new LogicManager();
       //loadManager = new LoadManager(_logicManager);
       
       var menuObject = GameObject.FindWithTag("SceneController");
       ControllerMenu controllerMenu = menuObject.GetComponent<ControllerMenu>();
       if (controllerMenu == null)
       {
           throw new UnityException("There is no ControllerMenu script on UI object");
       }
       
       var buttonObject = GameObject.Find("btnStart");
       MouseClickHandler clickHandler = buttonObject.GetComponent<MouseClickHandler>();
       if (clickHandler == null)
       {
           throw new UnityException("There is no MouseClickHandler script on UI object");
       }

       UiButtonsMenu uiButtonsMenu = buttonObject.GetComponent<UiButtonsMenu>();
       if (clickHandler == null)
       {
           throw new UnityException("There is no MouseClickHandler script on UI object");
       }
       
       // Set dependencies
       clickHandler.SetDependecies(animaManager);
       uiButtonsMenu.SetDependecies(saveManager,animaManager);
       controllerMenu.SetDependecies(saveManager);
       publisher.AddSubscriber(saveManager);
       
   }
   public void OnApplicationQuit()                //Сохраняем при выходе из игры на PC
   {
       saveManager.SaveGame();
   }
       public void OnApplicationPause(bool pause) //Сохраняем при выходе из игры на Android
    {
         if (pause) 
         {
             saveManager.SaveGame();
         } 
    }
}