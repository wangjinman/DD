using UnityEngine;
using System.Collections;

public class HitBehavior : MonoBehaviour {

	public DD.Hit hit {get;set;}

	public LayerMask mask {get;set;}

	void OnTriggerEnter2D(Collider2D coll){
		GameObject go = coll.gameObject;

		if (go == null){
			return;
		}

		if (( 1 << go.layer & mask) != 0 ){
			EventRecver recver = go.GetComponent<EventRecver>();
			DD.GameEvent e = new DD.GameEvent("hit",this);
			e.objArg0 = hit;
			recver.SendEvent(e);
		}
	}
}
