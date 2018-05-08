using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyControl : MonoBehaviour{
	[Header("TIME & QUESTION")]
	public Text ShowAns;
	public Text Questions;
	public Text Score;
	public long Ans = -1;
	private int QtLength;
	private int Answer;
	private int Sc = 0;
	public Text Showtime;
	public float time;
	public Text ShowNumOfQt;
	public int countQt;
	private int[] QtSet;
	private int n;
	private string CurrQt,LastQt;

	[Header("SUBMIT SCORE")]
	public GameObject SubmitPanel;
	public Text ShowScore;

	[Header("ALIEN & BOOM,MINUS TEXT")]
	public GameObject Alients;
	Vector3 AlientPST;
	public ParticleSystem BoomText;
	public ParticleSystem MinusText;
	public ParticleSystem MinusText2;

	[Header("HINT")]
	private int j;

	/*--------------------------Start & Call function from another script-------------*/
	static KeyControl KeyConClass;
	public static KeyControl GetKeyCon(){
		return KeyConClass;
	}

	void Start(){
		BoomText.Stop ();
		MinusText.Stop ();
		MinusText2.Stop ();
		GameObject[] obj = GameObject.FindGameObjectsWithTag ("Music");
		Destroy (obj[0]);
		QtSet = GameControl.GetInstance ().GetQtset ();
		n = Random.Range (0, QtSet.Length);
		CurrQt = Qt (QtSet [n]);
		LastQt = CurrQt;
		Questions.text = CurrQt;

		Debug.Log (n.ToString());

		countQt = 0;
		Time.timeScale = 1;
		KeyConClass = this;
	}
		
	/*-------------------------------Get Variable from GameControl.cs & Set Time-----------------*/
	void Update(){
		time += Time.deltaTime;
		Showtime.text = "Time: " + (30-Mathf.Round (time));

		//If No Answer
		if (time > 30) {
			time = 0;
			Ans = -2;
			CheckAns ();
		}

		if (countQt == 25) {
			Time.timeScale = 0;
			SubmitPanel.gameObject.SetActive (true);

			ShowScore.text = "คะแนนของคุณ คือ " + Sc + " !";
		}

		GameControl.GetInstance ().DestroyKeep ();
	}

	/*------------------------------Destroy Valiable : When Press Back | Home ----------------------------*/
	public void DestroySelectedVal(){
		GameControl.GetInstance ().DestroyGameController ();
	}

	//  Send Score to ScoreBoard.cs
	public int GetSc(){
		return Sc;
	}

	// Send time to PlayerMovement.cs
	public float GetTime(){
		return time;
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
		if (Ans != -1) {
			//Wrong Answer
			if (Ans != Answer && Ans != -2) {
				if (Sc != 0) {
					Sc = Sc - 5;
				}
				AudioSound.PlaySound ("Wrong");
				MinusText2.Play ();
				Ans = -1;
				ShowAns.text = "";
				Score.text = "Score: " + Sc;
			} else {
				//Right Answer
				if (Ans == Answer) {
					Sc = Sc + (30 - int.Parse (Mathf.Round (time).ToString ()));
					BoomText.Play ();
					AudioSound.PlaySound ("Shoot");
					Alients.gameObject.SetActive (false);
				}

				//No Answer
				if (Ans == -2) {
					if (Sc != 0) {
						Sc = Sc - 15;
					}
					AudioSound.PlaySound ("Damaged");
					MinusText.Play ();
				}

				time = 0;
				countQt++;
				StartCoroutine (WaitToStart ()); 

				if (countQt <= 25) {
					n = Random.Range (0, QtSet.Length);
					Debug.Log (n.ToString ());
					CurrQt = Qt (QtSet [n]);
					while (CurrQt == LastQt) {
						CurrQt = Qt (QtSet [n]);
					}
					Questions.text = CurrQt;
					Ans = -1;
					ShowAns.text = "";
					Score.text = "Score: " + Sc;
					if (countQt < 25) {
						ShowNumOfQt.text = (countQt + 1) + "/25";
					}
				}
			}
		} else {  //Not Answer and Click Shoot
			ShowAns.text = "กรุณากดตัวเลขก่อนกดปุ่มยิง";
		}
	}

	/*-----------------------------------Wait for Second Funtion--------------------------*/
	IEnumerator WaitToStart(){
		Debug.Log("Waiting..");
		yield return new WaitForSecondsRealtime (1);
		Alients.gameObject.SetActive (true);
		Alients.transform.position = PlayerMovement.GetAlien ().GetstartPos ();
	}

	/*-----------------------------------Questions Set------------------------------------*/

	//Switch Case
	protected string Qt(int n){
		int num1,num2;
		string qt;
		switch (n) {
		case 0: 								//Plus 1 Digit
			num1 = Random.Range (1, 9);
			num2 = Random.Range (1, 9);
			qt = num1 + "+" + num2 + "=?";
			Answer = num1 + num2;
			j = n;
			return qt;
		case 1: 								//Plus 2 Digits
			num1 = Random.Range (10, 99);
			num2 = Random.Range (1, 99);
			qt = num1 + "+" + num2 + "=?";
			Answer = num1 + num2;
			j = n;
			return qt;
		case 2: 								//Plus 3 Digits
			num1 = Random.Range (100, 999);
			num2 = Random.Range (1, 999);
			qt = num1 + "+" + num2 + "=?";
			Answer = num1 + num2;
			j = n;
			return qt;
		case 3: 								//Plus with Minus
			num1 = (Random.Range (1, 9)*100)+Random.Range (90, 99);
			num2 = Random.Range (11, 999);
			qt = num1 + "+" + num2 + "=?";
			Answer = num1 + num2;
			j = n;
			return qt;
		case 4: 								//Summation 10 value.
			num1 = Random.Range (1, 999);

			qt = num1+"+"+(num1+1)+"+"+(num1+2)+"+"+(num1+3)+"+"+(num1+4)+
				"+"+(num1+5)+"+"+(num1+6)+"+"+(num1+7)+"+"+(num1+8)+"+"+(num1+9);
			Answer = num1+(num1+1)+(num1+2)+(num1+3)+(num1+4)+(num1+5)+(num1+6)+(num1+7)+(num1+8)+(num1+9);
			j = n;
			return qt;
		case 5: 								//Minus 1 digit
			num1 = Random.Range (1, 9);
			num2 = Random.Range (1, 9);
			while (num1<num2) {
				num1 = Random.Range (1, 9);
				num2 = Random.Range (1, 9);
			}
			qt = num1 + "-" + num2 + "=?";
			Answer = num1 - num2;
			j = n;
			return qt;
		case 6: 								//Minus 2 digits
			num1 = Random.Range (10, 99);
			num2 = Random.Range (1, 99);
			while (num1<num2) {
				num1 = Random.Range (10, 99);
				num2 = Random.Range (1, 99);
			}
			qt = num1 + "-" + num2 + "=?";
			Answer = num1 - num2;
			j = n;
			return qt;
		case 7: 								//Minus 3 digits
			num1 = Random.Range (100, 999);
			num2 = Random.Range (1, 999);
			while (num1<num2) {
				num1 = Random.Range (100, 999);
				num2 = Random.Range (1, 999);
			}
			qt = num1 + "-" + num2 + "=?";
			Answer = num1 - num2;
			j = n;
			return qt;
		case 8: 								//Minus with Plus
			num1 = Random.Range (100, 999);
			num2 = (Random.Range (1, 9)*100)+90+Random.Range (1, 9);
			while (num1<num2) {
				num1 = Random.Range (100, 999);
				num2 = (Random.Range (1, 9)*100)+90+Random.Range (1, 9);
			}
			qt = num1 + "-" + num2 + "=?";
			Answer = num1 - num2;
			j = n;
			return qt;
		case 9: 								//Multiply 1 Digit
			num1 = Random.Range (1, 9);
			num2 = Random.Range (1, 9);
			qt = num1 + "x" + num2 + "=?";
			Answer = num1 * num2;
			j = n;
			return qt;
		case 10: 								//Multiply 2 Digits
			num1 = Random.Range (10, 99);
			num2 = Random.Range (1, 99);
			qt = num1 + "x" + num2 + "=?";
			Answer = num1 * num2;
			j = n;
			return qt;
		case 11: 								//Multiply 2 Digits bet. 10-20
			num1 = Random.Range (11, 19);
			num2 = Random.Range (11, 19);
			qt = num1 + "x" + num2 + "=?";
			Answer = num1 * num2;
			j = n;
			return qt;
		case 12: 								//Multiply 2 Digits ABxAC; B+C =10
			num1 = 10*Random.Range (1, 9);
			num2 = Random.Range (1, 9);
			qt = num1+num2 + "x" + (num1+10-num2) + "=?";
			Answer = (num1+num2)*(num1+10-num2);
			j = n;
			return qt;
		case 13: 								//Multiply numeric bet. 90 -110
			num1 = Random.Range (91, 109);
			num2 = Random.Range (91, 109);
			qt = num1+ "x" + num2 + "=?";
			Answer = num1*num2;
			j = n;
			return qt;
		case 14: 								//Multiply by 11
			num1 = Random.Range (10, 99);
			num2 = 11;
			qt = num1+ "x" + num2 + "=?";
			Answer = num1*num2;
			j = n;
			return qt;
		case 15: 								//Multiply by 5,50,500
			num1 = Random.Range (11, 999);
			num2 = 5*Mathf.RoundToInt(Mathf.Pow(10,Random.Range(0,2)));
			qt = num1+ "x" + num2 + "=?";
			Answer = num1*num2;
			j = n;
			return qt;
		case 16: 								//Multiply by 999
			num1 = Random.Range (101, 999);
			num2 = 999;
			qt = num1+ "x" + num2 + "=?";
			Answer = num1*num2;
			j = n;
			return qt;
		case 17: 								//Power 2
			num1 = Random.Range (10, 99);
			qt = num1+ "x" + num1 + "=?";
			Answer = num1*num1;
			j = n;
			return qt;
		case 18: 								//Power 2; End with 5
			num1 = 10*Random.Range (1, 99);
			num2 = 5;
			qt = (num1+num2)+ "x" + (num1+num2) + "=?";
			Answer = (num1+num2)*(num1+num2);
			j = n;
			return qt;
		case 19: 								//Divide by 10,100,1000
			num1 = 100*Random.Range (1, 99);
			num2 = Mathf.RoundToInt(Mathf.Pow(10,Random.Range (1, 2)));
			while (num1<num2 && num1%num2==0) {
				num1 = 100*Random.Range (1, 99);
				num2 = Mathf.RoundToInt(Mathf.Pow(10,Random.Range (1, 2)));
			}
			qt = num1 + "÷" + num2 + "=?";
			Answer = num1/num2;
			j = n;
			return qt;
		case 20: 								//Divide by 5,50,500
			num1 = 50*Random.Range (1, 99);
			num2 = 5*Mathf.RoundToInt(Mathf.Pow(10,Random.Range (1, 2)));
			while (num1<num2 && num1%num2==0) {
				num1 = 50*Random.Range (1, 99);
				num2 = 5*Mathf.RoundToInt(Mathf.Pow(10,Random.Range (1, 2)));
			}
			qt = num1+ "÷" + num2 + "=?";
			Answer = num1/num2;
			j = n;
			return qt;
		case 21: 								//Divide by 25
			num1 = 25*Random.Range (1, 99);
			qt = num1+"÷"+"25=?";
			Answer = num1/25;
			j = n;
			return qt;
		case 22: 								//Divide by 9
			num1 = 9 * Random.Range (1, 99);
			qt = num1 + "÷" + "9=?";
			Answer = num1 / 9;
			j = n;
			return qt;
		default:
			return "Failed";
		}
	}

	public int GetNumHint(){
		return j;
	}

}
