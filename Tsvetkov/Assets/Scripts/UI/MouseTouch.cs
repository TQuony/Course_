using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTouch : MonoBehaviour
{

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates.
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                var hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit))
                {
                    hit.transform.gameObject.SendMessage("OnMouseDown");
                }
                //var hit2d = new RaycastHit2D();
                //hit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, 100f)), Vector2.zero);
                //if (hit2d.collider != null)
                //{
                //    hit2d.transform.gameObject.SendMessage("OnMouseDown");
                //}
            }
        }
    }
}
