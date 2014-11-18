using UnityEngine;
using System.Collections;

public class Func  {

	public static float GetDir(GameObject go){
		return go.transform.localScale.x > 0.0f ? 1.0f : -1.0f;
	}
}
