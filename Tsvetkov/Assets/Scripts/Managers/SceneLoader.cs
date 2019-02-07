using InterfaceNamespace;
using UnityEngine.SceneManagement;


public class SceneLoader : ISceneLoader {

    public void LoadScene (SceneTypeEnum sceneType) {
		
        SceneManager.LoadScene("Game");
    }
}