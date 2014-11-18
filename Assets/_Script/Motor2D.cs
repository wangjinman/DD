using UnityEngine;
using System.Collections;


public class Motor2D : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float horzentalSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float verticalSpeed = 3f;

	private CharacterController2D _controller;
	//private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;

	void Awake()
	{
		_controller = GetComponent<CharacterController2D>();
		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
	}

	Vector3 selfMove;
	public Vector3 MotorVelocity {
		get {return selfMove;}
		set {selfMove = value;}
	}

	public void SetSelfMove(float x,float y){
		selfMove.x = x;
		selfMove.y = y;
	}

	public void AddSelfMove(float x,float y){
		selfMove.x += x;
		selfMove.y += y;
	}

	Vector3 additionVelocity; //额外的速度//
	public void AddAdditionVelocity(float x,float y){
		additionVelocity.x += x;
		additionVelocity.y += y;
	}
	public void SetAdditionVelocity(float x,float y){
		additionVelocity.x = x;
		additionVelocity.y = y;
	}

	private Transform activePlatform;
	private Vector3 activeLocalPlatformPoint;
	private Vector3 activeGlobalPlatformPoint; 
	private Vector3 lastPlatformVelocity;
	// If you want to support moving platform rotation as well: 
	private Quaternion activeLocalPlatformRotation; 
	private Quaternion activeGlobalPlatformRotation;
	
	void onControllerCollider( RaycastHit2D hit )
	{
		activePlatform = hit.transform;
	}
	
	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

		if( MotorVelocity.x > 0.0f )
		{
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
		}
		else if( MotorVelocity.x < 0.0f )
		{
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
		}

		if( MotorVelocity.y > 0.0f )
		{
			//vt = 2gh
			_velocity.y = verticalSpeed * MotorVelocity.y;
		}

		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, MotorVelocity.x * horzentalSpeed, Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		_velocity += additionVelocity;
		
		// Moving platform support
		if (activePlatform != null) {
			var newGlobalPlatformPoint = activePlatform.TransformPoint(activeLocalPlatformPoint);
			var moveDistance = (newGlobalPlatformPoint - activeGlobalPlatformPoint);
			if (moveDistance != Vector3.zero)
				_controller.move(moveDistance);
			//lastPlatformVelocity = (newGlobalPlatformPoint - activeGlobalPlatformPoint) / Time.deltaTime;
			
			// If you want to support moving platform rotation as well:
			var newGlobalPlatformRotation = activePlatform.rotation * activeLocalPlatformRotation;
			var rotationDiff = newGlobalPlatformRotation * Quaternion.Inverse(activeGlobalPlatformRotation);
			
			// Prevent rotation of the local up vector
			rotationDiff = Quaternion.FromToRotation(rotationDiff * transform.up, transform.up) * rotationDiff;
			
			transform.rotation = rotationDiff * transform.rotation;
		}
		else {
			lastPlatformVelocity = Vector3.zero;
		}
		activePlatform = null;
		
		_controller.move( _velocity * Time.deltaTime );
		
		// Moving platforms support
		if (activePlatform != null) {
			activeGlobalPlatformPoint = transform.position;
			activeLocalPlatformPoint = activePlatform.InverseTransformPoint (transform.position);
			
			// If you want to support moving platform rotation as well:
			activeGlobalPlatformRotation = transform.rotation;
			activeLocalPlatformRotation = Quaternion.Inverse(activePlatform.rotation) * transform.rotation; 
		}

		SetSelfMove(0.0f,0.0f);
		SetAdditionVelocity(0.0f,0.0f);
	}
}
