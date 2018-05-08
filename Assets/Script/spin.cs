using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour {
	public int Spin;
	void Start(){
		transform.Rotate (new Vector3 (0, 0, Spin) * Time.deltaTime);
	}
	void Update () {

		transform.Rotate (new Vector3 (0, 0, Spin) * Time.deltaTime);
		
	}
}
