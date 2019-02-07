
using DefaultNamespace;
using InterfaceNamespace;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour, IScene
{
   protected ILoadManager _loadManager;
   private IScene _sceneImplementation;

   public void SetDependencies(SceneTypeEnum sceneType, ILoadManager loadManager)
   {
      _loadManager = loadManager;
      SceneType = sceneType;
   }

   public SceneTypeEnum SceneType { get; private set; }

   //public abstract void OnEntry(CustomObject customObject);
   public void Activate()
   {
      
   }

   public void SetDependencies(SceneTypeEnum sceneTypeTo, LoadManager loadManager)
   {
      _sceneImplementation.SetDependencies(sceneTypeTo, loadManager);
   }
}
