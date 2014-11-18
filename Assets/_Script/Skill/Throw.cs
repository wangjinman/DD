using UnityEngine;
using System.Collections;

public class Throw : SkillBase {
	public GameObject throwObj;
	public int count = 1; //how much
	public float delay = 0.5f; //count不为1时，每次扔的间隔//
	public float startAngel = 0.0f; //开始角度//
	public float angelIncreasement = 0.0f; //每次的角度增量//
	//每次的位置偏移//
	public Vector3 positionIncreasement = new Vector2(0.0f,0.0f);
	public float force = 10.0f;
	public float forceIncreasement = 0.0f; //每次的速度增量//

	public Vector2[] otherForces;

	public override void OnUse(){
		StartCoroutine(ThrowCroutine());
	}

	IEnumerator ThrowCroutine(){
		for (int i=0; i<count;i++){
			DoThrow(i);
			yield return new WaitForSeconds(delay);
		}
	}

	Vector2 CaclForce(float force,float angel){
		float radius = Mathf.Deg2Rad * angel;
		float y = force * Mathf.Sin(radius);
		float x = force * Mathf.Cos(radius);
		return new Vector2(x,y);
	}
	
	void DoThrow(int i){
		Vector3 currentPos = this.transform.position + positionIncreasement * i;
		float currentforce = force + forceIncreasement * i;
		float currentAgel = startAngel + angelIncreasement * i;
		GameObject go = Instantiate(throwObj,currentPos,Quaternion.identity) as GameObject;
		HitTriggerForChar hit = go.GetComponent<HitTriggerForChar>();
		if (hit == null){
			hit = go.GetComponentInChildren<HitTriggerForChar>();
		}
		if (hit != null){
			hit.Attacker = user;
		}
		Rigidbody2D rb2D = go.GetComponent<Rigidbody2D>();
		Vector2 curforce = CaclForce(currentforce,currentAgel);
		float normal = this.transform.parent.localScale.x > 0.0f ? 1.0f : -1.0f;
		curforce.x *= normal;
		go.transform.localScale = new Vector3(go.transform.localScale.x * normal,go.transform.localScale.y);
		rb2D.AddForce(curforce);
		for (int j=0; j<otherForces.Length; j++){
			rb2D.AddForce(otherForces[j]);
		}
	}
}
