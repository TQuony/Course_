using UnityEngine;
using System;
using InterfaceNamespace;

public class SaveManager : ISaveManager //
{
    private Save sv = new Save();

    public bool CheckLoad()                 //проверим сохранение
    {
        if (!PlayerPrefs.HasKey("Save"))
        { 
             return false;
        }
        else
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
            if (sv.nameClass != "")
            {
                return true;
            }
            else
                return false;
        }
    }
    public void SetClass(CardsGameClass cardGame)
    {
        sv.nameClass = cardGame.cardName;
        sv.id = cardGame.id;
    }

    public void SaveGame()
    {
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv));
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }
    [Serializable]
    public class Save
    {
        public string nameClass;    //название класса
        public int id;              //ид класса
    }

    public void OnEvent(object messageData)
    {
        //throw new NotImplementedException();
    }

    public void OnEvent(object messageData, CardsGameClass cardsGameClass)
    {
        switch (messageData.ToString())
        {
            case "saveClass":
                SetClass(cardsGameClass);
                break;
        }
    }
}
