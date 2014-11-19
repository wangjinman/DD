using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace DD{
	public class GameEvent {
		public string type {get;set;}
		public object sender {get;set;}
		public int arg0;
		public int arg1;
		public object objArg0;
		public object objArg1;

		public GameEvent(string type,object sender){
			this.type = type;
			this.sender = sender;
		}
	}

	public delegate void OnEventListener(GameEvent e);

	public class EventDispatcher{
		Hashtable eventListeners;

		public EventDispatcher(){
			eventListeners = new Hashtable();
		}

		public void AddEventListener(string eventType,OnEventListener listener){
			if (eventListeners.ContainsKey(eventType)){
				ArrayList list = eventListeners[eventType] as ArrayList;
				list.Add(listener);
			} else {
				var list = new ArrayList();
				list.Add(listener);
				eventListeners.Add(eventType,list);
			}
		}

		public void RemoveListener(string eventType,OnEventListener listener){
			if (eventListeners.ContainsKey(eventType)){
				ArrayList list = eventListeners[eventType] as ArrayList;
				list.Remove(listener);
			}
		}

		public void DispatchEvent(GameEvent e){
			if (!eventListeners.ContainsKey(e.type)){
				return;
			}
			ArrayList list = eventListeners[e.type] as ArrayList;
			foreach(OnEventListener l in list){
				l(e);
			}
		}
	}
}
