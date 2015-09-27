using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AI<T> : MonoBehaviour where T : MonoBehaviour {
	public T target { get; private set; }
	List<AIBehaviour<T>> behaviours;
	Queue<Action<T>> actionQueue;

	//Behaviours
	public ICollection<AIBehaviour<T>> Behaviours {
		get { return new List<AIBehaviour<T>>(behaviours); }
	}
	public void AddBehaviour(AIBehaviour<T> b) {
		b.ai = this;
		b.target = target;
		b.Start();
		behaviours.Add (b);
	}
	public void RemoveBehaviour(AIBehaviour<T> b) {
		behaviours.Remove (b);
	}

	//Actions
	public Queue<Action<T>> ActionQueue {
		get { return new Queue<Action<T>>(actionQueue); }
	}
	public bool Do(Action<T> action) {
		if(action.IsAllowed(target)) {
			actionQueue.Enqueue(action);
			return true;
		}
		else {
			return false;
		}
	}
	public bool Do<V>(params object[] args) where V : Action<T> {
		Action<T> action = System.Activator.CreateInstance(typeof(V),args) as Action<T>;
		return Do (action);
	}

	void Start() {
		target = GetComponent<T>();
		behaviours = new List<AIBehaviour<T>>();
		actionQueue = new Queue<Action<T>>();
		Setup();
	}
	protected abstract void Setup();

	void Update() {
		UpdateBehaviours();
		if(actionQueue.Count > 0) {
			DoNextAction();
		}
	}
	void UpdateBehaviours() {
		foreach(AIBehaviour<T> b in behaviours) {
			if(b.Done()) {
				RemoveBehaviour(b);
			}
			else {
				b.Update();
			}
		}
	}
	void DoNextAction() {
		Action<T> action = actionQueue.Dequeue();
		if(action.IsAllowed(target)) {
			action.Execute(target);
		}
	}
}
