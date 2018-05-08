using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOffHeader : MonoBehaviour {
	public GameObject[] List;
	public Image[] Arrow;
	public void OnOffListToggle(int id){
		List[id].SetActive (!List[id].activeSelf);
		Arrow [id].transform.Rotate (0, 0, 180);
	}
}