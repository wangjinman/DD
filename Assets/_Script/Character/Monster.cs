using UnityEngine;
using System.Collections;

public class Monster : Character {
	public GameObject dieParticle;


	// Use this for initialization
	void Start () {
		OnInit();
	}
	
	// Update is called once per frame
	void Update () {
		Tick();
	}

	public override void OnDie(Character killer){
		if (dieParticle != null){
			Instantiate(dieParticle,transform.position,Quaternion.identity);
		}
		Destroy(gameObject);
	}
}
