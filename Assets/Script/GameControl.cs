using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	public Transform[] Images_Checkmark;
	protected int[] KeepVal;
	public int[] temp;
	protected int numSelected = 0;
	protected int[] QtSelected;

	static GameControl instance;

	public static GameControl GetInstance(){
		return instance;
	}

	/*-----------------------Set Keep Value Array & Store Value-------------*/
	void Start (){

		KeepVal = new int[23];
		for (int i = 0; i < KeepVal.Length; i++) {
			KeepVal [i] = -1;
		}

		instance = this;
		GameObject.DontDestroyOnLoad (this.gameObject);  //Save value to use in another Script.
	}


	/*-----------------------------Select Sub Menu---------------------------*/
	public void ListOnClick(int val){
		if (temp [val] % 2 == 1) {
			KeepVal [val] = val;
			Images_Checkmark [val].gameObject.SetActive (true);
			temp [val]++;
		} else {
			KeepVal [val] = -1;
			Images_Checkmark [val].gameObject.SetActive (false);
			temp [val]++;
		}
	}

	/*-----------------------------Start Button----------------------------*/
	public void StartGame(){
		int j = 0;
		foreach (int x in KeepVal) {
			if (x > -1) {
				numSelected++;
			} 
		}
		QtSelected = new int[numSelected];
		foreach (int x in KeepVal) {
			if (x > -1) {
				QtSelected [j] = x;
				j++;
			}
		}
		if (numSelected == 0) {
			Debug.Log("No selection");
		} else {
			numSelected = 0;
			SceneManager.LoadScene("Question");
		}
	}

	/*-------------------------Function for Use in another Scene ----------------------*/
	public int[] GetQtset(){
		return QtSelected;
	}

	public void DestroyKeep(){
		for (int i = 0; i < KeepVal.Length; i++) {
			KeepVal [i] = -1;
		}
	}

	public void DestroyGameController(){
		Destroy (this.gameObject);
	}
}
