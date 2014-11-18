using UnityEngine;
using System.Collections;

public class Spawer : MonoBehaviour {
	public GameObject spawObj;
	public int count = 1;
	public float delay = 1.0f;

	// Use this for initialization
	void Start () {
		Spaw();
	}

	void OnEnable(){
	}

	IEnumerator SpawCoroutine(){
		for (int i=0; i<count; i++){
			Instantiate(spawObj,transform.position,Quaternion.identity);
			yield return new WaitForSeconds(delay);
		}
	}

	public void Spaw(){
		StartCoroutine(SpawCoroutine());
	}
}
