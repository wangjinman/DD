using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Motor2D))]
[RequireComponent (typeof(CharacterController2D))]
public class Character : MonoBehaviour {
	[SerializeField]
	int MaxHP = 2;
	[SerializeField]
	int defend = 1;
	public int Defend {
		get {return defend;}
		set {defend = value;}
	}
	int curHP;
	public int HP{
		get {return curHP;}
		set {curHP = value;}
	}

	[SerializeField]
	float invincibleTimeAfterDamage = 0.7f;
	float invincibleAfterDamgeTimer;

	public GameObject onHitEffect;

	bool invincible = false;

	public Motor2D motor {get;set;}
	public CharacterController2D controller {get;set;}

	
	//是否无敌//
	public bool Invincible {
		get {return invincible;}
		set {invincible = value;}
	}

	// Use this for initialization
	void Start () {
		OnInit();
	}

	protected virtual void OnInit(){
		curHP = MaxHP;
		motor = GetComponent<Motor2D>();
		controller = GetComponent<CharacterController2D>();
	}

	// Update is called once per frame
	void Update () {
		Tick();
	}

	protected virtual void Tick(){
	}

	public virtual void OnHit(Character attacker,int damage){
		if (Invincible)
			return;

		if (HP <= 0)
			return;

		int trueDamage = damage - Defend;
		trueDamage = trueDamage <= 0 ? 1 : trueDamage;
		HP -= trueDamage;
		if (HP <= 0){
			OnDie(attacker);
			//if (attacker != null){
				attacker.OnKill(this);
			//}
		}
		//invincibleAfterDamgeTimer = invincibleTimeAfterDamage;
		StartCoroutine(InvincibleCroutine());
	}

	public virtual void OnHitOther(Character other){

	}

	public virtual void OnDie(Character killer){
		//Destroy(gameObject,1.0f);
	}

	public virtual void OnKill(Character other){
	}

	public virtual bool IsDie() {
		return curHP <= 0;
	}

	IEnumerator InvincibleCroutine(){
		Invincible = true;
		float invincibleAfterDamgeTimer = invincibleTimeAfterDamage;
		while (invincibleAfterDamgeTimer > 0.0f) {
			invincibleAfterDamgeTimer -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		Invincible = false;
	}
}
