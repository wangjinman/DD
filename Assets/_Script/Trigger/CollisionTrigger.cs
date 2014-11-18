using UnityEngine;
using System.Collections;

public class CollisionTrigger : MonoBehaviour {

	public LayerMask mask = 0;
	public bool excuteOnce = true;

	bool excuted = false;

	void OnCollisionEnter2D(Collision2D coll) {
		TestCollison(coll.gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll){
		TestCollison(coll.gameObject);
	}

	void TestCollison(GameObject go){
		if (excuteOnce && excuted)
			return;
		
		if (( 1 << go.layer & mask) != 0 ){
			OnConllisonTrigger(go);
			excuted = true;
		}
	}

	void OnCollisionStay2D(Collision2D coll){
		TestCollison(coll.gameObject);
	}

	void OnTriggerStay2D(Collider2D coll){
		TestCollison(coll.gameObject);
	}

	public virtual void OnConllisonTrigger(GameObject go){

	}
}
