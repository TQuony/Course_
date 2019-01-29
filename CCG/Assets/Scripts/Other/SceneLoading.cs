using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour {
    [Header("Загружаемая сцена")]
    public string sceneName;
    [Header("Остальные объекты")]
    public Image LoadingImg;
    private void Start()
    {
        sceneName = Managers.gameManager.nameScene;
        StartCoroutine(AsyncLoad());
    }
    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            LoadingImg.fillAmount = progress;
            yield return null;
        }
    }
}
