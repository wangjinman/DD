using UnityEngine;
using System.Collections;
using System;

public class SkillBase : MonoBehaviour{
	public float cooldownTime = 1.0f; //how fast can use
	float cooldownTimer;
	public Character user {get;set;}

	void Update(){
		if (cooldownTime > 0.0f)
			cooldownTimer -= Time.deltaTime;

		OnUpdate();
	}

	public void Use(Character user){
		if (CanUse()){
			this.user = user;
			cooldownTimer = cooldownTime;
			OnUse();
		}
	}

	public virtual void OnUpdate(){

	}

	public virtual void OnUse(){

	}
	
	public virtual bool CanUse()
	{
		return cooldownTimer <= 0.0f;
	}

	public virtual void HandleInput(InputData data){

	}
}
