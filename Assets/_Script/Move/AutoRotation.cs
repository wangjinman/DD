using UnityEngine;
using System.Collections;

public class AutoRotation : MonoBehaviour {

	public float speed = 1.0f;

	public LayerMask stopByConllisonLayer;

	GoTween tween;

	// Use this for initialization
	void Start () {
		GoTweenConfig config = new GoTweenConfig();
		config.setIterations(-1);
		float normal = this.transform.localScale.x > 0.0f ? 1.0f : -1.0f;
		config.localEulerAngles(new Vector3(0.0f,0.0f,-360.0f * normal)); //顺时针
		tween = Go.to( gameObject.transform, 1.0f / speed ,config);
	}

	public void Pause(){
		tween.pause();
	}

	void OnDestroy(){
		tween.destroy();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (tween.state == GoTweenState.Paused){
			return;
		}

		if (( 1 << coll.gameObject.layer & stopByConllisonLayer) != 0 ){
			tween.pause();
		}
	}
}
