using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMove : MonoBehaviour {

    public RectTransform cardFront;

    private float speed;
    private EditPath PathToFollow;
    private float reachDistance = 2.0f;
    private int CurrentWayPointID = 0;

    Vector3 current_position;
    Vector3 scale_position;

    IEnumerator cardMoved( )            //перемещение карты из колоды
    {
        PathToFollow = Managers.cardManager.playerPath;
        scale_position = transform.localScale;
        CurrentWayPointID = 0;
        while (CurrentWayPointID < PathToFollow.path_objs.Count)
        {
            // делаем переход от текущей позиции к новой
            current_position = PathToFollow.path_objs[CurrentWayPointID].position;
            float distance = Vector3.Distance(current_position, transform.position);
            // тут меняем текущую позицию с учетом скорости и прошедшего с последнего фрейма времени
            // и ждем следующего фрейма
            transform.position = Vector3.MoveTowards(transform.position, current_position, Time.deltaTime * speed);
            scale_position = transform.localScale;
            if (scale_position.x < 2f)
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
        Vector3 end = new Vector3(2f, 2f, 0);
        transform.localScale = end;
        GameObject Hand = Managers.gameManager.Hand;
        transform.SetParent(Hand.transform);
       // transform.GetComponent<Transform>().SetPositionAndRotation( Managers.cardManager.arrayPos[Managers.cardManager.Pos].position, Managers.cardManager.arrayPos[Managers.cardManager.Pos].rotation);
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
