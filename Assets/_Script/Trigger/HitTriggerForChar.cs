using UnityEngine;
using System.Collections;

public class HitTriggerForChar : CollisionTrigger {
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
		//先从节点层级找//
		Motor2D motor = go.GetComponent<Motor2D>();
		if (motor == null){
			//找不到，从父节点找
			motor = go.GetComponentInParent<Motor2D>();
		}
		Character other = go.GetComponent<Character>(); //伤害碰撞应该和角色信息在一个节点
		if (other == null){
			Debug.LogError("other is nil,maybe layer is wrong");
		}
		if (other.Invincible || other.IsDie())
			return;
		if (motor == null) {
			Debug.Log("no motor found" + go.name);
		} else {
			float dir = 1.0f;
			if (Attacker.transform.position.x > other.transform.position.x){
				dir = -1.0f;
			}
			motor.AddAdditionVelocity(pushVelocity.x * dir,pushVelocity.y);
			Attacker.OnHitOther(other);//other不能为空
			other.OnHit(Attacker,damage);
		}

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
