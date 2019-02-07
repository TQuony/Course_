using InterfaceNamespace;
using UnityEngine;
using Object = System.Object;

public class PopupClass:  ISubscriber
{
   //pu GameObject ClassPopup;        //вспылывающее окно информация по классу
   private GameObject _classPopup;
   private  bool _flagPopup;
   private GameObject _classView;
   
   private IPublisher _publisher;
   private CardsGameClass _cardGame;
   public PopupClass(GameObject classPopup,GameObject clasView)
   {
      _classPopup = classPopup;
      _classView = clasView;
      _flagPopup = false;

   }
   private void Start()
   {
       SetDependecies(GameObject.Find("Bootstrapper").GetComponent<Bootstrapper>().publisher);
   }
   public void OnEvent(Object messageData)
   {
      switch (messageData.ToString())
      {
          case "closePopup":
             if(_flagPopup)
             {
                 _classPopup.SetActive(false);
                 _flagPopup = false;
             }
              break;
          case "selectClass":
              if(_flagPopup)
              {
                  Debug.Log("name select = "+_cardGame.cardName);
                  _publisher.Publish("saveClass", _cardGame);
                  _flagPopup = false;
              }
              break;
      }
      
   }

   public void OnEvent(Object messageData, CardsGameClass cardsGameClass)
   {
       switch (messageData.ToString())
       {
           case "openPopup":
               if (!_flagPopup)
               {
                   _cardGame = cardsGameClass;
                   _classPopup.SetActive(true);
                   ActionsWithCard cardDis = _classView.GetComponent<ActionsWithCard>();
                   cardDis.CardSetup(_cardGame);
                   _classView.gameObject.GetComponent<Animator>().SetBool("view", true);
                   _flagPopup = true;
               }
               break;
           /*case "selectClass":
               if(_flagPopup)
               {
                   _publisher.Publish(this,"selectClass", cardsClass);
                   _classPopup.SetActive(false);
                   _flagPopup = false;
               }
               break;*/
       }
   }
   
   public void SetDependecies( IPublisher publisher)
   {
       _publisher = publisher;
   }
}
