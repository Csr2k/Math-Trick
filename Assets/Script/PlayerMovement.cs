using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	Vector3 startPOS;
	static PlayerMovement Alien;
	float AlienZ;
	public float Z;
	//private Animator animator;

	public static PlayerMovement GetAlien(){
		return Alien;
	}

	void Start () {
		startPOS = transform.position;
		Alien = this;
		//animator = GetComponent<Animator> ();
	}

	void Update () {
		transform.Translate (new Vector3 (0, 0, Z) * Time.deltaTime);
		AlienZ = transform.position.z;
	}

	public Vector3 GetstartPos(){
		return startPOS;
	}

	public float GetAlienZ(){
		Debug.Log (AlienZ);
		return AlienZ;
	}
}
