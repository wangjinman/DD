using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Motor2D))]
public class SimpleFly : MonoBehaviour {
	Motor2D motor;
	
	public float glideDuration = 1.0f;
	public bool flyImmediately = true;

	void Awake () {
		motor = GetComponent<Motor2D>();
	}

	// Use this for initialization
	void Start () {
		if (flyImmediately){
			Fly();
		}
		StartCoroutine(FlyCroutine()); 
	}

	IEnumerator FlyCroutine(){
		while (true){
			yield return new WaitForSeconds(glideDuration);
			Fly();
		}
	}

	void Fly(){
		//Vector3 v = new Vector3(motor.MotorVelocity.x,motor.MotorVelocity.y + 1.0f);
		motor.SetSelfMove(motor.MotorVelocity.x,motor.MotorVelocity.y + 1.0f);
	}
}
