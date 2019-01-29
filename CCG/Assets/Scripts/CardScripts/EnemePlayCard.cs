using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemePlayCard : MonoBehaviour
{
    public int CurrentWayPointID = 0;
    public RectTransform cardFront;
    public RectTransform cardBack;

    private float speed;
    private EditPath PathToFollow;
    private EditPath pathHand;
    private float reachDistance = 7.0f;
    Vector3 current_position;
    Vector3 scale_position;
    private Card card;

    IEnumerator cardMoved()            //перемещение карты из колоды Enemy
    {
        PathToFollow = Managers.cardManager.enemyPath;
        scale_position = transform.localScale;
        CurrentWayPointID = 0;
        while (CurrentWayPointID < PathToFollow.path_objs.Count)
        {
            current_position = PathToFollow.path_objs[CurrentWayPointID].position;
            float distance = Vector3.Distance(current_position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, current_position, Time.deltaTime * speed);
            cardFront.gameObject.SetActive(true);
            cardBack.gameObject.SetActive(false);
            scale_position = transform.localScale;
            if (scale_position.x < 2.2f)
            {
                scale_position.x += Time.deltaTime;
                scale_position.y += Time.deltaTime;
                transform.localScale = scale_position;
            }
            if (distance <= reachDistance - 1)
            {
                CurrentWayPointID++;
            }
            yield return null;
        }
        Vector3 endScale = new Vector3(2.4f, 2.4f, 0);
        transform.localScale = endScale;
        transform.SetParent(Managers.gameManager.ActiveZone.transform);
        yield return new WaitForSeconds(0.4f);
        
        card = (this.gameObject.GetComponent<CardDisplay>()).card;
        if (card.damage < 0)
        {      //если damage карты < 0, ударить врага на это значение
            
            Managers.gameManager.changeHpPlayer(card.damage);
        }
        if (card.damage > 0)
        {      //если damage карты > 0, карта хилит игрока
            Managers.gameManager.changeHpEnemy(card.damage);
        }
        gameObject.GetComponent<Dissolve>().StartDissolve();
    }
    IEnumerator cardInEnemyHand()            //перемещение карты из колоды Enemy
    {
        pathHand = Managers.cardManager.enemyPathHand;
        scale_position = transform.localScale;
        CurrentWayPointID = 0;
        while (CurrentWayPointID < pathHand.path_objs.Count)
        {
            current_position = pathHand.path_objs[CurrentWayPointID].position;
            float distance = Vector3.Distance(current_position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, current_position, Time.deltaTime * speed);
            scale_position = transform.localScale;
            if (scale_position.x < 0.5f)
            {
                scale_position.x += Time.deltaTime;
                scale_position.y += Time.deltaTime;
                transform.localScale = scale_position;
            }
            if (distance <= reachDistance)
            {
                CurrentWayPointID++;
            }
            yield return null;
        }
        Vector3 endScale = new Vector3(0.5f, 0.5f, 0);
        transform.localScale = endScale;
        GameObject Hand = Managers.gameManager.enemyHand;
        transform.SetParent(Hand.transform);
    }
    public float Speed
    {
        set
        {
            speed = value;
        }
    }
    public int StartPoint
    {
        set
        {
            CurrentWayPointID = value;
        }
    }
}
