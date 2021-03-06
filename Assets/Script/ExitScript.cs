using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour {
	public Button ExitBT;
	public Button YesBt;
	public Button NoBt;
	public GameObject ConfirmPanel;

    public void Bt_Exit ()
	{
		if (ExitBT.gameObject.activeSelf == true) {
			ConfirmPanel.SetActive (true);
		}
	}

	public void ConfirmYes(){
		if (YesBt.gameObject.activeSelf == true) {
			ConfirmPanel.SetActive (false);
			Application.Quit ();

		}
	}
	public void ConfirmNo(){
		if (NoBt.gameObject.activeSelf == true) {
			ConfirmPanel.SetActive (false);
		}
	}
}
