using UnityEngine;
using System.Collections;

public class ContinuesHit : CollisionTrigger {
	public int damage = 1;
	public Vector2 pushVelocity = new Vector2(5.0f,1.0f);

	public Character Attacker {get;set;}

	public bool destroyWhenHit = false; //use for fly obj

	public GameObject hitEffect;

	void Awake(){
		Attacker = GetComponent<Character>(); //尝试找到角色
		if (Attacker == null){
			Attacker = GetComponentInParent<Character>();
		}
	}

	public override void OnConllisonTrigger(GameObject go){
		Character other = go.GetComponent<Character>(); //伤害碰撞应该和角色信息在一个节点
		if (other == null){
			Debug.LogError("other is nil,maybe layer is wrong");
		}

		DD.Hit hit = new DD.Hit(damage,Attacker);
		hit.pushForceX = pushVelocity.x;
		hit.pushForceY = pushVelocity.y;
		hit.HitPosiontX = transform.position.x;
		hit.HitPostionY = transform.position.y;

		other.OnHit(hit);

		Attacker.OnHitOther(other);//other不能为空

		//DD.GameEvent e = new DD.GameEvent("hit",this);

		if (destroyWhenHit){
			Destroy(this.gameObject);
		}

		if (hitEffect != null){
			Instantiate(hitEffect,transform.position,Quaternion.identity);
		}

		if (other.onHitEffect != null) {
			Instantiate(other.onHitEffect,transform.position,Quaternion.identity);
		}
	}
}
