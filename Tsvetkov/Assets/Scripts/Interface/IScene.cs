namespace InterfaceNamespace
{
    public interface IScene
    {
          void Activate();
          void SetDependencies(SceneTypeEnum sceneTypeTo, LoadManager loadManager);
    }
}