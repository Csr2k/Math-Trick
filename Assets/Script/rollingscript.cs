using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollingscript : MonoBehaviour {
	public float speed;
	Vector3 startPOS;
	// Use this for initialization
	void Start () {
		startPOS = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate ((new Vector3 (0, -1, 0))*speed*Time.deltaTime);
		if (transform.position.z > -373)
			transform.position = startPOS;
	}
}
