using UnityEngine;
using System.Collections;

public class EventRecver : MonoBehaviour {
	DD.EventDispatcher dispatcher;

	public void SendEvent(DD.GameEvent e){
		dispatcher.DispatchEvent(e);
	}
}
