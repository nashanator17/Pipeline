using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThingAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void MoveDown() {
		transform.position = new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z);
	}
	public void MoveUp() {
		transform.position = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
	}
}
