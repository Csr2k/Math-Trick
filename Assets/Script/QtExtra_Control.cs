using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QtExtra_Control : MonoBehaviour {
	public GameObject Qt_Panel;

	void Start () {
		Qt_Panel.gameObject.SetActive (false);
	}
	
	public void Click_QtExtra() {
		Qt_Panel.gameObject.SetActive (true);
	}

	public void Click_No() {
		Qt_Panel.gameObject.SetActive (false);
	}
}
