using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class SceneMenu : MonoBehaviour {

    [SerializeField] private Button btnPrefab;//Сериализованная переменная для связи с объектом-шаблоном
    [SerializeField] private AudioSource musMenu;
    [SerializeField] private AudioSource musMenu2;
    [SerializeField] private string strMusic;
    
    private ISaveManager _saveManager;


    void Start()
    {
        Managers.Audio.music1 = strMusic;
        Managers.Audio.music1Source = musMenu;
        Managers.Audio.music2Source = musMenu2;
        Managers.Audio.music1Source.ignoreListenerVolume = true;
        Managers.Audio.music2Source.ignoreListenerVolume = true;
        Managers.Audio.music1Source.ignoreListenerPause = true;
        Managers.Audio.music2Source.ignoreListenerPause = true;
        Managers.Audio.soundVolume = 1f;
        Managers.Audio.musicVolume = 1f;
        Managers.Audio._activeMusic = Managers.Audio.music1Source; // Инициализируем один из источников как активный.
        Managers.Audio._inactiveMusic = Managers.Audio.music2Source;

        Managers.Audio.PlayMusic1();

        _saveManager.btnContinue = btnPrefab;
        _saveManager.CheckLoad();
    }

    public void SetDependecies(ISaveManager saveManager)
    {
        _saveManager = saveManager;
    }
}
