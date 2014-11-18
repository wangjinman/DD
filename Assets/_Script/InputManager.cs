using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Motor2D))]
[RequireComponent (typeof(CharacterController2D))]

public class InputData{
	public Vector2 move = new Vector2(0.0f,0.0f);
	public bool useSkill1 = false;
	public bool useSkill2 =false;
}

public class InputManager : MonoBehaviour
{
	bool lockMove; 
	bool lockJump;

	public float inputDelay = 0.05f; //输入延迟，限制最快输入//
	float inputTimerForLeft;
	float inputTimerForRight;

	Player player;
	
	void Awake()
	{
		// listen to some events for illustration purposes
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		player = go.GetComponent<Player>();
	}

	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		InputData data = new InputData();
		// grab our current _velocity to use as a base for all calculations
		//Vector3 velocity = new Vector3();

		if( Input.GetKey( KeyCode.A ) && inputTimerForLeft <= 0.0f )
		{
			inputTimerForLeft = inputDelay;
			data.move.x = -1.0f;
		}
		else if( Input.GetKey( KeyCode.D ) && inputTimerForRight <= 0.0f )
		{
			inputTimerForRight = inputDelay;
			data.move.x = 1.0f;
		}
		else
		{
			data.move.x  = 0;
		}
		
		// we can only jump whilst grounded
		if (Input.GetKeyDown(KeyCode.J))
		{
			data.move.y = 1.0f;
		}

		if (lockMove)
			data.move.x = 0.0f;
		if (lockJump)
			data.move.y = 0.0f;

		if (inputTimerForLeft > 0.0f)
			inputTimerForLeft -= Time.deltaTime;

		if (inputTimerForRight > 0.0f)
			inputTimerForRight -= Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.K)){
			data.useSkill1 = true;
		}

		if (Input.GetKeyDown(KeyCode.L)){
			data.useSkill2 = true;
		}

		//player.HandleInput(data);
		DD.GameEvent e = new DD.GameEvent("input",this);
		e.args = data;
		DD.EventCore.GetInstance().SendEvent(e);
		//motor.MotorVelocity = velocity;
	}

	public void LockMove(){
		this.lockMove = true;
	}

	public void UnlockMove(){
		this.lockMove = false;
	}

	public void LockJump(){
		this.lockJump = true;
	}

	public void UnlockJump(){
		this.lockJump = false;
	}
}
