using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 10f;
        transform.Translate(new Vector3(x, 0, 0));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7, 5), transform.position.y, transform.position.z);
    }
}
