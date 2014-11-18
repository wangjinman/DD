using UnityEngine;
using System.Collections;

public class DestroyByCollision : CollisionTrigger {
	public float delay = 1.0f;

	public override void OnConllisonTrigger(GameObject go){
		Destroy(this.gameObject,delay);
	}
}
