using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour {


	[SerializeField]

	private Transform player;

	float cameraX;
	float cameraZ;

	public float y = 10;
	public float z = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (player) {

			cameraX = player.position.x;
			cameraZ = player.position.z;
			this.transform.position = new Vector3(cameraX,y,cameraZ + z);
		
		}
	}
}
