using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setTxt : MonoBehaviour {

    public Text txtCard;
	public void CountCard(string str)
    {
        txtCard.text = str;
    }
}
