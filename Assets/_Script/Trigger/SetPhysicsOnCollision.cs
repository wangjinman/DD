using UnityEngine;
using System.Collections;

public class SetPhysicsOnCollision : CollisionTrigger {
	
	public float gravityScale = 1.0f;
	public bool isTrigger = false;
	public int newLayer = 0;

	void Start(){

	}

	public override void OnConllisonTrigger(GameObject go) {
		this.rigidbody2D.gravityScale = gravityScale;
		this.collider2D.isTrigger = isTrigger;
		//Debug.Log(LayerMask.LayerToName(newMask));
		this.gameObject.layer = newLayer;
	}
}
