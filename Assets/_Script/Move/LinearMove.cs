using UnityEngine;
using System.Collections;

//指定两点之间线性匀速移动//
//会自动设置position到startPosition//

public class LinearMove : MonoBehaviour {

	public Transform startPosition;
	public Transform endPosition;
	public float speed = 10.0f;
	public bool IsPingPong; //是否来回移动//
	
	// Use this for initialization
	void Start () {
		GoTweenConfig config = new GoTweenConfig();
		if (startPosition == null){
			Debug.LogError("Start Postion must not be null");
			return;
		} else if (endPosition == null){
			Debug.LogError("End Postion must not be null");
			return;
		}
		GoSpline path = new GoSpline(new Vector3[]{startPosition.position,endPosition.position});
		config.positionPath(path);
		config.setIterations (-1,GoLoopType.PingPong);
		float duration = Vector3.Distance(startPosition.position,endPosition.position) / speed;
		Go.to(gameObject.transform,duration,config);
	}

	void OnDrawGizmos()
	{
		if (startPosition != null && endPosition != null)
		{
			var lpath = new GoSpline(new Vector3[]{startPosition.position,endPosition.position});
			lpath.drawGizmos(1.0f);
		}
	}
}
