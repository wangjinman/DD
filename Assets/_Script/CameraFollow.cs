using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization

	[SerializeField]
	Vector3 max = new Vector2(5.0f,5.0f);
	[SerializeField]
	Vector3 min = new Vector2(-5.0f,-5.0f);
	[SerializeField]
	Vector3 offset2Move = new Vector2(3.0f,3.0f);
	[SerializeField]
	float cameraFollowSpeed = 1.0f;

	[SerializeField]
	GameObject target;
	//Vector3 orgPos;

	Vector3 maxPos;
	Vector3 minPos;
	
	void Start () {
		Transport(this.transform.position);
	}

	void Transport(Vector3 newPos) {
		//orgPos = newPos;
		maxPos = max + newPos;
		minPos = min + newPos;
	}
	
	// Update is called once per frame
	//test
	void LateUpdate () {
		Vector3 targetPos = target.transform.position;
		Vector3 myPos = this.transform.position;
		Vector3 offset = myPos - targetPos;
		//Vector3 offset2Org = myPos - orgPos;
		Vector3 newPos = new Vector3(myPos.x,myPos.y,myPos.z);
		if (Mathf.Abs(offset.x) > offset2Move.x){
			newPos.x =  Mathf.Lerp(myPos.x,Mathf.Clamp(targetPos.x,minPos.x,maxPos.x),Time.deltaTime * cameraFollowSpeed);
		}
		if (Mathf.Abs(offset.y) > offset2Move.y){
			newPos.y = Mathf.Lerp(myPos.y,Mathf.Clamp(targetPos.y,minPos.y,maxPos.y),Time.deltaTime * cameraFollowSpeed);
		}
		transform.position = newPos;
	}
}
