using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnList : MonoBehaviour {
	public GameObject[] ListObject;

	public void ShowContent(int number){
		ListObject [number].gameObject.SetActive (true);
	}

	public void CloseContent(int number){
		ListObject [number].gameObject.SetActive (false);
	}
}
