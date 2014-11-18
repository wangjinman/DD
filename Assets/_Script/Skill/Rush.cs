using UnityEngine;
using System.Collections;

public class Rush : SkillBase {

	public Vector2 velocity;
	
	public float rushTime = 1.0f;

	float rushTimer;
	bool run;

	float orgHorzentalSpeed;
	float orgVerticalSpeed;

	InputManager playerInput;

	public override void OnUpdate(){
		if (run){
			OnRush();
		}
	}

	public override void OnUse(){
		rushTimer = rushTime;
		run = true;
		orgHorzentalSpeed = user.motor.horzentalSpeed;
		orgVerticalSpeed = user.motor.verticalSpeed;
		user.motor.horzentalSpeed = velocity.x;
		user.motor.verticalSpeed = velocity.y;
		//motor.AddMotorVelocity(20.0f,0.0f);
	}

	void OnRush(){
		if (rushTimer > 0.0f){
			rushTimer -= Time.deltaTime;
			float dir = user.transform.localScale.x > 0.0f ? 1.0f : -1.0f;
			user.motor.AddSelfMove(1.0f * dir,0.0f);
		} else {
			run = false; 
			user.motor.horzentalSpeed = orgHorzentalSpeed;
			user.motor.verticalSpeed = orgVerticalSpeed;
		}
	}
}
