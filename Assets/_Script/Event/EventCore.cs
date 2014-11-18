using UnityEngine;
using System.Collections;

namespace DD{
	public class EventCore {
		private static EventDispatcher dispatcher;
		private static EventCore self;

		private EventCore(){
			dispatcher = new EventDispatcher();
		}

		public static EventCore GetInstance(){
			if (self == null){
				self = new EventCore();
			}
			return self;
		}

		public void SendEvent(GameEvent e){
			dispatcher.DispatchEvent(e);
		}

		public void AddEventListener(string eventType,OnEventListener l){
			dispatcher.AddEventListener(eventType,l);
		}

		public void RemoveListener(string eventType,OnEventListener l){
			dispatcher.RemoveListener(eventType,l);
		}
	}
}

