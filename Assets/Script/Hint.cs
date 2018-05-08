using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour {
	public Button HintBT;
	public GameObject[] HintPanel;
	int i;

	void Update(){
		i = KeyControl.GetKeyCon ().GetNumHint ();
	}

	public void OnOffHint(){
		if (HintPanel[i].gameObject.activeSelf == true) {
			HintPanel[i].SetActive (false);
		}
		else if (HintPanel[i].gameObject.activeSelf == false)
			HintPanel[i].SetActive (true);
	}
}
