using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerScore{
	public string playerName;
	public int playerScore;

	public PlayerScore (string playerName, int playerScore){
		this.playerName = playerName;
		this.playerScore = playerScore;
	}

	public string GetFormat(){
		return playerName + "~S~" + playerScore;
	}
}

public class ScoreBoard : MonoBehaviour {
	public int scoreCount = 10;
	private int sc;

	[Header("SAVE PANEL")]
	public InputField inputName;

	[Header("SCORE DISPLAY")]
	public GameObject scoreObject;
	public GameObject scoreRoot;
	public Text textName, textScore;

	static ScoreBoard scoreBoard;
	static string separator = "~S~";

	public static ScoreBoard GetScoreBoard(){
		return scoreBoard;
	}

	// Use this for initialization
	void Start () {
		scoreBoard = this;
	}

	/*------------------------------Save Score Now---------------------------------*/
	public void SaveScoreNow(){
		sc = KeyControl.GetKeyCon().GetSc ();
		//print (sc.ToString ());
		SaveScore (inputName.text, sc);
	}

	/*------------------------------Save Score Function----------------------------*/
	public static void SaveScore(string name, int score){
		List<PlayerScore> playerScores = new List<PlayerScore> ();
		for (int i = 0; i < scoreBoard.scoreCount; i++) {
			if (PlayerPrefs.HasKey ("Score" + i)) {
				string[] scoreFormat = PlayerPrefs.GetString ("Score" + i).Split (new string[]{ separator }, System.StringSplitOptions.RemoveEmptyEntries);
				playerScores.Add (new PlayerScore (scoreFormat [0], int.Parse (scoreFormat [1])));
			} else {
				break;
			}
		}
		if (playerScores.Count < 1) { 
			PlayerPrefs.SetString ("Score0", name + separator + score);
			print ("Csr2k");
			return;
		}

		playerScores.Add (new PlayerScore (name, score));
		playerScores = playerScores.OrderByDescending (o => o.playerScore).ToList ();

		for (int i = 0; i < scoreBoard.scoreCount; i++) {
			if (i >= playerScores.Count) {
				break;
			}
			PlayerPrefs.SetString ("Score" + i, playerScores [i].GetFormat ());
		}
	}

	/*--------------------------------Get Score Funtion-------------------------------*/

	public List<PlayerScore> GetScore(){
		List<PlayerScore> playerScores = new List<PlayerScore> ();
		for (int i = 0; i < scoreBoard.scoreCount; i++) {
			if (PlayerPrefs.HasKey ("Score" + i)) {
				string[] scoreFormat = PlayerPrefs.GetString ("Score" + i).Split (new string[]{ separator }, System.StringSplitOptions.RemoveEmptyEntries);
				playerScores.Add (new PlayerScore (scoreFormat [0], int.Parse (scoreFormat [1])));
			} else {
				break;
			}
		}
		return playerScores;
	}

	/*-----------------------------Show Score--------------------------------------*/
	public void ShowScore()
	{
		StartCoroutine (CoShowScore());
	}

	/*------------------------------Enumelator Function------------------------------*/
	IEnumerator CoShowScore(){
		while (scoreRoot.transform.childCount > 0) {
			Destroy (scoreRoot.transform.GetChild (0).gameObject);
			yield return null;
		}

		List<PlayerScore> playerScore = GetScore ();
		foreach (PlayerScore score in playerScore) {
			textName.text = score.playerName;
			textScore.text = score.playerScore.ToString();

			GameObject instantiatedScore = Instantiate (scoreObject);
			instantiatedScore.SetActive (true);

			instantiatedScore.transform.SetParent (scoreRoot.transform,false);
		}
	}

}
