using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Qt_Extra : MonoBehaviour {

	[Header("TIME & QUESTION")]
	public Transform[] Qt;
	public Text ShowAns;
	public Text Showtime;
	private float time;
	public long Ans = -1;
	private int Answer;
	public int countQt;
	public int[] Arr;
	private int n,j;
	public Transform ShowQt;

	[Header("ALIEN & BOOM,MINUS TEXT")]
	public GameObject Alients;
	Vector3 AlientPST;
	public ParticleSystem BoomText;

	[Header("END GAME")]
	public GameObject SubmitPanel;
	public Text ShowTime,ShowText;

	void Start () {
		GameObject[] obj = GameObject.FindGameObjectsWithTag ("Music");
		Destroy (obj[0]);
		Arr = new int[3];
		for (int i = 0; i < Arr.Length; i++) {
			Arr [i] = -1;
		}
		n = Random.Range (0, Qt.Length);
		Qt [n].gameObject.SetActive (true);
		j = 0;
		Arr [j] = n;
		Debug.Log (n.ToString ());
		Answer = QtAns (n);
		BoomText.Stop ();
		countQt = 0;
		Time.timeScale = 1;
		time = Time.time;
	}
	
	void Update () {

		float timer = Time.time - time;
		string minutes = ((int)timer / 60).ToString ();
		string seconds = (timer % 60).ToString ("f2");
		if (minutes == "0") {
			Showtime.text = "Time: " + seconds + " s ";
		} else {
			Showtime.text = "Time: " + minutes + " m " + seconds + " s ";
		}
		Answer = QtAns (n);

		//If No Answer
		if (minutes == "3") {
			Time.timeScale = 0;
			Qt [n].gameObject.SetActive (false);
			SubmitPanel.gameObject.SetActive (true);
			ShowText.text = "ว้า หมดเวลาแล้วสิ พยายามอีกหน่อยนะ";
		}

		if (countQt == 3) {
			Time.timeScale = 0;
			Qt [n].gameObject.SetActive (false);
			SubmitPanel.gameObject.SetActive (true);
			ShowText.text = "ว้าว! ยอดเยี่ยมไปเลย";
			ShowTime.text = "เวลาที่ใช้ " + minutes + " นาที " + seconds + " วินาที";
		}
	}
		
	/*-------------------------------Show User Answer---------------------------------*/
	public void ShowUserAns(int num){
		if (Ans != -1) {
			if (Ans < 9999999999) {
				if (Ans != 0) {
					Ans = (Ans * 10) + num;
					ShowAns.text = Ans.ToString ();
				} else {
					Ans = num;
					ShowAns.text = Ans.ToString ();
				}
			} else {
				ShowAns.text = "ผิดพลาด! กรุณากดปุ่มลบ";
			}
		} else {
			Ans = 0;
			ShowUserAns (num);
		}
	}
	/*------------------------------Clear & Shoot Button------------------------------------*/
	public void ClearAns(){
		if (Ans != -1) {
			Ans = Ans / 10;
			if (Ans == 0) {
				Ans = -1;
				ShowAns.text = "";
			} else {
				ShowAns.text = Ans.ToString ();
			}
		}
	}

	public void CheckAns(){
		if (Ans != -1) { //Answer and Click Shoot
			//If Wrong Answer
			if (Ans != Answer && Ans != -2) {
				Ans = -1;
				ShowAns.text = "";
				AudioSound.PlaySound ("Wrong");
			} else {
				//If Right Answer
				if (Ans == Answer) {
					BoomText.Play ();
					AudioSound.PlaySound ("Shoot");
					Qt [n].gameObject.SetActive (false);

				}
				countQt++;
				if (countQt <= 2) {
					n = Random.Range (0, Qt.Length);
					Debug.Log ("สุ่ม ได้ " + n.ToString ());
					while (n == Arr [0] || n == Arr [1]) {
						n = Random.Range (0, Qt.Length);
						Debug.Log ("สุ่มอีกที "+n.ToString ());
					}
						j += 1;
					Arr [j] = n;
					Qt [n].gameObject.SetActive (true);
					Ans = -1;
					ShowAns.text = "";

				}
			}
		} else {  //Not Answer and Click Shoot
			ShowAns.text = "กรุณากดตัวเลขก่อนกดปุ่มยิง";
		}
	}

	public int QtAns(int num){
		int ans;
		switch (num) {
		case 0:
			ans = 3025;
			return ans;
		case 1:
			ans = 1125;
			return ans;
		case 2:
			ans = 240;
			return ans;
		case 3:
			ans = 13750;
			return ans;
		case 4:
			ans = 3261;
			return ans;
		case 5:
			ans = 850;
			return ans;
		case 6:
			ans = 6175;
			return ans;
		case 7:
			ans = 10;
			return ans;
		case 8:
			ans = 49;
			return ans;
		default:
			return 0;
		}
	}
}
