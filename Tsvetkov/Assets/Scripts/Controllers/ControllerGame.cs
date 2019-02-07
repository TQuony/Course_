using System.Collections;
using DefaultNamespace;
using UnityEngine;
using InterfaceNamespace;


public class ControllerGame : BaseScene
{

    private ISaveManager  _saveManager;
    private IPublisher    _publisher;
    private IAnimaManager _animaManager;

    private CardsGameClass[] _allcards;        //Все карты классов (информация)
    private GameObject[] _cardInGame;      //Карты в игре  
    private GameObject[] _cardClassPrefab; //префаб карты Класса

    private GameObject _deck;              //колода на столе
    private GameObject _popupClass;        //вспылывающее окно информация по классу
    
    private PopupClass _classPopup;


    void Start()
    {
        /*SetDependecies(GameObject.Find("Bootstrapper").GetComponent<Bootstrapper>().saveManager,
            GameObject.Find("Bootstrapper").GetComponent<Bootstrapper>().animaManager ,
            GameObject.Find("Bootstrapper").GetComponent<Bootstrapper>().publisher  );*/

        _deck = (GameObject.Find("Deck"));
        _popupClass = (GameObject.Find("PopupClass"));
        _popupClass.transform.localPosition = new Vector3(0f,0f,0f);
        _popupClass.SetActive(false);

        if (_saveManager.CheckLoad())
        {
            Debug.Log("Continue game");
        }
        else
        {
            Debug.Log("New game");
            StartCoroutine("SpawnCardClass"); //заспавним 3 карты класссов
        }
    }

    IEnumerator SpawnCardClass()
    {
        _cardClassPrefab = Resources.LoadAll<GameObject>("Prefabs/CardPrefab");
        _allcards = Resources.LoadAll<CardsGameClass>("CardsClass/");
        _cardInGame = new GameObject[_allcards.Length];
        //---------------добавляю карты на сцену
        for (int i = 0; i < _allcards.Length; i++)
        {
            _cardInGame[i] = Instantiate(_cardClassPrefab[0], _deck.transform, false);
            _cardInGame[i].transform.localPosition = new Vector3(0,0f,0f);
            ActionsWithCard cardDis = _cardInGame[i].GetComponent<ActionsWithCard>();
            cardDis.CardSetup(_allcards[i]);
            _animaManager.SetStateAnima(_cardInGame[i], "dist_num", i + 1);
            yield return new WaitForSeconds( 0.5f);
        }
        //добавляю карту на панель описания класса
        GameObject card = Instantiate(_cardClassPrefab[0], _popupClass.transform, false);
        card.transform.localPosition = new Vector3(-620f,10f,0f);
        card.transform.localScale = new Vector3(1.5f,1.5f,0f);
        
        _classPopup = new PopupClass (_popupClass, card);
        _publisher.AddSubscriber(_classPopup);
        
    }   
    
    public void SetDependecies(ISaveManager saveManager, IAnimaManager animaManager, IPublisher publisher)
    {
        _saveManager = saveManager;
        _animaManager = animaManager;
        _publisher = publisher;
    }

    // TODO Implement in future version
    /*public override void OnEntry(CustomObject customObject)
    {
        
    }*/
}




