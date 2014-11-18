using UnityEngine;
using System.Collections;

//简单左右移动//
//从出身点开始，向左右移动
[RequireComponent (typeof(Motor2D))]
public class LeftRightPotrol : MonoBehaviour {

	public float leftMax = -5.0f;
	public float rightMax = 5.0f;

	public float startDirection = 1.0f;

	public bool randomDirection = false;

	Motor2D motor;
	//CharacterController2D controller;

	Vector3 spawnPosition;

	void Start(){
		spawnPosition = transform.position;
		motor = GetComponent<Motor2D>();
		//controller = GetComponent<CharacterController2D>();
		startDirection = Random.Range(-2.0f,2.0f);
		dir = startDirection >= 0.0f ? 1.0f : -1.0f;
		transform.localScale = new Vector3( transform.localScale.x * dir, transform.localScale.y, transform.localScale.z );
	}

	float dir;
	
	// Update is called once per frame
	void Update () {
		Vector3 distance = transform.position - spawnPosition;
		if ((distance.x >= rightMax && dir > 0.0f)
		    || (distance.x <= leftMax && dir <0.0f)){
			dir = -dir;
		}
		motor.AddSelfMove(dir,0.0f);
	}
}
