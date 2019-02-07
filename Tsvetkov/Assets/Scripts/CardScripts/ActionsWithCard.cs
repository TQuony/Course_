using InterfaceNamespace;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionsWithCard : MonoBehaviour , ICard, IPointerDownHandler{

	public Image cardImage;
	public Image cardFace;
	
	private IPublisher _publisher;
	
	[SerializeField]
	private Image _cardDown;
	private CardsGameClass _cardGame;

	void Start()
	{
		SetDependecies(GameObject.Find("Bootstrapper").GetComponent<Bootstrapper>().publisher);
	}
	public void CardSetup<T>(T thisCard)
	{
		_cardGame = thisCard as CardsGameClass;
		if (_cardGame != null)
		{
			cardImage.sprite = _cardGame.art;
			cardFace.sprite = _cardGame.face;
		}
	}

	public void OnPointerDown(PointerEventData pointerEventData)
	{
		if (!_cardDown.IsActive())
		{
			_publisher.Publish(this,"openPopup", _cardGame);
			
		}
	}
	public void SetDependecies(IPublisher publisher)
	{
		_publisher = publisher;
	}
}
