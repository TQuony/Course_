using System.Collections.Generic;

public static class Strings
{
    private static IDictionary<SceneTypeEnum, string> _sceneNameStorage;

    static Strings()
    {
        _sceneNameStorage = new Dictionary<SceneTypeEnum, string>();
        InitSceneStorage();
    }

    private static void InitSceneStorage()
    {
        _sceneNameStorage.Add(SceneTypeEnum.Game, "Game");
        _sceneNameStorage.Add(SceneTypeEnum.Menu, "Menu");
    }

    public static object GetScenePath(SceneTypeEnum sceneTypeTo)
    {
        
        return sceneTypeTo == SceneTypeEnum.Menu;
    }
}