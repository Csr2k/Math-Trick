using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScoreScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ScoreBoard.GetScoreBoard ().ShowScore ();
	}
	
}
