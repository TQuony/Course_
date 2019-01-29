using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//скрипт вылетающий текст
public class moveTxtDamage : MonoBehaviour {

	private bool move;
	private Vector2 randomVector;
    private float speed = 1f;
    private Vector3 pointEnemy = new Vector3(680f, 70f, 1.5f);
    private Vector3 pointPlayer = new Vector3(-600f, 70f, 1.5f);

    private void Update()
    {
	    if(!move) return;
        transform.Translate(randomVector * Time.deltaTime*speed);

    }
	public void startMove(int damage){
        if (Managers.Player.playertTurn) //если ход игрока, одни правила
        {
            if (damage < 0)
            {
                transform.localPosition = pointEnemy;
                GetComponent<Text>().text = "" + damage;
                GetComponent<Text>().color = new Color32(180, 0, 0, 255);//красноватый
                randomVector = new Vector2(Random.Range(-20, 20), Random.Range(90, 100));
            }
            if (damage > 0)
            {
                transform.localPosition = pointPlayer;
                GetComponent<Text>().text = "+" + damage;
                GetComponent<Text>().color = new Color32(0, 180, 0, 255);//зеленый
                randomVector = new Vector2(Random.Range(-20, 20), Random.Range(90, 100));
            }
        }
        else //если ход Enemy, другие правила
        {
            if (damage < 0)
            {
                transform.localPosition = pointPlayer;
                GetComponent<Text>().text = "" + damage;
                GetComponent<Text>().color = new Color32(180, 0, 0, 255);//красноватый
                randomVector = new Vector2(Random.Range(-20, 20), Random.Range(90, 100));
            }
            if (damage > 0)
            {
                transform.localPosition = pointEnemy;
                GetComponent<Text>().text = "+" + damage;
                GetComponent<Text>().color = new Color32(0, 180, 0, 255);//зеленый
                randomVector = new Vector2(Random.Range(-20, 20), Random.Range(90, 100));
            }
        }
        move = true;
        GetComponent<Animation>().Play();
    }
    public void stopMove()
    {
        move = false;
    }
}
