using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pausedScript : MonoBehaviour {

	public GameObject HiddenPanel;

	public void SettingGame (bool TF) {
		HiddenPanel.SetActive (TF);
	}
}