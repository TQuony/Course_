using UnityEngine;

//добавляем функцию создания в CreateAssetMenu 
[CreateAssetMenu(fileName = "New Card", menuName = "CardsGameClass")]
public class CardsGameClass : ScriptableObject
{
	public int id;
	public string cardName;
	public Sprite art;
	public Sprite face;//лицевая сторона

}